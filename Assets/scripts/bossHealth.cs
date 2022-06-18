using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bossHealth : MonoBehaviour
{

    public int health = 2;
    public int scoreValue = 1;
    public string scoreKeeperTagName = "scoreKeeper";
    public string tagName = "laser";
    scoreKeeper scorekeeper;
    GameObject scoreNumberDisplay;


    public GameObject shipDestroyEffect;

    public GameObject shipDamageEffect;



    public GameObject YouWinDisplay;

    //TextMeshProUGUI healthDisplayTextMesh;

   // public GameObject backgroundMusic;

    void Start()
    {
    }

    void Awake()
    {     
        scoreNumberDisplay = GameObject.FindWithTag("scoreKeeper");
        scorekeeper = scoreNumberDisplay.GetComponent<scoreKeeper>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagName)
        {
            //decrease health if collision with enemy
            health = health - 1;

           


            //create a damage effect if not destroyed
            if (shipDamageEffect != null)
            {
                Instantiate(shipDamageEffect, transform.position, transform.rotation);
            }


            //Has the ship run out of health?
            if (health <= 0)
            {
             //turn on game over message
                if (YouWinDisplay != null)
                {
                    YouWinDisplay.SetActive(true);

                }


                //create an explosion effect on destruction
                if (shipDestroyEffect != null)
                {
                    Instantiate(shipDestroyEffect, transform.position, transform.rotation);


                }

                if (scoreNumberDisplay != null)
                {
                    //increase score
                    scorekeeper.startingScore = scorekeeper.startingScore + scoreValue;

                }
                // Destroy(backgroundMusic);
                //this code destroys the gameobject that it is attached to
                Destroy(gameObject);

            }


        }
    }
}
 
