using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadExcel : MonoBehaviour
{
    public question blankQuestion;
    public List<question> questionDatabase = new List<question>();

    public void LoadItemData(){
        questionDatabase.Clear();
        List<Dictionary<string,object>> data = csvReader.Read("questionDatabase");
        for(var i= 0; i< data.Count; i++){
            string question = data[i]["questionString"].ToString();
            string correctAnswer = data[i]["correctAnswer"].ToString();
            string incorrectAnswer1 = data[i]["incorrectAnswer1"].ToString();
            string incorrectAnswer2 = data[i]["incorrectAnswer2"].ToString();
            string incorrectAnswer3 = data[i]["incorrectAnswer3"].ToString();
        
            AddItem(question, correctAnswer, incorrectAnswer1, incorrectAnswer2, incorrectAnswer3);
        }
    }

    void AddItem(string questionString, string correctAnswer, string incorrectAnswer1, string incorrectAnswer2, string incorrectAnswer3){
        question tempQuestion = new question(blankQuestion);

        tempQuestion.questionString = questionString;
        tempQuestion.correctAnswer = correctAnswer;
        tempQuestion.incorrectAnswer1 = incorrectAnswer1;
        tempQuestion.incorrectAnswer2 = incorrectAnswer2;
        tempQuestion.incorrectAnswer3 = incorrectAnswer3;

        questionDatabase.Add(tempQuestion);
        // reference
        // https://www.youtube.com/watch?v=C37C2yCUlCM
    }

}
