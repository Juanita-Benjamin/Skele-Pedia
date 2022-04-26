using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  TMPro;
public class PlaceSkeleton : MonoBehaviour
{
    public int score;

    public TMP_Text scoretext;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public string theTagThatThisObjectCanGet;
    // Update is called once per frame
   
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag(theTagThatThisObjectCanGet))
        {
            //objectClicked = other.gameObject; 
            Debug.Log(other.name +"will snap to:" + gameObject);
            other.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            Destroy(other.GetComponent<Drag>());

            score += 10;
            scoretext.text = score.ToString();
        }
        
        
    }
}
