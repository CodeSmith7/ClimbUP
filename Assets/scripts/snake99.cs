using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake99 : MonoBehaviour
{
    public float speed = 1.0f;
    public float amplitude = 1.0f;
    public float frequency = 1.0f;
   
    private Vector3 startPos;
    private float noiseSeed;

    void Start()
    {
        startPos = transform.position;
        noiseSeed = Random.value * 100;
    }

    void Update()
    {
        float z = amplitude * (Mathf.PerlinNoise(noiseSeed, Time.time * speed) - 0.5f) * 2.0f;
        transform.position = startPos + new Vector3(0, 0, z * frequency);
    }

   
 }
