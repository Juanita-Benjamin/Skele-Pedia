using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questions : MonoBehaviour
{

    public static int X, Y;
    [System.Serializable]
    public class question
    {
        public string[] rows = new string[Y];
    }

    public question[] questionList = new question[X];
}
