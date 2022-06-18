using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class destroyOnTrigger : MonoBehaviour
{

    public string tagName = "enemy";

    public string scoreKeeperTagName = "scoreKeeper";

    public int scoreValue = 1;

    GameObject scoreNumberDisplay;

    scoreKeeper scorekeeper;
    //for explisions and sounds once destroyed 
    public GameObject destroyEffect;

    // Start is called before the first frame update
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
            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
            }


            if (scoreNumberDisplay != null)
            {
                //increase score
                scorekeeper.startingScore = scorekeeper.startingScore + scoreValue;

            }

            //this code destroys the gameobject that it is attached to
            Destroy(gameObject);
        }
    }
    /*
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == tagName)
        {
            if(destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
            }

            //this code destroys the gameobject that it is attached to
            Destroy(gameObject);
        }
    }
    */
}