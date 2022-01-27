using UnityEngine.XR.MagicLeap;

using UnityEngine;

using System;
using System.Collections.Generic;

namespace UnityEditor.XR.MagicLeap
{
    /// <summary>
    /// A scriptable object that is used to cache image library binary blob
    /// generation data to prevent rebuilds everytime the user presses play in
    /// the editor.
    /// </summary>
    public class MagicLeapImageDatabaseLibraryCache : ScriptableObject
    {

        /// <summary>
        /// The dictionary that maps all image database assets to the cache.
        /// </summary>
        public Dictionary<string, DateTime> m_LibraryCache;
    }
}