using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class dice3D : MonoBehaviour
{
    float rotationSpeed = 10.0f;
    bool isRolling = false;
    
   
    // Start is called before the first frame update
    void Start()
    {
        isRolling = true;
    }



    private void OnMouseDown()
    {
        if (isRolling)
        {
            if (gameObject.CompareTag("dice3D1") && playerMovement.Instance.chance)
            {
                Debug.Log("Dice 1 ");
                //playerMovement.Instance.playerTurn = true;
                StartCoroutine(RotateDice());
            }

            if (gameObject.CompareTag("dice3D2") && playerMovement.Instance.chance )
            {
                Debug.Log("Dice 2");
                //playerMovement.Instance.playerTurn = false;
                StartCoroutine(RotateDice());
            }



        }

    }
    IEnumerator RotateDice()
    {
        Vector3 initialPosition = transform.position;  // Store the initial position

        playerMovement.Instance.diceSound.GetComponent<AudioSource>().Play();

        isRolling = false;

        for (int i = 0; i < 10; i++)  // Increase this value for more rotations
        {
            Quaternion from = transform.rotation;
           
            Quaternion to = Quaternion.Euler(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0,360), UnityEngine.Random.Range(0 ,360));

            for (float t = 0; t < 1; t += Time.deltaTime / 0.05f)  // Adjust this value to control the speed of each rotation
            {
                transform.rotation = Quaternion.Lerp(from, to, t);
                transform.position += new Vector3(0, 1f, 0);  // Add a small value to the y position
                yield return null;
            }
        }

        // After the dice has finished rotating, adjust the rotation so that one face is perfectly upwards
        Vector3 eulerRotation = transform.rotation.eulerAngles;

        eulerRotation.x = Mathf.Round(eulerRotation.x / 90) * 90;
        eulerRotation.y = Mathf.Round(eulerRotation.y / 90) * 90;
        eulerRotation.z = Mathf.Round(eulerRotation.z / 90) * 90;

        transform.rotation = Quaternion.Euler(eulerRotation);

        transform.position = initialPosition;  // Set the position back to the initial position

        int finalSide =  GetDiceCount();

        yield return new WaitForSeconds(0.5f);
        Debug.Log(finalSide);
        playerMovement.Instance.diceRoll(finalSide);
        isRolling = true;


    }

   
    int GetDiceCount()
    {
        int diceCount = 0;

        if (Vector3.Dot(transform.forward, Vector3.up) >= 0.5)
            diceCount = 3;
        else if (Vector3.Dot(-transform.forward, Vector3.up) >= 0.5)
            diceCount = 4;
        else if (Vector3.Dot(transform.up, Vector3.up) >= 0.5)
            diceCount = 2;
        else if (Vector3.Dot(-transform.up, Vector3.up) >= 0.5)
            diceCount = 5;
        else if (Vector3.Dot(transform.right, Vector3.up) >= 0.5)
            diceCount = 6;
        else if (Vector3.Dot(-transform.right, Vector3.up) >= 0.5)
            diceCount = 1;

        return diceCount;
    }

    
}
