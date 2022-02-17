using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private float time = 5f;

    public List<GameObject> startList;

    public TMP_Text instruction;
    // Start is called before the first frame update
    void Start()
    {
       
    } 

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            time = 0;
            foreach (var obj in startList)
            {
                obj.SetActive(false);
            }
            instruction.gameObject.SetActive(true);
            Debug.Log("Times up");
        }
    }
}
