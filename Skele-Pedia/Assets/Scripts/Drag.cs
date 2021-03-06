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

    public string theOneItsSupposedToBe;
    
    void Start()
    {
        
        
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
                //if (hit.collider.CompareTag("Pelvis") || hit.collider.CompareTag("Humerus")
                //|| hit.collider.CompareTag("Skull") || hit.collider.CompareTag("Ribcage"))
                if(hit.collider.CompareTag(theOneItsSupposedToBe))
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
    
   

}
