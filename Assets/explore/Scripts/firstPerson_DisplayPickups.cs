using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class firstPerson_DisplayPickups : MonoBehaviour
{

    public GameObject playerCapsule;
    firstPerson_Pickup FirstPerson_Pickup;
    TextMeshProUGUI textMeshProUGUI;


    // Start is called before the first frame update
    void Start()
    {
        if(playerCapsule != null)
        {

            FirstPerson_Pickup = playerCapsule.GetComponent<firstPerson_Pickup>();         
        }
        
      
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            

    }

    // Update is called once per frame
    void Update()
    {
        textMeshProUGUI.text = FirstPerson_Pickup.remainingPickups.ToString();


    }
}
