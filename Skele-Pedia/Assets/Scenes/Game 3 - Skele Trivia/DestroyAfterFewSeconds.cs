using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterFewSeconds : MonoBehaviour
{
    // Start is called before the first frame update
    public int howManySeconds = 2;

    void Start()
    {
        Destroy(gameObject,howManySeconds);   
    }

}
