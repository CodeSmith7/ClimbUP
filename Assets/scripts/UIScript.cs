using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    //public static UIScript Instance;
    public TMP_Text dice1;
    public TMP_Text playerChance;
    public GameObject Player1Dice;
   
    public GameObject won1;
    public GameObject won2;

    public GameObject restartButton;
    public GameObject exitButton;
    public GameObject pause; 
    public GameObject title;
    public GameObject begin;
    public GameObject exitPopUP;
    public GameObject winPopUP;

    //UInew 
    public GameObject UIBackground;

    


    //Sprite
    public Sprite exitSprite;
    public Sprite WinSprite;

    private void Awake()
    {
        playerMovement.Instance.uiscript = this;
        title.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void restart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(0);
    }
    public void quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
    
    public void pauseUI()
    {
        exitButton.SetActive(true);
        
    }

    public void exitUI ()
    {
       UIBackground.GetComponent<Image>().sprite = exitSprite;
       UIBackground.SetActive(true);
       exitPopUP.SetActive(true);
      
    }
    public void exitYes()
    {
        // Application.Quit();
        SceneManager.LoadScene(0);
        Debug.Log("Exit Yes Called");
    }

    public void exitNo()
    {
        UIBackground.SetActive(false);
        exitPopUP.SetActive(false);
    }

    public void winUI() 
    {
        UIBackground.GetComponent<Image>().sprite = WinSprite;
        UIBackground.SetActive(true);

        winPopUP.SetActive(true);
       
    }
    public void retry ()
    {
        SceneManager.LoadScene(1);
    }
    public void home()
    {
        SceneManager.LoadScene(0);
    }

    
}
