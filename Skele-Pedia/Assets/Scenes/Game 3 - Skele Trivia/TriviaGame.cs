using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriviaGame : MonoBehaviour
{
    public int i = 0; //index
    public GameObject[] Bones;
    //public int length;

    public Button[] options = new Button[4];
    public Text[] optionsText;


    public bool randomCorrectAnswer = true;
    public GameObject correctAnswer;
    public string[] choices = new string[4];

    public Text QuestionText;
    public int clicked;

    void Start(){

        if(randomCorrectAnswer==true) //set it, other wise make it what we chose
            correctAnswer = GetRandomCorrectAnswer(Bones);
        correctAnswer.AddComponent<highLight>();
        correctAnswer.GetComponent<highLight>().hover = false;
        correctAnswer.GetComponent<highLight>().highLightThis();
        Debug.Log(Bones[0].name);
        choices[0] = correctAnswer.name;
        Debug.Log(choices[0]);
        int chosenBone = 0;
        for(int i = 1; i<choices.Length;i++){
            int randVal = Random.Range(0,Bones.Length);
            if(Bones[randVal]==correctAnswer || Bones[randVal].name == "blank"){
                randVal = (randVal + 1) % (Bones.Length);
            }
            
            string randValStr = Bones[randVal].name;
            choices[i] = randValStr;
            Debug.Log(choices[i]);
            Bones[randVal].name="blank";
            
        }
        loadText();
        //displayBone();
    }


    void loadText(){
        for(int i=0; i<choices.Length;i++){
            optionsText[i].text = choices[i];
        }
    }

    void displayBone(){
        GameObject boneOnDisplay = Instantiate(correctAnswer,transform.position, Quaternion.identity);
        //boneOnDisplay.transform.localScale = new Vector3(5f,5f,5f); //make it twice as big as it normally would be
    }

    void displayCorrect(){
        
    }

    public GameObject GetRandomCorrectAnswer(GameObject[] Bones){
        return Bones[Random.Range(0,Bones.Length)];
    }
    
    
}
