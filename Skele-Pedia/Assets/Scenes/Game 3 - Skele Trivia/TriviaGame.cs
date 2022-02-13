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
    //public int length;

    public List<Button> options = new List<Button>();
    public List<Text> optionsText;
    

    
    public GameObject correctAnswer;
    public List<string> choices = new List<string>();
    
    public Text QuestionText;
    public int clicked;

    public bool displaySingleBone = true;
    public bool highlightSingleBone = true;

    private GameObject boneOnDisplay;
    public Button nextQuestionButton;

    void Start()
    {
        Reset();
    }


    void loadText(){
        for(int i=0; i<choices.Count;i++){
            optionsText[i].text = choices[i];
        }
    }

    void displayBone(){
        correctAnswer.GetComponent<highLight>().deHighLight(); 

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

    public GameObject GetRandomCorrectAnswer(List<GameObject> Bones){
        return Bones[Random.Range(0,Bones.Count)];
    }

    public void highlight(){
        correctAnswer.AddComponent<highLight>();
        correctAnswer.GetComponent<highLight>().hover = false;
        correctAnswer.GetComponent<highLight>().highLightThis();
    }

    public void Reset()
    {
        Debug.Log("Reseting!");
        if(randomCorrectAnswer==true) //set it, other wise make it what we chose
            correctAnswer = GetRandomCorrectAnswer(Bones);
        choices[0] = correctAnswer.name;
        int chosenBone = 0;
        for(int i = 1; i<choices.Count;i++){
            int randVal = Random.Range(0,Bones.Count);
            if(Bones[randVal]==correctAnswer){
                randVal = (randVal + 1) % (Bones.Count);
            }
            string randValStr = Bones[randVal].name;
            choices[i] = randValStr;
            Bones.RemoveAt(randVal); //
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
    }
    
    
}