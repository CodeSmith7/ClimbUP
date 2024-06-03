using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class camMovement : MonoBehaviour
{
    public Animator animator;

    public Camera cam;


    private void Awake()
    {
        animator = GetComponent<Animator>();        
    }
    // Start is called before the first frame update
    void Start()
    {
        playerMovement.Instance.backgroundSound.GetComponent<AudioSource>().Play();     
        StartCoroutine(initCam());
    }

    
    IEnumerator initCam()
    {
        playerMovement.Instance.uiscript.playerChance.gameObject.SetActive(false);
        playerMovement.Instance.uiscript.exitButton.gameObject.SetActive(false);    

        animator.CrossFade("camMoving", 0);
        
        yield return new WaitForSeconds(4f);
        playerMovement.Instance.uiscript.title.gameObject.SetActive(false);

        yield return new WaitForSeconds(7f);
        playerMovement.Instance.uiscript.begin.gameObject.SetActive(true);


        cam.orthographic = true;

        yield return new WaitForSeconds(2f);
        playerMovement.Instance.uiscript.begin.gameObject.SetActive(false);


        animator.CrossFade("cameraIdle", 0);
        playerMovement.Instance.uiscript.playerChance.gameObject.SetActive(true);


        playerMovement.Instance.uiscript.exitButton.gameObject.SetActive(true);



        playerMovement.Instance.isGameStart = true;


        
    }
}
