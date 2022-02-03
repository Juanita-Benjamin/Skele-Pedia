using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wigglable : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void OnMouseDown(){
        anim.Play("WiggleAround");
    }
}
