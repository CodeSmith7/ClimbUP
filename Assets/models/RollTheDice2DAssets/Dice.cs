using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dice : MonoBehaviour {

    // Array of dice sides sprites to load from Resources folder
    public Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private SpriteRenderer rend;

    bool isRolling = false;

	// Use this for initialization
	private void Start () {

        // Assign Renderer component
        rend = GetComponent<SpriteRenderer>();
        isRolling = true;

        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        //diceSides = Resources.LoadAll<Sprite>("DiceSides/");
	}
	
    // If you left click over the dice then RollTheDice coroutine is started
    private void OnMouseDown()
    {
        if (isRolling)
        {
            if (gameObject.CompareTag("dice1") && playerMovement.Instance.chance)
            {
                Debug.Log("Dice 1 ");
                playerMovement.Instance.playerTurn = true;
                StartCoroutine("RollTheDice");
            }
            if (gameObject.CompareTag("dice2") && playerMovement.Instance.chance)
            {
                Debug.Log("Dice 2");
                playerMovement.Instance.playerTurn = false;
                StartCoroutine("RollTheDice");
            }

        }
        

        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log("Inside the space");
            StartCoroutine("RollTheDice");
        }

    }
    // Coroutine that rolls the dice
    private IEnumerator RollTheDice()
    {
        int randomDiceSide = 0;   
        int finalSide = 0;
      
        playerMovement.Instance.diceSound.GetComponent<AudioSource>().Play();

        isRolling = false;
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 6);
            

            // Set sprite to upper face of dice from array according to random value
            rend.sprite = diceSides[randomDiceSide];

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }

        
        finalSide = randomDiceSide + 1;
        yield return new WaitForSeconds(0.5f);
        Debug.Log(finalSide);
        playerMovement.Instance.diceRoll(finalSide);
        isRolling = true;
    }
}
