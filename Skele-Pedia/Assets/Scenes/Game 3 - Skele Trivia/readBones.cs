using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readBones : MonoBehaviour
{
    //has not been used in the project thus far.
    //reads all the bones in a premade skeleton gameObject and sends that list to the trivia game list
    public GameObject skeleton;
    public List<string> listOfBones = new List<string>();

    public GameObject skull;
    public string name;

    void Start(){
        name = skull.name;
    }
    void loadBones(){
        //foreach (GameObject child in skeleton) {
         //   listOfBones.Add(child.name); 
        //}
    }
}
