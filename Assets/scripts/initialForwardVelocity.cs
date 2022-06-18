using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialForwardVelocity : MonoBehaviour
{
    Rigidbody2D thisBody;
    public float initialForwardThrust = 500f;
    // Start is called before the first frame update
    void Awake()
    {
        thisBody = GetComponent<Rigidbody2D>();

        thisBody.AddForce(transform.up * initialForwardThrust);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
