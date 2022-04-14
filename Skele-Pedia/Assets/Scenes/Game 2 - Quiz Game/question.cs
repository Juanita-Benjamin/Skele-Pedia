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
    
    public int timesCorrect; //useful to have both, for different reasons
    public int timesIncorrect; //we want incorrects, when we want the users to know which ones he needs to work on
    //we want the correct ones when we want to get a tally of the questions they got correct in this round
    
    //ideally timesCorrect only goes up for that round, but timesIncorrect would be permanently recorded
}
