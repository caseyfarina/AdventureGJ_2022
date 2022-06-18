using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shipHealth : MonoBehaviour
{

    public int health = 2;
    public string enemyTag = "enemy";

    public GameObject shipDestroyEffect;

    public GameObject shipDamageEffect;

    public GameObject healthDisplay;

    public GameObject GameOverDisplay;
    public GameObject FaceDisplayAlive;
    public GameObject FaceDisplayDead;

    TextMeshProUGUI healthDisplayTextMesh;

    void Start()
    {
        if(healthDisplay != null)
        {
            healthDisplayTextMesh = healthDisplay.GetComponent<TextMeshProUGUI>();

            healthDisplayTextMesh.text = health.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == enemyTag)
        {
            //decrease health if collision with enemy
            health = health - 1;

            //update UI
            if (healthDisplay != null) { healthDisplayTextMesh.text = health.ToString(); }


            //create a damage effect if not destroyed
            if (shipDamageEffect != null)
            {
                Instantiate(shipDamageEffect, transform.position, transform.rotation);
            }


            //Has the ship run out of health?
            if (health <= 0)
            {
                


                //turn on game over message
                if (GameOverDisplay != null)
                {
                    FaceDisplayAlive.SetActive(false);
                    GameOverDisplay.SetActive(true);
                    FaceDisplayDead.SetActive(true);

                }


                //create an explosion effect on destruction
                if (shipDestroyEffect != null)
                {
                    Instantiate(shipDestroyEffect, transform.position, transform.rotation);


                }

                //this code destroys the gameobject that it is attached to
                Destroy(gameObject);
               // gameObject.SetActive(false);
               

            }

            
        }
    }
}
