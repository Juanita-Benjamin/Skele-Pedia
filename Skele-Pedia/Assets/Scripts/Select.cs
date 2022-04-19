using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    public List<GameObject> skeletalpieces; //created a list of gameobjects for the skeleton
    public List<GameObject> descriptions; //created a list of gameobjects for the description of the skeleton
    public GameObject descriptionPanel; //this references UI in the game. It displays the description text and color.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Highlight();
    }

    void Highlight()
    {
        //if the touchCount is more than 0, and the phase (in which you're touching)
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            //then send  raycast from the camera to the position of the touch
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit; //this is the "information" that will be received back

            //if there is a hit
            if (Physics.Raycast(ray, out hit))
            {
                //loop through the list of skeletal bones
                for (int i = 0; i < skeletalpieces.Count; i++)
                {
                    //if the object we touched name = the skeletal bone
                    if (hit.collider.gameObject.name == skeletalpieces[i].gameObject.name)
                    {
                        //then activate the outline script to true.
                        hit.collider.gameObject.GetComponent<Outline>().enabled = true;

                    }
                    //loop through the description list
                    for (int k = 0; k < descriptions.Count; k++)
                    {
                        //if the object we touched name = the description object
                        if (hit.collider.gameObject.name == descriptions[k].gameObject.name)
                        {
                            //then set the UI panel to true
                            descriptionPanel.SetActive(true);
                            descriptions[i].SetActive(true); //set that gameobject active
                        }

                        else
                        {
                            //set the gameobject false
                            descriptions[k].SetActive(false);
                        }
                    }
                }
                Debug.Log(hit.collider.name);
            }

            
           
        }
    }
}
