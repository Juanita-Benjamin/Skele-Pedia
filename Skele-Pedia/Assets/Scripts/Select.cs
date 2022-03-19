using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    public List<GameObject> skeletalpieces;
    public List<GameObject> newpieces;

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

                for (int i = 0; i < skeletalpieces.Count; i++)
                {
                    if (hit.collider.gameObject.name == skeletalpieces[i].gameObject.name)
                    {
                        hit.collider.gameObject.GetComponent<Outline>().enabled = true;
                        newpieces.Add(hit.collider.gameObject);
                        skeletalpieces.Remove(hit.collider.gameObject);
                       
                    }
                    else
                    {
                        // foreach (var piece in newpieces)
                        // {
                        //     piece.GetComponent<Outline>().enabled = false;
                        // }
                    }
                }
                // if (hit.collider != null)
                // {
                //     hit.collider.GetComponent<Outline>().enabled = true;
                // }
                Debug.Log(hit.collider.name);
            }
        }
    }
}
