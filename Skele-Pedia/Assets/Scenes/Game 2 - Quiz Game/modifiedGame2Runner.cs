using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class modifiedGame2Runner : MonoBehaviour
{
    public int i = 0; //index
    
    public questions qList; // new List<GameObject>();
    

    public List<Button> options = new List<Button>();

    public TextMeshProUGUI QuestionText; //its a text mesh pro text object
    public TextMeshProUGUI correctAnswText;
    public TextMeshProUGUI incorrectAnswText1, incorrectAnswText2, incorrectAnswText3;
    public Animator button1Anim, button2Anim, button3Anim, button4Anim;
    public Animator skeleAnim; 
    public bool clickedCorrect = true;


    public void Reset(){
        int currentQuestion = Random.Range(0,qList.questionList.Length) - removedQuestions;
        string temp = qList.questionList[currentQuestion].rows[0].ToString();
        QuestionText.text = temp;
        string tempCorr = qList.questionList[currentQuestion].rows[1].ToString();
        correctAnswText.text = tempCorr; //temporary correct 
        string inc1 = qList.questionList[currentQuestion].rows[2].ToString();
        string inc2 = qList.questionList[currentQuestion].rows[3].ToString();
        string inc3 = qList.questionList[currentQuestion].rows[4].ToString();
        incorrectAnswText1.text = inc1;
        incorrectAnswText2.text = inc2;
        incorrectAnswText3.text = inc3;
        materializeNewQuestionAnimationPlay();

        if(clickedCorrect){
            skeleAnim.Play("correct");
        }
        else{
            skeleAnim.Play("incorrect");
        }
    }

    void materializeNewQuestionAnimationPlay(){
        button1Anim.Play("materialize");
        button2Anim.Play("materialize");
        button3Anim.Play("materialize");
        button4Anim.Play("materialize");

    }

    public void ClickedCorrect(){
        clickedCorrect = true;
    }
    public void ClickedIncorrect(){
        clickedCorrect = false;
    }

    public List<Text> optionsText; //text object
    public List<Transform> optionsTransforms = new List<Transform>();
    public List<string> choices = new List<string>();

    public GameObject correctAnswer;
    
    
    
    public int clicked;
    public string clickedString;

    public bool displaySingleBone = true;
    public bool highlightSingleBone = true;

    private GameObject boneOnDisplay;
    public Button nextQuestionButton;

    public bool gameEnd = false;

    public GameObject animationCorrect; 

    public Animator anim;
    public GameObject correctLogo1; //displayed when the player gets something right

    int streak = 0;

    public GameObject scoreAnim;
    public int score = 0;
    public GameObject multiplierText;

    int removedQuestions = 0;
/*
    void Start()
    {
        Reset();
    }

    void modifyPosition(Transform boneOnDisplayTransform){
        //trying to fix the center position issue by using gameObject tags that say there the game object center should be
        Vector3 offset = transform.position - boneOnDisplayTransform.Find("center").transform.position; //holds x, y, z values
        Debug.Log(offset);
        offset += new Vector3(0f, 0f, -3f); //also bring it closer to the camera
        //offset += new Vector3(0f, 2f, 0f); //move it a little up
        boneOnDisplayTransform.position = offset;
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

    */
}
