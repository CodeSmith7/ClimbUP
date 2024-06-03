using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuUIScript : MonoBehaviour
{
    public GameObject start;
    public GameObject setting;
    public GameObject exit;
    public GameObject exitPopUP;
    public GameObject settingPopUP;
    public GameObject colorChoose;
    public GameObject colorChoose2;
    public GameObject colorChooseTitle;
    public GameObject colorChooseStart;
    public GameObject audioToggler;
    public GameObject backToMenu;
    public GameObject bg;

    //Outline 
    public GameObject[] colorBorder;
    public GameObject[] colorBorderP2;

    //sprites
    public Sprite exitsprite;
    public Sprite settingSprite;
    public Sprite mainmenu;
    public Sprite startSprite;

    static bool isFirstLoad = true;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement.isSoundON = true;

        if (isFirstLoad)
        {
            StartCoroutine(firstScreen());
            isFirstLoad = false;    
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        colorPicker();
    }
    public void back() 
    {
        bg.GetComponent<Image>().sprite = mainmenu;
        start.SetActive(true);
        exit.SetActive(true);
        setting.SetActive(true);
        exitPopUP.SetActive(false);
        settingPopUP.SetActive(false);
        colorChoose.SetActive(false);
        colorChoose2.SetActive(false);
        colorChooseStart.SetActive(false);
        colorChooseTitle.SetActive(false);
        backToMenu.SetActive(false);
    }
    public void exitGame()
    {
        bg.GetComponent<Image>().sprite = exitsprite;
        start.SetActive(false);    
        exit.SetActive(false); 
        setting.SetActive(false);
        colorChoose.SetActive(false);
        backToMenu.SetActive(false);
        exitPopUP.SetActive(true);
    }
    public void gameSetting()
    {
        bg.GetComponent<Image>().sprite = settingSprite;
        start.SetActive(false);
        exit.SetActive(false);
        setting.SetActive(false);
        colorChoose.SetActive(false);
        backToMenu.SetActive(true);
        settingPopUP.SetActive(true);
    }

    public void yesQuit()
    {
        Application.Quit();
    }
    public void noQuit()
    {
        back();
    }


    //for Player P1
    public void colorY()
    {
        playerMovement.colorP1 = 0;
        displayBorder(0);

    }

    public void colorR()
    {
        playerMovement.colorP1 = 1;
        displayBorder(1);
    }

    public void colorB()
    {
        playerMovement.colorP1 = 2;
        displayBorder(2);
    }
    public void colorG()
    {
        playerMovement.colorP1 = 3;
        displayBorder(3);
    }


    //For Player P2
    public void colorYP2()
    {
        playerMovement.colorP2 = 0;
        displayBorderP2(0);

    }

    public void colorRP2()
    {
        playerMovement.colorP2 = 1;
        displayBorderP2(1);
    }

    public void colorBP2()
    {
        playerMovement.colorP2 = 2;
        displayBorderP2(2);
    }
    public void colorGP2()
    {
        playerMovement.colorP2 = 3;
        displayBorderP2(3);
    }
    void displayBorder(int color) 
    {
        Debug.Log("Inside the displayBorder");
        for (int i = 0; i < 4;i++)
        {
            if (i == color)
            {
                colorBorder[i].SetActive(true);
            } 
            else
            {
                colorBorder[i].SetActive(false);
            }
        }
    }

    void displayBorderP2(int color)
    {
        Debug.Log("Inside the displayBorder");
        for (int i = 0; i < 4; i++)
        {
            if (i == color)
            {
                colorBorderP2[i].SetActive(true);
            }
            else
            {
                colorBorderP2[i].SetActive(false);
            }
        }
    }
    public void audioChanger()
    {
        Debug.Log("Inside Audio changer");
        if(playerMovement.isSoundON)
        {
            audioToggler.SetActive(false);
            playerMovement.isSoundON = false;
            Debug.Log("True Sound");
        }
        else
        {
            audioToggler.SetActive(true);
            playerMovement.isSoundON = true;
            Debug.Log("false Sound");
        }
    }

    IEnumerator firstScreen()
    {
        bg.GetComponent<Image>().sprite = startSprite;
        start.SetActive(false);
        exit.SetActive(false);
        setting.SetActive(false);
        backToMenu.SetActive(false);
        
        yield return new WaitForSeconds(3f);

        bg.GetComponent<Image>().sprite = mainmenu;
        start.SetActive(true);
        exit.SetActive(true);
        setting.SetActive(true);
        backToMenu.SetActive(false);
    }

    public void colorPicker()
    {
        exit.SetActive(false);
        start.SetActive(false);
        setting.SetActive(false);
        colorChoose.SetActive(true);
        colorChoose2.SetActive(true);
        colorChooseStart.SetActive(true);
        colorChooseTitle.SetActive(true);
    }
    public void colorStart()
    {
        SceneManager.LoadScene(1);
    }
}
