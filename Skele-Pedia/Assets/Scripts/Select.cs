using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Select : MonoBehaviour
{
    public List<GameObject> skeletalpieces;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Highlight();
      
      
        //To Do:
        //1: Get name of gameobject
        //2: When you click on the object, and if the object matches the name 
        //in the list then, highlight.
        //3: Otherwise: de-highlight
    }

    void Highlight()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    hit.collider.GetComponent<Outline>().enabled = true;
                    Debug.Log(hit.collider.name);
                }
            }
        }
    }
}
