using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;
//using System;

public class TriviaGame : MonoBehaviour
{
    public bool randomCorrectAnswer = true;
    public int i = 0; //index
    
    public List<GameObject> Bones = new List<GameObject>();
    public List<GameObject> correctBones = new List<GameObject>();
    public List<GameObject> temp = new List<GameObject>(); 
    //public int length;

    public List<Button> options = new List<Button>();
    public List<Transform> optionsTransforms = new List<Transform>();
    public List<Text> optionsText;
    
    public GameObject correctAnswer;
    public List<string> choices = new List<string>();
    
    public Text QuestionText;
    public int clicked;

    public bool displaySingleBone = true;
    public bool highlightSingleBone = true;

    private GameObject boneOnDisplay;
    public Button nextQuestionButton;

    public bool gameEnd = false;

    public Button tempButton;
    void Start()
    {


        copyBones();
        //Reset();
        Debug.Log("Bones:" + Bones.Count);
    }

    void copyBones(){
        correctBones = Bones;
        for(int i = 0; i<Bones.Count;i++){
            correctBones[i] = Bones[i];
        }
    }

    void loadText(){
        for(int i=0; i<choices.Count;i++){
            optionsText[i].text = choices[i];
        }
    }

    void displayBone(){
        //correctAnswer.GetComponent<highLight>().deHighLight(); 

        boneOnDisplay = Instantiate(correctAnswer,transform.position, Quaternion.identity);
        boneOnDisplay.GetComponent<highLight>().deHighLight();
        modifyPosition(boneOnDisplay.transform);
        //boneOnDisplay.transform.localScale = new Vector3(5f,5f,5f); //make it twice as big as it normally would be
    }

    void modifyPosition(Transform boneOnDisplayTransform){
        //trying to fix the center position issue by using gameObject tags that say there the game object center should be
        Vector3 offset = transform.position - boneOnDisplayTransform.Find("center").transform.position; //holds x, y, z values
        Debug.Log(offset);
        offset += new Vector3(0f, 0f, -3f); //also bring it closer to the camera
        //offset += new Vector3(0f, 2f, 0f); //move it a little up
        boneOnDisplayTransform.position = offset;
    }
    

    void displayCorrect(){
        
    }

    public void highlight(){
        correctAnswer.AddComponent<highLight>();
        correctAnswer.GetComponent<highLight>().hover = false;
        correctAnswer.GetComponent<highLight>().highLightThis();
    }

    public void Reset()
    {
        Debug.Log("Reseting!");
        int correctAnswerIndex = Random.Range(0,correctBones.Count);
        correctAnswer = correctBones[correctAnswerIndex]; //gets random correct bones```  `
        //remove from correct bones
        correctBones.RemoveAt(correctAnswerIndex); //
        //temp = correctBones.ToList();

        

        choices[0] = correctAnswer.name; //multiple choice first answer = correct answer (later randomized)
        // for(int i = 1; i<choices.Count-1;i++){
        //     int randVal = Random.Range(0,Bones.Count);
        //     string randValStr = Bones[randVal].name;
        //     choices[i] = randValStr;
        //     temp.RemoveAt(randVal);   
        // }
=======
        for(int i = 0; i < correctBones.Count; i++ ){
            temp[i] = correctBones[i];
        }

        choices[0] = correctAnswer.name; //multiple choice first answer = correct answer (later randomized)
        for(int i = 1; i<choices.Count;i++){
            int randVal = Random.Range(0,temp.Count);
            string randValStr = temp[randVal].name;
            choices[i] = randValStr;
            temp.RemoveAt(randVal);   
        }
        
        loadText();
         if(boneOnDisplay!=null)
             Destroy(boneOnDisplay);
        if(displaySingleBone){
            displayBone();
        }
        if(highlightSingleBone){
            highlight();
        }

        Debug.Log("correct Bones:" + correctBones.Count);
        Debug.Log("Bones:" + Bones.Count);
        
        randomizeButtons(); //randomizes button order
        
    }

    void randomizeButtons(){
        
        Vector3 temp;
        for (int i = 0; i < optionsTransforms.Count; i++) {
             int rnd = Random.Range(0, optionsTransforms.Count); //random number created 
             temp = optionsTransforms[rnd].position; //we set the currently,
             optionsTransforms[rnd].position = optionsTransforms[i].position;
             optionsTransforms[i].position = temp;
         }
        Vector3 tempPosition = new Vector3();
    }
    
    
}
