using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipControl : MonoBehaviour
{
    //public variable of the forward thrust
    public float forwardThrust = 200f;
    //public variable of the backward thrust
    public float backwardThrust = 102f;
    //public variable of the angular thrust
    public float AngularThrust = 10;
    // set aside space for the rigidbody and name it this thisBody 
    Rigidbody2D thisBody; 
    // Start is called before the first frame update
    void Start()
    {
        //grab a reference to this current Rigidbody2D
        thisBody = GetComponent<Rigidbody2D>();     
    }

    // Update is called once per frame
    void Update()
    {
        //push the ship forward if you press the w key
        if (Input.GetKeyDown(KeyCode.W))
        { 
            print("w key was pressed");
            //f= float, int= integrar, adds forwardthrust into the ship
            thisBody.AddForce(transform.up * forwardThrust);
        }
        //push the ship backward if you press the s key
        if (Input.GetKeyDown(KeyCode.S))
        { 
            print("s key was pressed");
        //f= float, int= integrar, adds backwardthrust into the ship
        thisBody.AddForce(-transform.up*backwardThrust);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //adds forward thrust to the ship
            thisBody.AddTorque(AngularThrust, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //adds forward thrust to the ship
            thisBody.AddTorque(-AngularThrust, ForceMode2D.Impulse);
        }
    }
}
