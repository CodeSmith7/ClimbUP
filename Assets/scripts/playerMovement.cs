using JetBrains.Annotations;
using System.Collections;
using UnityEditor;
using System;



using System.Collections.Generic;
using System.Reflection;
using TMPro;

using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using UnityEngine.Audio;


public class playerMovement : MonoBehaviour
{
    public static playerMovement Instance;
    public bool isGameStart = false;
    public int myTurn;
    public int currentTurn;
   

    public GameObject player1;
    public GameObject player2;

    int player1Pos = 0;
    int player2Pos = 0;


    int randomval;
    int randomval2; 
    public bool chance = true;
    public int _turn = 1;
    public float moveSpeed = 50.00f;

    private int[] ladders = { 19, 7, 9, 55, 69, 51, 37, 43, 61 };
    private int[] snakes = { 25, 41, 59, 82, 33, 67, 95, 91, 99 };

    private int[] laddersTarget = { 38,26,46,74,88,93,62,94,97};
    private int[] snakesTarget = { 3,4,21,60,11,32,74,47,10};

    public GameObject[] snakeTail;
    public GameObject[] ladderTop;
    

    public GameObject dice1;
    public GameObject dice2;

    bool p1 = false;
    bool p2 = false;

    public bool playerTurn = true;


    public GameObject AllSoundParent;
    public GameObject diceSound;
    public GameObject ladderSound;
    public GameObject snakeSound;
    public GameObject backgroundSound;
    public GameObject winning;
    public GameObject error;
    public GameObject openSound;
    public GameObject jumpSound;


    public GameObject restart;
    public GameObject quit;

    bool isopenP1;
    bool isopenP2;

    //SCRIPTS
    public celebration celeb;
    public UIScript uiscript;

   

    //lights 
    public GameObject light;

    //accross scene 
    public static bool isSoundON = true;
    public static int colorP1;          // 0 -> yellow, 1-> red, 2-> blue, 3 ->  green 
    public static int colorP2;

    //player color material 
    public Material[] playerColorMaterial;
    public GameObject player1Color;
    public GameObject player2Color;

    //dice Background
    public GameObject diceBGL;
    public GameObject diceBGR;

    //  public AudioMixer audi;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        _turn = 1;
        isopenP1 = false;
        isopenP2 = false;
        currentTurn = _turn;
        setColorPlayer();
        
        Time.timeScale = 1;
        if (isSoundON)
        {
            AllSoundParent.SetActive(true);
        } 
        else
        {
            AllSoundParent.SetActive(false);

        }

        setColorPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (_turn == 1 )
        {
            dice1.SetActive(true);
            dice2.SetActive(false);
            diceBGL.SetActive(true);
            diceBGR.SetActive(false);


        }
        else if (_turn == 2)
        {
            dice1.SetActive(false);
            dice2.SetActive(true);
            diceBGL.SetActive(false);
            diceBGR.SetActive(true);
        }

        if (player1Pos == 100 || player2Pos == 100)
        {
            dice1.SetActive(false);
            dice2.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            //Debug.Log("P1  : " + player1.transform.position + "  P2 :  " + player2.transform.position);
            Debug.Log("tunr is : " + _turn);

            Debug.Log(" Color choose is : " + colorP1);
            Debug.Log("audio is ON  :" + isSoundON);
            playerMovement.Instance.uiscript.winUI();
        }


    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("restart"))
        {
            SceneManager.LoadScene(0);
            Debug.Log("Inside Restart");
        }

        if (gameObject.CompareTag("quit"))
        {
            Application.Quit();
        }
    }

    IEnumerator movePlayer(int randomVal)
    {
        
        if (_turn == 1  && (player1Pos + randomVal <= 100))
        {
            chance = false;
             
            if (isopenP1 == false && randomVal == 6)
            {
                player1Pos = 1;
                isopenP1 = true;
                openSound.GetComponent<AudioSource>().Play();
                Debug.Log("inside open p1");
                player1.transform.position = new Vector3(player1.transform.position.x + 100, player1.transform.position.y, player1.transform.position.z);

            }
            else
            {
                if (isopenP1 == true)
                {
                    for (int i = 0; i < randomVal; i++)
                    {
                        if (player1Pos == 0)
                        {
                            player1.transform.position = new Vector3(player1.transform.position.x + 100, player1.transform.position.y, player1.transform.position.z);
                        }
                        else
                        {
                            if (player1Pos % 10 == 0 && player1Pos != 100)
                            {
                                player1.transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, player1.transform.position.z + 100);
                            }
                            else if ((player1Pos / 10) % 2 == 0)
                            {
                                player1.transform.position = new Vector3(player1.transform.position.x + 100, player1.transform.position.y, player1.transform.position.z);
                            }
                            else if ((player1Pos / 10) % 2 == 1)
                            {
                                player1.transform.position = new Vector3(player1.transform.position.x - 100, player1.transform.position.y, player1.transform.position.z);
                            }

                        }


                        jumpSound.GetComponent<AudioSource>().Play();

                        yield return new WaitForSeconds(0.2f);
                        player1Pos++;

                    }

                    checkSnake(player1Pos);
                    checkLadder(player1Pos);


                    if (player1Pos == 100)
                    {
                        light.SetActive(true);
                        Debug.Log("Player 1 WON");
                        winning.GetComponent<AudioSource>().Play();
                        StartCoroutine("celebrationLast");
                        
                        playerMovement.Instance.uiscript.won1.SetActive(true);
                        playerMovement.Instance.uiscript.playerChance.gameObject.SetActive(false);

                    }

                    if (player1Pos == player2Pos)
                    {
                        player2Pos = 0;
                        snakeSound.GetComponent<AudioSource>().Play();
                        isopenP2 = false;
                        player2.transform.position = new Vector3(-50f, 33.6f, 50f);
                    }
                }
                 
            }

            chance = true;

            playerMovement.Instance.uiscript.playerChance.SetText("PLAYER 2");
            playerMovement.Instance.uiscript.playerChance.color = Color.red;

            _turn = 2;
        }
        else if (_turn == 2 && (player2Pos + randomVal <= 100))
        {
            chance = false;
            
            if (isopenP2 == false && randomVal == 6)
            {
                openSound.GetComponent<AudioSource>().Play();

                player2.transform.position = new Vector3(player2.transform.position.x + 100, player2.transform.position.y, player2.transform.position.z);
                isopenP2 = true;
                player2Pos = 1;
                Debug.Log("inside open p2");
            }
            else
            {
                if (isopenP2 == true)
                {
                    for (int i = 0; i < randomVal; i++)
                    {
                        if (player2Pos == 0)
                        {
                            player2.transform.position = new Vector3(player2.transform.position.x + 100, player2.transform.position.y, player2.transform.position.z);
                        }
                        else
                        {
                            if (player2Pos % 10 == 0 && player2Pos != 100)
                            {
                                player2.transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, player2.transform.position.z + 100);
                            }
                            else if ((player2Pos / 10) % 2 == 0)
                            {
                                player2.transform.position = new Vector3(player2.transform.position.x + 100, player2.transform.position.y, player2.transform.position.z);
                            }
                            else if ((player2Pos / 10) % 2 == 1)
                            {
                                player2.transform.position = new Vector3(player2.transform.position.x - 100, player2.transform.position.y, player2.transform.position.z);
                            }
                        }

                        jumpSound.GetComponent<AudioSource>().Play();
                        yield return new WaitForSeconds(0.2f);
                        player2Pos++;

                    }

                    checkLadder(player2Pos);
                    checkSnake(player2Pos);

                    if (player2Pos == 100)
                    {
                        light.SetActive(true);

                        Debug.Log("Player 2 WON");
                        playerMovement.Instance.uiscript.playerChance.gameObject.SetActive(false);
                        playerMovement.Instance.uiscript.won2.SetActive(true);
                        winning.GetComponent<AudioSource>().Play();
                        StartCoroutine(celebrationLast());

                    }

                    if (player1Pos == player2Pos)
                    {
                        player1Pos = 0;
                        snakeSound.GetComponent<AudioSource>().Play();
                        isopenP1 = false;
                        player1.transform.position = new Vector3(-50f, 33.6f, 50f);
                    }
                }
                
            }

            chance = true;
            playerMovement.Instance.uiscript.playerChance.SetText("PLAYER 1");
            playerMovement.Instance.uiscript.playerChance.color = Color.blue;
            _turn = 1;
        }
        else 
        {
            error.GetComponent<AudioSource>().Play();
            _turn = _turn == 1 ? 2 : 1;
        }

        Debug.Log("P1  " + player1Pos + "    " + "P2  "+ player2Pos);
    }



    //function to check the snake at that position
    void checkSnake(int pos)
    {
        int temp = -1;
        for (int i = 0; i < snakes.Length; i++) 
        {
            if (pos == snakes[i])
            {
                temp = i; break;
            } 
        }

        if (temp != -1)
        {
            snakeSound.GetComponent<AudioSource>().Play();
;            if (_turn == 1)
            {
                player1.transform.position = Vector3.Lerp(player1.transform.position,snakeTail[temp].transform.position,1f);
               
                /* StartCoroutine(MoveTo(snakeTail[temp].transform.position, player1, () =>
                 {
                     player1.transform.position = snakeTail[temp].transform.position;
                 }));*/

               // StartCoroutine(MoveTo(snakeTail[temp].transform.position, player1.transform.position    ));
               //StartCoroutine(moveObject(player1, snakeTail[temp].transform.position));

                player1Pos = snakesTarget[temp];

            }
            else if (_turn == 2)
            {
                 player2.transform.position = Vector3.Lerp(player2.transform.position, snakeTail[temp].transform.position,1f);
                /*    StartCoroutine(MoveTo(snakeTail[temp].transform.position, player2, () =>
                    {
                        player2.transform.position = snakeTail[temp].transform.position;
                    }));*/

               // StartCoroutine(MoveTo(snakeTail[temp].transform.position, player2.transform.position ));

                player2Pos = snakesTarget[temp];    
            }

            temp = -1;
        }
    }

    //function to check the ladder at that position
    void checkLadder(int pos) 
    {
        int temp = -1;

        for (int i = 0; i < ladders.Length; i++) 
        {
            if (pos == ladders[i])
            {
                temp = i; break;
            }

        }

        if (temp != -1)
        {
            ladderSound.GetComponent<AudioSource>().Play();
            
            if (_turn == 1)
            {
                player1.transform.position = Vector3.Lerp(player1.transform.position, ladderTop[temp].transform.position, 1f);
                
                /* StartCoroutine(MoveTo(ladderTop[temp].transform.position, player1, () =>
                 {
                     player1.transform.position = ladderTop[temp].transform.position;
                 }));
                 */

                //StartCoroutine(MoveTo(ladderTop[temp].transform.position,player1.transform.position ));

                player1Pos = laddersTarget[temp];

            }
            else if (_turn == 2)
            {
                player2.transform.position = Vector3.Lerp(player2.transform.position,ladderTop[temp].transform.position, 1f);
               
                /* StartCoroutine(MoveTo(ladderTop[temp].transform.position, player2, () =>
                 {
                     player2.transform.position = ladderTop[temp].transform.position;
                 }));*/
               // StartCoroutine(MoveTo(ladderTop[temp].transform.position, player2.transform.position    ));
               
                player2Pos = laddersTarget[temp];
            }
            temp = -1;
        }
    }

    IEnumerator MoveTo(Vector3 targetPosition, Vector3 startPosition)
    {
        int turn = _turn;
        float distance = Vector3.Distance(startPosition, targetPosition);

        float duration = distance / moveSpeed;

        // Calculate the start time
        float startTime = Time.time;

        // While the elapsed time is less than the duration, move the player towards the target position
        while (Time.time - startTime < duration)
        {
            // Calculate the interpolation factor
            float t = (Time.time - startTime) / duration;

            // Calculate the new position using linear interpolation
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, Time.deltaTime);

            // Move the player to the new position
            if (turn == 1)
            {
                player1.transform.position = newPosition;
            }

            else if (_turn == 2)
            {
                player2.transform.position = newPosition;
            }

            // Wait for the next frame
            yield return null;
        }
        
        if (_turn == 1)
        {
            player1.transform.position = targetPosition;
        }
        else if (_turn == 2)
        {
            player2.transform.position = targetPosition;
        }
    }

    public void diceRoll(int val)
    {
        if (chance )
        {
            if (_turn == 1)
            {
                randomval = val;
                p1 = true;
            }
            else if (_turn == 2)
            {
                randomval2 = val;
                p2 = true;
            }

            if (p1 == true)
            {
                p1 = false;
                StartCoroutine(movePlayer(randomval));
            }
            else if (p2 == true)
            {
                p2 = false;
                StartCoroutine(movePlayer(randomval2));    
            }
        }
    }
    private IEnumerator celebrationLast()
    {
        dice1.SetActive(false);
        dice2.SetActive(false);

        // celebration.instance.PlayConfettiCelebration();
        celeb.PlayConfettiCelebration();
        
        yield return new WaitForSeconds(3f);
        playerMovement.Instance.uiscript.winUI();


        playerMovement.Instance.uiscript.winUI();
        

        Time.timeScale = 0;
    }

    void setColorPlayer()
    {
        Debug.Log(" Color P1 : " + colorP1 + "  color P2  :  " + colorP2);

        //if both of them have same color then one choose different color S
        if (colorP1 == colorP2)
        {
            colorP2 = (colorP1 + 1) % 4;
        }

        //setting color for player1
        player1Color.GetComponent<Renderer>().material = playerColorMaterial[colorP1];

        //setting color for the player2 
        player2Color.GetComponent<Renderer>().material = playerColorMaterial[colorP2];
    }
    
}


