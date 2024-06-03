using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


public class player : MonoBehaviour
{
    public float speed = 10;
    public float jumpForce = 10;
    Vector3 pos;
    Vector3 pos1;
    int randomval;
    public TMP_Text dice;
    bool chance = true;
    public GameObject won;
    Rigidbody rb;

    int player1Pos = 1;

    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            float direction = Input.GetKeyDown(KeyCode.RightArrow) ? 100f : -100f;
            pos = new Vector3(transform.position.x + direction, transform.position.y, transform.position.z);
            //pos.x = Mathf.Clamp(pos.x, 50f, 950f);
            transform.position = pos;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            float direction = Input.GetKeyDown(KeyCode.UpArrow) ? 100f : -100f;
            pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + direction);
           // pos.z = Mathf.Clamp(pos.z, 50f, 950f);
            transform.position = pos;
        }

        if (Input.GetKeyDown(KeyCode.R)  && chance )
        {
            randomval = Random.Range(1, 7);

            dice.SetText("Dice : " + randomval.ToString());
            Debug.Log("value is : " + randomval);
            if ( player1Pos + randomval  <= 100)
            {
                StartCoroutine(movePlayer(randomval));

            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("ffff" + player1Pos / 10);
            Debug.Log("PLayer poition value is : ->  " + player1Pos);
        }

        

    }
   
    
    IEnumerator movePlayer(int randomVal)
    {
        chance = false;

        for (int i = 0; i < randomVal; i++)
        {
          
            if (player1Pos%10 == 0  && player1Pos != 100)
            {
                pos1 = new Vector3(transform.position.x, transform.position.y, transform.position.z + 100);
            } 
            else if ( (player1Pos / 10) % 2 == 0) 
            {
                pos1 = new Vector3(transform.position.x + 100, transform.position.y, transform.position.z);
            }
            else if ((player1Pos / 10) % 2 == 1)
            {
                pos1 = new Vector3(transform.position.x - 100, transform.position.y, transform.position.z);
            }
            

            transform.position = pos1;
            
            
            yield return new WaitForSeconds(0.5f);
            player1Pos++;
            Debug.Log("palyer position ->  " + player1Pos);
        }
        if (player1Pos == 100)
        {
            Debug.Log("Player 1 WON");
            won.SetActive(true);
        }
        chance = true;
    }
    public void diceROll()
    {
        if (chance )
        {
            randomval = Random.Range(1, 7);

            dice.SetText("Dice : " + randomval.ToString());
            Debug.Log("value is : " + randomval);
            if (player1Pos + randomval <= 100)
            {
                StartCoroutine(movePlayer(randomval));

            }
        }
    }
}
