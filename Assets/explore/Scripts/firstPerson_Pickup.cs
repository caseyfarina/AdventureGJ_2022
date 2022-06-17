using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class firstPerson_Pickup : MonoBehaviour
{
    Rigidbody body;
    firstPerson_ScoreKeeper scoreKeeper;
    public GameObject pickupEffect;
    public GameObject scoreObject;
    public int totalPickups = 10;
    public float pickUpVolume = .5f;
    public int currentPickups = 0;
    public int remainingPickups = 0;
    public GameObject winMessage;
   
   


    // Start is called before the first frame update
    void Start()
    {
       
        currentPickups = 0;
        remainingPickups = totalPickups - currentPickups;

        body = GetComponent<Rigidbody>();
        if (scoreObject != null) {
            scoreKeeper = GetComponent<firstPerson_ScoreKeeper>();
                }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collided)
    {
        if (collided.gameObject.tag == "PickUp")
        {
            collided.gameObject.SetActive(false);

            currentPickups += 1;
            remainingPickups = totalPickups - currentPickups;
            



            if (pickupEffect != null)
            {             
                Instantiate(pickupEffect, transform.position, Quaternion.identity);             
            }

            if(scoreObject != null)
            {
                scoreKeeper.IncrementScore();
            }        
            
            if(currentPickups >= totalPickups)
            {
                winMessage.SetActive(true);
                Invoke("mainScene", 4);

            }
        }
    }

    void mainScene()
    {
        SceneManager.LoadScene(0);
    }
}
