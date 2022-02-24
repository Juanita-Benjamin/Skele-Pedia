using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnBone : MonoBehaviour
{
    bool correctSpot = false;
    public Vector3 originalPosition;

    //bool moving = false;
    //Vector3 destination;
    //float moveBackSpeed = 10f; 
    void OnMouseUp()
    {
        if (correctSpot == false)
        {
            WarpMove(originalPosition); //instant teleport
        }
        Debug.Log("Drag ended!");
    }
    void OnMouseDown()
    {
        originalPosition = transform.position;
    }
    void WarpMove(Vector3 back)
    {
        transform.position = back;
    }
    /*
    void SlowlyMoveTowards(Vector3 newDestination){
        moving = true;
        destination = newDestination;
    }
    void Update(){
        if(moving==true){
            if(transform.position == originalPosition){
                moving = false;
            }else{
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPosition, Time.deltaTime * moveBackSpeed);
            }
        }
    }
    */
}
