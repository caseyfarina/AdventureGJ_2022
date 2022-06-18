using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intermittentForwardVelocity : MonoBehaviour
{
    Rigidbody2D thisBody;
    public float forwardThrustPower = 500f;
    public float timeInterval = 2f;
    // Start is called before the first frame update
    void Awake()
    {
        thisBody = GetComponent<Rigidbody2D>();

        InvokeRepeating("forwardThrust", timeInterval, timeInterval);


        thisBody.AddForce(transform.up * forwardThrustPower);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void forwardThrust()
    {
        thisBody.AddForce(transform.up * forwardThrustPower);

        Debug.Log("push");
    }
}
