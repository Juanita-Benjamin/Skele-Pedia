using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    public List<GameObject> skeletalpieces;
    public List<GameObject> descriptions;
    public GameObject descriptionPanel;

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

                    }
                    
                    for (int k = 0; k < descriptions.Count; k++)
                    {
                        if (hit.collider.gameObject.name == descriptions[k].gameObject.name)
                        {
                            descriptionPanel.SetActive(true);
                            descriptions[i].SetActive(true);
                        }

                        else
                        {
                            descriptions[k].SetActive(false);
                        }
                    }
                }
                Debug.Log(hit.collider.name);
            }

            
           
        }
    }
}
