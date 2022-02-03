using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCamera : MonoBehaviour
{   
    public string currentName; //current place you are located in
    public GameObject currentCamera; // so we know which camera is being viewed

    public Camera[] cameras;
    private int index = 0; //current camera index

    public string[] regionNames  = new [] { 
            "Full View",
            "Side View",
            "Other Side View"
    };
    

    public KeyCode key = KeyCode.Space;

    void Start(){
        index = 0;
         
        //Turn all cameras off, except the first default one
        for (int i=1; i<cameras.Length; i++) 
        {
            cameras[i].gameObject.SetActive(false);
        }
        
        //If any cameras were added to the controller, enable the first one
        if (cameras.Length>0)
        {
            cameras [0].gameObject.SetActive(true);
            Debug.Log ("Camera with name: " + cameras [0].GetComponent<Camera>().name + ", is now enabled");
        }
        
    }
    void Update(){
        if (Input.GetKeyDown(key))
        {
            Debug.Log("Pressed: "+key.ToString()+" moving to next camera");
            nextCamera();
            
        }
    }
    public void nextCamera(){
        
        index ++;
        SetCamera();

        Debug.Log ("Camera with name: " + cameras [index].GetComponent<Camera>().name + ", is now enabled");
        SetName(regionNames[index]);

    }
    public void SetCamera(){
        if (index < cameras.Length)
        {
            cameras[index-1].gameObject.SetActive(false);
            cameras[index].gameObject.SetActive(true);
        }
        else
        {
            cameras[index-1].gameObject.SetActive(false);
            index = 0;
            cameras[index].gameObject.SetActive(true);
        }
    }
    public void SetName(string name){
        currentName = name;
    }
    public string GetName(){
        return currentName;
    }
}
