using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class triviaGameAnswButton : MonoBehaviour
{

    bool playingTriviaGame1 = true;
    public Text thisButtonText;
    public string thisButtonName;
    public TriviaGame2 triviaGame; //change it later to one, clickedString gets edited

    void Start(){
        thisButtonName = thisButtonText.GetComponent<Text>().text; 
    }

    public void OnClick(){
        thisButtonName = thisButtonText.GetComponent<Text>().text; //send the text of this object to the main script
        triviaGame.clickedString = thisButtonName;
        Debug.Log(thisButtonName);
    }

}
