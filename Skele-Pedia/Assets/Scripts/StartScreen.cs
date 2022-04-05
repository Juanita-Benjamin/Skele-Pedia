using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private float time = 5f;

    public GameObject welcomePanel;
    public GameObject NamePrompt;
    
    void Start()
    {
       
    }
    void Update()
    {
        
        time -= Time.deltaTime;

        if (time <= 0)
        {
            time = 0;
            welcomePanel.gameObject.SetActive(true);
            Debug.Log("Times up");
        }
        
    }

    public void NewGame()
    {
        welcomePanel.SetActive(false);
        NamePrompt.SetActive(true);
    }
}
