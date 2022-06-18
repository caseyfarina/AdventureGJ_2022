using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class shipHealthBar : MonoBehaviour
{

    public float health = 2f;
    public float colorChangeThreshold = 2f;
    public string enemyTag = "enemy";

    public GameObject shipDestroyEffect;

    public GameObject shipDamageEffect;

    public GameObject healthBarDisplay;

    public GameObject GameOverDisplay;

    TextMeshProUGUI healthDisplayTextMesh;

    RectTransform rectTransform;

    Image healthImage;

    public Color healthColorHealthy;
    public Color healthColorDanger;

    public float healthBarScaler = .5f;
    public GameObject FaceDisplayAlive;
    public GameObject FaceDisplayDead;


    void Start()
    {
        if(healthBarDisplay != null)
        {
            rectTransform = healthBarDisplay.GetComponent<RectTransform>();
            healthImage = healthBarDisplay.GetComponent<Image>();


            //change color of the bar
            healthImage.color = healthColorHealthy;

            //set the bar to the correct size
            rectTransform.localScale = new Vector3(health * healthBarScaler, rectTransform.localScale.y, rectTransform.localScale.z);

            //healthDisplayTextMesh.text = health.ToString();
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

            
            if( health <= colorChangeThreshold)
            {
                healthImage.color = healthColorDanger;
            }

            


            //update UI
            if (healthBarDisplay != null) { rectTransform.localScale = new Vector3(health * healthBarScaler, rectTransform.localScale.y, rectTransform.localScale.z); }


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
                    FaceDisplayDead.SetActive(true);
                    GameOverDisplay.SetActive(true);

                }


                //create an explosion effect on destruction
                if (shipDestroyEffect != null)
                {
                    Instantiate(shipDestroyEffect, transform.position, transform.rotation);


                }

                //this code destroys the gameobject that it is attached to
                Destroy(gameObject);

            }

            
        }
    }
}
