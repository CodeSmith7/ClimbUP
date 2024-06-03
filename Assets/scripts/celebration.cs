using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class celebration : MonoBehaviour
{
    public ParticleSystem[] particle;

    //public static celebration instance;


    private void Awake()
    {
        playerMovement.Instance.celeb = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            PlayConfettiCelebration();
        }
    }


    public void PlayConfettiCelebration()
    {
       
        foreach (ParticleSystem item in particle)
        {
            item.gameObject.SetActive(true);
            item.Play();
        }
    }
}
