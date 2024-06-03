using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crown : MonoBehaviour
{
    public float zRotation;
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, zRotation);
    }
}
