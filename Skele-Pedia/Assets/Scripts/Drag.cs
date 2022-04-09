using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private float _distance;

    private bool _drag;

    private Vector3 _offset;

    private Transform _toDrag;

    //public Transform ribCageTransfrom;
    public GameObject objectClicked;
    public Vector3 origPosition = new Vector3(0f, 0f, 0f);
    Vector3 correctPosition = new Vector3(0f, 0f, 0f);
    public Vector3 offset = new Vector3(0f, 0f, 0f);
    
    void Start()
    {
        origPosition = transform.position;
        //ribCageTransfrom = GameObject.Find("Ribcage").transform;
    }
   
    // Update is called once per frame
    void Update()
    {
        Vector3 vect3;

        if (Input.touchCount != 1)
        {
            _drag = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
            
            //if the touch phase has begun
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Pelvis") || hit.collider.CompareTag("Humerus")
                || hit.collider.CompareTag("Skull") || hit.collider.CompareTag("Ribcage"))
                {
                    _toDrag = hit.transform;
                    _distance = hit.transform.position.z - Camera.main.transform.position.z; //was z not x
                    vect3 = new Vector3(pos.x, pos.y, _distance);
                    vect3 = Camera.main.ScreenToWorldPoint(vect3);
                    _offset = _toDrag.position - vect3;
                    _drag = true;
                }
            }
        }

        if (_drag && touch.phase == TouchPhase.Moved)
        {
            vect3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _distance);
            vect3 = Camera.main.ScreenToWorldPoint(vect3);
            _toDrag.position = vect3 + _offset;
        }

        if (_drag && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            _drag = false;
            
        }
    }
    
    //create a collider script
    //when drag element has collided with cube, then
    //snap into place at Vector.zero

    public void OnTriggerEnter(Collider other)
    {
        objectClicked = gameObject;
        if (other.CompareTag("Ribcage"))
        {
            Debug.Log(other.name +"Trigger was hit");
            Debug.Log(other.GetComponent<Collider>().name);
            Debug.Log(other.GetComponent<Collider>().tag);

            
            objectClicked.transform.localPosition = correctPosition + offset; //subtracts to balance it back into place

            //transform.position = new Vector3(0, 5, 0);
        }
        
    }
}
