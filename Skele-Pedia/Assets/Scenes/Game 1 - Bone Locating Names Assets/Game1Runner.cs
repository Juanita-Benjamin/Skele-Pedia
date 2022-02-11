using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; //You need to include this on your script, it doesn't come by default but
//^What it does is it lets you access all the UI Features, text, buttons, images on the screen etc.

public class Game1Runner : MonoBehaviour
{

    public bool AR = false;

    
    public Vector3[] cameraPositions = new Vector3[5]; //an array of camera locations
    public Quaternion[] rotations = new Quaternion[5];
    public int i = 0; //iterator for i : i++ means go to next next camera
    public string[] regionNames = new string[5];

    public Camera mainCamera;

    public float turnSpeed = 40.0f;
    public float moveSpeed = 20.0f;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    private float rotX;
    
    public bool escaped = false;

    //Have to manually set these in the inspector if anything goes wrong
    public Text TextBox; //Text Box for the Region we are locate in
    public Animator animator; //animator for textBox to fade in and out
    public Text QuestionTextBox; //Question Asker
    public GameObject skeleton;
    public string[] Questions; //bone names, to be used for the questions, at this time
    int j = 0; //iterator, used to index through the possible options for questions


    public string currentBone; //holds which bone you're looking at
    public bool showAnswers = true;
    public Text temporaryAnswerTextBox; //remove later, once you for sure have it done. 
    public string currentQuestion;

    float timer = 0f;
    public int countDown = 5;
    public Text CountDownTextBox;

    bool correct = false; //if your answer was correct

    void Start(){
        if(AR==false){
            Screen.lockCursor = true;
        }
        
        cameraPositions  = new [] { 
            new Vector3(0f, 2f, -8f),
            new Vector3(2.5f,2.5f,-1.7f),
            new Vector3(1f,2f,-2f),
            new Vector3(-1.08f,1.01f,-1.7f),
            new Vector3(0f,3f,2f)
        };
        regionNames  = new [] { 
            "Full View",
            "Upper Arm Region",
            "Middle Body Area",
            "Lower Leg Region",
            "Upper Back Area",
        };

        rotations  = new [] { 
            Quaternion.Euler(10f, 0f, 0f),
            Quaternion.Euler(50f,293.06f,0f),
            Quaternion.Euler(2f,-20,0f),
            Quaternion.Euler(0f,17.923f,0f),
            Quaternion.Euler(29f,180f,0f)
        };


        animator = TextBox.GetComponent<Animator>(); //for the fade out animation
        
        ChangeCameras(0);
        StartRound();
    }

    void FixedUpdate ()
    {
        if(Input.GetKeyDown("escape")){
            //switch escape on and off. By making boolean negatve of self when pressed
            if(escaped==true){
                escaped = false;
            } 
            else{
                escaped = true;
            }
        }
        if(AR==false){
            FPSControls(); //simulating AR when not available
            if (Input.GetMouseButtonDown(0)){
                Debug.Log("Pressed primary button. Moving the camera");
                ChangeCameras(i); //i = the iterator. i++ means next camera.
            }
                
        }
        if(countDown>=0)
            CountDown(Time.deltaTime); //Time.deltaTime - the sliver of time that past since the last Update
    }

    void FPSControls(){
        if(escaped==false){
            MouseAiming();
            KeyboardMovement();
        }
        
    }
    void MouseAiming ()
    {
        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        // rotate the camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
    }
    void KeyboardMovement ()
    {
        Vector3 dir = new Vector3(0, 0, 0);
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
    

    void ChangeCameras(int current){
        
        if(current>=cameraPositions.Length){
            i = 0;
        }
        Vector3 currentCamera = cameraPositions[i]; //getting current camera position indecies
        Quaternion currentRotation = rotations[i]; //Quaterion = rotation. getting current rotation of cameras from indecies
        transform.position = currentCamera; //setting the position of the main camera 
        transform.rotation = currentRotation; //setting the rotations of the main camera 

        SetText(i); //change text to new bone region

        i++;

        
    }

    void SetText(int current){ //plays animation that reveals text and says what that region is
        string currentRegion;
        currentRegion = regionNames[current];
        TextBox.text = currentRegion;
        animator.Play("FadeOut"); //makes the UI Animator know turn the text on
    }

    void StartRound(){
        int numberOfChildren = skeleton.transform.childCount;
        Questions = new string[numberOfChildren];

        for (int i = 0; i < numberOfChildren; ++i){
             Questions[i] =  skeleton.transform.GetChild(i).name;
        }

        string Question = PickRandomQuestion(Questions);
        currentQuestion = SpiceItUp(Question);
        QuestionTextBox.text = currentQuestion;
    }
    string PickRandomQuestion(string[] Questions){
        int randomChoice = Random.Range(0, Questions.Length); //picks a random number between 1 and 19
        
        string chosenQuestion = Questions[randomChoice];
        return chosenQuestion;
    }
    string SpiceItUp(string Question){
        int numberOfVariations = 4;
        int rand = Random.Range(0,numberOfVariations);

        if(rand == 0){ //the bone in question is located where?
            return "The "+ Question + " is located where?";
        }
        if(rand == 1){ 
            return "In what part of the Skeleton is "+ Question + " located?";
        }
        else if(rand == 2){ 
            return "Where can we find the "+ Question + "?";
        }
        else if(rand == 3){ 
            return "Which of these bones is the "+ Question + "?";
        }
        else{
            return "The "+ Question + " is located where?";
        }
        return "The "+ Question + " is located where?";
        
    }
    public void SetCurrentBone(string name){
        currentBone = name;

        //This line: line 168 - comment this line out once the full thing is done
        if(currentBone == currentQuestion ){
            temporaryAnswerTextBox.text = "CORRECT! That is the "+ currentBone+"!";

            correct = true;
        }
        else{
            temporaryAnswerTextBox.text = "Current Bone: "+ currentBone;
            correct = false;
        }
    }
    void CountDown(float timePast){
        timer+=timePast;
        if(timer>1f){
            countDown--;
            timer = 0;
        }
        CountDownTextBox.text = countDown.ToString();
        if(countDown<=0){
            if(correct){
                CountDownTextBox.text = "CORRECT!";
                
            }
            else{
                CountDownTextBox.text = "Times\nUp!";
                
            }
                
        }
    }

}
