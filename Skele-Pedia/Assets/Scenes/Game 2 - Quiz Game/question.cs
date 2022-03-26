using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class question 
{
    public string questionString;
    public string correctAnswer;
    public string incorrectAnswer1;
    public string incorrectAnswer2;
    public string incorrectAnswer3;

    public question(question d){
        questionString = d.questionString;
        correctAnswer = d.correctAnswer;
        
        incorrectAnswer1 = d.incorrectAnswer1;
        incorrectAnswer2 = d.incorrectAnswer2;
        incorrectAnswer3 = d.incorrectAnswer3;
    }
}
