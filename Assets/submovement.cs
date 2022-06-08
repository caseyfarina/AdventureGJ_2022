using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class submovement : MonoBehaviour
{
    [Header("Propulsion Controls")]
    public float upThrust = 3f;
    public float turnThrust = 4f;
    public float forwardThrustValue = 5f;
    //public GameObject fin;
    public float finRotation = -45f;
    


    Vector3 forwardThrust;
  
    
    //public GameObject propeller;
    public Vector3 propellerRotationSpeed = new Vector3(0, 1, 0);
    Rigidbody thisBody;
    Animator thisAnimator;
   
    // Start is called before the first frame update
    void Start()
    {
        thisBody = transform.GetComponent < Rigidbody > ();
        thisAnimator = transform.GetComponent<Animator>();

        forwardThrust = new Vector3(0, 0, forwardThrustValue);
    }

    // Update is called once per frame
    void Update()
    {
        thisBody.AddForce(new Vector3(0, Input.GetAxis("Vertical") * upThrust, 0));
        Debug.Log(Input.GetAxis("Horizontal"));
        float tempfinRotation = Mathf.SmoothStep(-finRotation, finRotation, Mathf.InverseLerp(-1,1,Input.GetAxis("Horizontal")));
        Debug.Log("lerp " + finRotation);
        //fin.transform.localEulerAngles = new Vector3(0f, tempfinRotation, 0f);

        /*

        if (Input.GetAxis("Horizontal") < 0)
        {
            thisAnimator.Play("leftTurn");
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            thisAnimator.Play("rightTurn");
        }
        */

        float h = Input.GetAxis("Horizontal") * turnThrust * Time.deltaTime;
        thisBody.AddTorque(transform.up *h, 0);

        if (Input.GetKey(KeyCode.Space))
        {
            thisBody.AddRelativeForce(forwardThrust);


            /*
            // Vector3 smoothRotation = new Vector3(Vector3.Lerp())
            if (propeller != null)
            {
                propeller.transform.Rotate(propellerRotationSpeed);
            }
            */

        }
        
        //THIS CONVERTS A BOOL TO AN INT
       // int smoothRotationInt = Input.GetKey(KeyCode.Space) ? 1 : 0;
        //float smoothRotation = (float)smoothRotationInt;

        
        

    }
    

}
