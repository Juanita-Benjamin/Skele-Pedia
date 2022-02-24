using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetAllBonesRough : MonoBehaviour
{
    public GameObject[] allTheBones;
    public Vector3[] allTheBonesPositions;
    public Vector3[] originalPositions;
    public Vector3[] correctPositions;

    public GameObject temporaryGameObject;
    // Start is called before the first frame update
    void Start()
    {
        allTheBonesPositions = new Vector3[allTheBones.Length];
        originalPositions = new Vector3[allTheBonesPositions.Length];
        correctPositions = new Vector3[allTheBonesPositions.Length];

        for(int i = 0; i < allTheBones.Length; i++){
            allTheBonesPositions[i] = allTheBones[i].transform.position;
            correctPositions[i] = allTheBonesPositions[i];
        }
        
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Randomize() {
         for (int i = 0; i < allTheBones.Length; i++) {
             int rnd = Random.Range(0, allTheBones.Length);
             temporaryGameObject = allTheBones[rnd];
             allTheBones[rnd] = allTheBones[i];
             allTheBones[i] = temporaryGameObject;
         }
         for(int i = 0; i < allTheBones.Length; i++){
            allTheBones[i].transform.position = allTheBonesPositions[i];
        }
        saveCurrentSet();
     }
     public void saveCurrentSet(){
         for(int i = 0; i < allTheBones.Length; i++){
            originalPositions[i] = allTheBonesPositions[i];
        }
     }
    public void resetAll(){
        for(int i = 0; i < allTheBones.Length; i++){
            allTheBones[i].transform.position = originalPositions[i];
        }
    }
    public void makeCorrect(){
        for(int i = 0; i < allTheBones.Length; i++){
            allTheBones[i].transform.position = correctPositions[i];
        }
    }

}
