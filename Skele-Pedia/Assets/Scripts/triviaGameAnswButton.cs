using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class triviaGameAnswButton : MonoBehaviour
{

    bool playingTriviaGame1 = true;
    public TextMeshProUGUI thisButtonText;
    public string thisButtonName;
    public TriviaGame2 triviaGame; //change it later to one, clickedString gets edited

    void Start(){
        thisButtonName = thisButtonText.GetComponent<TextMeshProUGUI>().text; 
    }

    public void OnClick(){
        thisButtonName = thisButtonText.GetComponent<TextMeshProUGUI>().text; //send the text of this object to the main script
        triviaGame.clickedString = thisButtonName;
        Debug.Log(thisButtonName);
    }

}
