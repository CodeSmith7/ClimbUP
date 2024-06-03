using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mouseDown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (gameObject.CompareTag("restart"))
        {
            Debug.Log("In the start");

            SceneManager.LoadScene(0);
        }
        if (gameObject.CompareTag("quit"))
        {
            //  Application.Quit();
            Debug.Log("In the quit");
        }
    }
}
