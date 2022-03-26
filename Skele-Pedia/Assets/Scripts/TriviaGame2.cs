using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using System;
using UnityEngine.EventSystems;

public class TriviaGame2 : MonoBehaviour
{
    public bool randomCorrectAnswer = true;
    public int i = 0; //index
    
    public List<GameObject> Bones; // new List<GameObject>();
    public List<GameObject> correctBones = new List<GameObject>();
    public List<GameObject> temp = new List<GameObject>(); 
    //public int length;
    

    public List<Button> options = new List<Button>();
    public List<Text> optionsText;
    public List<Transform> optionsTransforms = new List<Transform>();
    
    public GameObject correctAnswer;
    public List<string> choices = new List<string>();
    
    public TextMeshProUGUI QuestionText;
    public int clicked;
    public string clickedString;

    public bool displaySingleBone = true;
    public bool highlightSingleBone = true;

    private GameObject boneOnDisplay;
    public Button nextQuestionButton;

    public bool gameEnd = false;

    EventSystem eventSystem; //to get which button was clicked

    public GameObject animationCorrect; 

    public Animator anim;
    public GameObject correctLogo1; //displayed when the player gets something right

    int streak = 0;

    public GameObject scoreAnim;
    public int score = 0;
    public GameObject multiplierText;

    int removedBones = 0;

    void Start()
    {
        /* foreach (Transform child in skeleton.transform)
        {
            if (true){
                //if the bone is not in the list
                Bones.Add(child.GetComponent<GameObject>());
            }
        } */

        /* foreach (Button button in FindObjectsOfType<Button>())
        {
            //options button
            if(button.GetComponent<Text>().text.Contains("Option")){
                options.Add(button); // add options button
                optionsText.Add(button.GetComponent<Text>());
                optionsTransforms.Add(button.GetComponent<Transform>());
                choices.Add(button.GetComponent<Text>().text);
            }
            else if(button.GetComponent<Text>().text.Contains("Next Question")){
                Button nextQuestion = button; //not an option, but a next question button instead
                nextQuestionButton = nextQuestion; //
            }
        }
        

        

        for(int i=0; i<options.Count;i++){
            optionsText[i] = options[i].GetComponent<Text>();
        }
        */



        copyBones();
        Reset();
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

    public void highlight(){
        correctAnswer.AddComponent<highLight>();
        correctAnswer.GetComponent<highLight>().hover = false;
        correctAnswer.GetComponent<highLight>().highLightThis();
    }

    public void Reset()
    {
        removedBones++;
        Debug.Log("Reseting!");
        int correctAnswerIndex = Random.Range(0,correctBones.Count - removedBones);
        Debug.Log("Correct Answer index"+correctAnswerIndex);
        correctAnswer = correctBones[correctAnswerIndex]; //gets random correct bones```  `
        //remove from correct bones
        correctBones.RemoveAt(correctAnswerIndex); //
        //temp = correctBones.ToList();
        

        choices[0] = correctAnswer.name; //multiple choice first answer = correct answer (later randomized)
        for(int i = 1; i<choices.Count;i++){
            int randVal = Random.Range(0,Bones.Count - removedBones);
            string randValStr = temp[randVal].name;
            choices[i] = randValStr;
            //temp.RemoveAt(randVal);   
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
        
        //make the new correct answer button get adjusted
    }

    public void randomizeButtons(){
        
        Vector3 temp;
        for (int i = 0; i < optionsTransforms.Count; i++) {
             int rnd = Random.Range(0, optionsTransforms.Count); //random number created 
             temp = optionsTransforms[rnd].position; //we set the currently,
             optionsTransforms[rnd].position = optionsTransforms[i].position;
             optionsTransforms[i].position = temp;
         }
    }

    public void checkIfCorrect(){
        string clickedName = clickedString;
        Debug.Log("Checking if " +clickedName+ " is the correct answer.");
        
        string correctAnswerString = correctAnswer.name;

        if(correctAnswerString == clickedName){
            Debug.Log("That is a correct answer!");
            streak++;
            
            sayCorrect();
            //Invoke("Reset",3f); //regardless, go to the next question after 2 seconds
        }
        else{
            Debug.Log("Wrong: clickedString: "+ clickedString+ " correctAnswerString: "+correctAnswerString );
            anim.Play("wrong");
            streak = 0;

        }

        

    } 
    void sayCorrect(){
        //animationCorrect.gameObject.SetActive(true);
        int addedScore = 10*Random.Range(1,4)*streak;
        score = score+addedScore;
        anim.Play("correct");
        scoreAnim.GetComponent<Animator>().Play("scale");
        string scorePlusScoreString = addedScore.ToString() +"+"+ score.ToString();
        scoreAnim.GetComponent<TextMeshProUGUI>().text = scorePlusScoreString;
        Invoke("updateScoreText",2f);

        multiplierText.GetComponent<TextMeshProUGUI>().text = "x"+(streak+1).ToString();
        multiplierText.GetComponent<Animator>().Play("show");

        //Invoke("Reset",3f);
    }

    void updateScoreText(){
        scoreAnim.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
    
    
}
