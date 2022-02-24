using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScenes : MonoBehaviour
{
    int currentScene = 0, numberOfScenes = 4;
    public string[] Scenes;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Switch Scenes"))
        {
            currentScene = currentScene + 1; //increment the number of the current scene
            currentScene = currentScene % numberOfScenes; //makes sure its within index

            Debug.Log("Scene" + currentScene + " loading: " + Scenes[currentScene]);
            LoadScene(Scenes[currentScene]);
        }
    }

    public void LoadScene(string nameOfScene)
    {
        Debug.Log("Loading Scene: " + nameOfScene);
        SceneManager.LoadScene(nameOfScene, LoadSceneMode.Single); // make it say LoadSceneMode.Additive for Menu
    }
    /*
    public void LoadSceneMenu(string nameOfScene){ // for menu overlay - still working on
        Debug.Log("Loading Scene: " + nameOfScene);
        SceneManager.LoadScene(nameOfScene, LoadSceneMode.Additive); 
    }
    */
}
