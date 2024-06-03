using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Vector3 moveDirection;
    public float speed = 10;
    Rigidbody rb;
    bool isgrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        moveDirection *= speed;
        moveDirection *= Time.deltaTime;

        transform.Translate(moveDirection);

        if (Input.GetKeyDown(KeyCode.Space) && isgrounded)
        {
            print("jumping");
            rb.AddForce(new Vector3(0, 1, 0) * speed * Time.deltaTime, ForceMode.Impulse);
            isgrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Check if Space key is pressed
        {
            GetComponent<Rigidbody>().AddTorque(Random.rotation.eulerAngles * 500); // Apply random rotation
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isgrounded = true;
        }

    }
}


