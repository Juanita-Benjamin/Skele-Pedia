using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceSkeleton : MonoBehaviour
{
    public GameObject skeleton;

    private GameObject spawnedObj; //represent the object we are using

    private ARRaycastManager _arRaycastManager;

    private Vector2 _touchPosition;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryTouch(out Vector2 touchPos)
    {
        if (Input.touchCount > 0) //if there is touch count
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }

        touchPos = default;
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryTouch(out Vector2 touchPos))
        {
            return;
            //shoot the raycast manager
            //Takes touch position, hits(list above), trackableType: a feature in the physical environment that a
            //device is able to track, such as a plane
            //PlaneWithinPolygon: The boundary of a BoundedPlane
            
        }
        if (_arRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitpos = hits[0].pose; //get the first hit
            //check whether there is a spawned object
            //if there is none, Instantiate it

            if (spawnedObj == null)
            {
                spawnedObj = Instantiate(skeleton, hitpos.position, hitpos.rotation);
                spawnedObj.SetActive(true);
            }
            
            //To move around - Not sure we need this (PROBABLY NOT)
            else
            {
                spawnedObj.transform.position = hitpos.position;
            }
        }
    }
}
