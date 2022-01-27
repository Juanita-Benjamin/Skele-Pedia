using UnityEngine.XR.MagicLeap;

using System;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using UnityEditor.Build;
using UnityEngine;
using Unity.Jobs;
using UnityEngine.XR.ARSubsystems;
using System.Collections;

namespace UnityEditor.XR.MagicLeap
{
    public static class MagicLeapImageDatabaseBuildProcessor
    {
        public class InvalidImageSettingsException : Exception { }
        public class InvalidImagePhysicalSizeException : Exception { }

        private class DisposableList : IDisposable
        {
            public List<NativeArray<byte>> m_ListOfNativeArrays;

            public DisposableList(int count)
            {
                m_ListOfNativeArrays = new List<NativeArray<byte>>(count);

                for (int i = 0; i < count; ++i)
                    m_ListOfNativeArrays.Add(default);
            }

            public void Dispose()
            {
                foreach (var nativeArray in m_ListOfNativeArrays)
                {
                    if (nativeArray.IsCreated)
                    {
                        nativeArray.Dispose();
                    }
                }
            }
        }

        static MagicLeapImageDatabaseLibraryCache s_LibraryCache;

        static readonly string k_ImageLibraryModificationCachePath = Path.Combine("Assets" + MagicLeapImageTrackingSubsystem.k_StreamingAssetsPath.Substring(Application.dataPath.Length), "ImageLibraryCache.asset");

        public static IEnumerable<T> AssetsOfType<T>() where T : UnityEngine.Object
        {
            foreach (var guid in AssetDatabase.FindAssets("t:" + typeof(T).Name))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                yield return AssetDatabase.LoadAssetAtPath<T>(path);
            }
        }

        static MagicLeapImageDatabaseLibraryCache CreateOrCleanupImageCacheFileIfNeeded(List<string> assetGuids)
        {
            string libraryCachePath = k_ImageLibraryModificationCachePath;

            var libraryCache = AssetDatabase.LoadAssetAtPath<MagicLeapImageDatabaseLibraryCache>(libraryCachePath);

            if (libraryCache == null)
            {
                libraryCache = ScriptableObject.CreateInstance<MagicLeapImageDatabaseLibraryCache>();
                libraryCache.m_LibraryCache = new Dictionary<string, DateTime>();
            }
            else
            {
                foreach (var libraryGuid in libraryCache.m_LibraryCache)
                {
                    if (!assetGuids.Contains(libraryGuid.Key))
                        libraryCache.m_LibraryCache.Remove(libraryGuid.Key);
                }
            }

            return libraryCache;
        }

        static bool DoesLibraryNeedToBeUpdated(string libraryGuid, ref Dictionary<string, DateTime> libraryCache)
        {
            var libraryPath = AssetDatabase.GUIDToAssetPath(libraryGuid);
            var pakFilePath = Path.Combine(MagicLeapImageTrackingSubsystem.k_StreamingAssetsPath, libraryGuid + ".imgpak");
            var currentTimestamp = File.GetLastWriteTime(libraryPath);

            if (libraryCache.ContainsKey(libraryGuid) || File.Exists(pakFilePath))
            {
                var previousTimestamp = libraryCache[libraryGuid];

                if (!(previousTimestamp < currentTimestamp))
                {
                    return false;
                }
            }

            libraryCache[libraryGuid] = currentTimestamp;
            return true;
        }

        static void SaveStaticLibraryCachePostBuild()
        {
            if (AssetDatabase.Contains(s_LibraryCache))
                AssetDatabase.SaveAssets();
            else
                AssetDatabase.CreateAsset(s_LibraryCache, k_ImageLibraryModificationCachePath);
        }

        static void SetTemporaryTextureImportSettingsIfNeeded(
            string textureAssetPath,
            Texture2D texture,
            out TextureImporterSettings maybeOriginalImportSettings,
            out TextureImporterPlatformSettings maybeOriginalPlatformSettings)
        {
            maybeOriginalImportSettings = null;
            maybeOriginalPlatformSettings = null;

            // Should the texture import settings be set to values that are not supported reimport the texture with values that are supported.
            if (!texture.isReadable || texture.mipmapCount > 1 || !IsImageFormatSupported(texture.format))
            {
                var textureImporter = (TextureImporter)AssetImporter.GetAtPath(textureAssetPath);

                maybeOriginalImportSettings = new TextureImporterSettings();
                textureImporter.ReadTextureSettings(maybeOriginalImportSettings);

                maybeOriginalPlatformSettings = textureImporter.GetPlatformTextureSettings("Lumin");
                var luminImportSettings = textureImporter.GetPlatformTextureSettings("Lumin");

                textureImporter.isReadable = true;
                textureImporter.mipmapEnabled = false;
                luminImportSettings.overridden = true;
                luminImportSettings.format = TextureImporterFormat.RGBA32;
                textureImporter.SetPlatformTextureSettings(luminImportSettings);
                textureImporter.SaveAndReimport();
            }
        }

        static void ResetTextureImportSettingsForTexture(string textureAssetPath, TextureImporterSettings maybeOriginalImportSettings, TextureImporterPlatformSettings maybeOriginalPlatformSettings)
        {
            if (maybeOriginalImportSettings != null)
            {
                var textureImporter = (TextureImporter)AssetImporter.GetAtPath(textureAssetPath);
                textureImporter.SetTextureSettings(maybeOriginalImportSettings);
                textureImporter.SetPlatformTextureSettings(maybeOriginalPlatformSettings);
                textureImporter.SaveAndReimport();
            }
        }

        static JobHandle ScheduleRawTextureDataConversionJob(int index, List<NativeArray<byte>> nativeTextureDataCopies, ref Texture2D texture)
        {
            nativeTextureDataCopies[index] = new NativeArray<byte>(texture.width * texture.height, Allocator.TempJob);
            var textureData = new NativeArray<byte>(texture.GetRawTextureData<byte>(), Allocator.TempJob);
            var conversionHandle = ConversionJob.Schedule(
                textureData,
                new Vector2Int(texture.width, texture.height),
                texture.format, nativeTextureDataCopies[index],
                default(JobHandle));
            new DeallocateJob { data = textureData }.Schedule(conversionHandle);

            return conversionHandle;
        }

        /// <summary>
        /// Build the image tracking library binary blob asset and create a cache
        /// to indicate when files were last updated
        /// </summary>
#if PLATFORM_LUMIN
        [InitializeOnEnterPlayMode]
#endif
        public static void BuildImageTrackingAssets()
        {
            if (!Directory.Exists(MagicLeapImageTrackingSubsystem.k_StreamingAssetsPath))
                Directory.CreateDirectory(MagicLeapImageTrackingSubsystem.k_StreamingAssetsPath);

            var assetGuidList = new List<String>(AssetDatabase.FindAssets("t:XRReferenceImageLibrary"));

            s_LibraryCache = CreateOrCleanupImageCacheFileIfNeeded(assetGuidList);

            int count = 0;

            float overallProgress = 0.0f;

            foreach (var library in AssetsOfType<XRReferenceImageLibrary>())
            {
                if (!DoesLibraryNeedToBeUpdated(assetGuidList[count++], ref s_LibraryCache.m_LibraryCache))
                    continue;

                EditorUtility.DisplayProgressBar(
                                    "Building Magic Leap Image Tracking Libraries",
                                    library.name + ": " + assetGuidList[count - 1],
                                    overallProgress = ((count) / assetGuidList.Count));

                if (library.count > 25)
                    Debug.LogWarning($"Image Library '{library.name}' has an image count of '{library.count}' which is larger than the suggested number of images that can be tracked at once.  This library could suffer performance issues.");

                using (var fWriter = new FileStream(MagicLeapImageTrackingSubsystem.GetDatabaseFilePathFromLibrary(library), FileMode.Create, FileAccess.Write))
                {
                    var magicBytes = BitConverter.GetBytes(MagicLeapImageDatabase.kMagicBytes);
                    if (!BitConverter.IsLittleEndian)
                        Array.Reverse(magicBytes);

                    fWriter.Write(magicBytes, 0, magicBytes.Length);

                    JobHandle imageConversionJobsHandle = default;

                    using (DisposableList nativeTextureDataCopies = new DisposableList(library.count))
                    {
                        for (int i = 0; i < library.count; ++i)
                        {
                            var textureAssetPath = AssetDatabase.GUIDToAssetPath(library[i].textureGuid.ToString("N"));
                            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(textureAssetPath);

                            try
                            {
                                if (!library[i].specifySize)
                                    throw new InvalidImageSettingsException();

                                if (library[i].size.x < 0.00001 || library[i].size.y < 0.00001)
                                    throw new InvalidImagePhysicalSizeException();
                            }
                            catch (InvalidImageSettingsException)
                            {
                                throw new BuildFailedException($"Image '{texture.name}' that is a part of the Image Library at '{library.name}' is not specifying it's physical size in meters.  Magic Leap requires that a non-zero size be specified in order to track images appropriately.  See '{library.name}'=>'{texture.name}'=>'Specify Size'=>'Physical Size (meters)' for more information.");
                            }
                            catch (InvalidImagePhysicalSizeException)
                            {
                                throw new BuildFailedException($"Image '{texture.name}' in Image Library '{library.name}' has an extremely small (sub 0.00001 meters) or zero physical size parameter.  Magic Leap requires that a non-zero size be specified in order to track images appropriately.  See '{library.name}'=>'{texture.name}'=>'Specify Size'=>'Physical Size (meters)' for more information.");
                            }
                            finally
                            {
                                imageConversionJobsHandle.Complete();
                            }

                            SetTemporaryTextureImportSettingsIfNeeded(textureAssetPath, texture, out var maybeOriginalSettings, out var maybeOriginalPlatformSettings);

                            imageConversionJobsHandle = JobHandle.CombineDependencies(ScheduleRawTextureDataConversionJob(i, nativeTextureDataCopies.m_ListOfNativeArrays, ref texture), imageConversionJobsHandle);

                            ResetTextureImportSettingsForTexture(textureAssetPath, maybeOriginalSettings, maybeOriginalPlatformSettings);
                        }

                        imageConversionJobsHandle.Complete();

                        for (int i = 0; i < library.count; ++i)
                        {
                            var textureAssetPath = AssetDatabase.GUIDToAssetPath(library[i].textureGuid.ToString("N"));
                            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(textureAssetPath);

                            var textureBytes = nativeTextureDataCopies.m_ListOfNativeArrays[i].ToArray();
                            var widthBytes = BitConverter.GetBytes(texture.width);
                            var heightBytes = BitConverter.GetBytes(texture.height);

                            if (!BitConverter.IsLittleEndian)
                            {
                                Array.Reverse(widthBytes);
                                Array.Reverse(heightBytes);
                            }

                            fWriter.Write(widthBytes, 0, widthBytes.Length);
                            fWriter.Write(heightBytes, 0, heightBytes.Length);
                            fWriter.Write(textureBytes, 0, textureBytes.Length);
                        }
                    }
                }
            }
            SaveStaticLibraryCachePostBuild();

            EditorUtility.ClearProgressBar();
        }

        static bool IsImageFormatSupported(TextureFormat format)
        {
            switch (format)
            {
                case TextureFormat.R8:
                case TextureFormat.RGB24:
                case TextureFormat.RGBA32:
                    return true;

                default:
                    return false;
            }
        }

        internal struct DeallocateJob : IJob
        {
            [DeallocateOnJobCompletion]
            public NativeArray<byte> data;

            public void Execute() { }
        }
    }
}
