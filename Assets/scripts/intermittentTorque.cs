using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intermittentTorque: MonoBehaviour
{
    Rigidbody2D thisBody;
    public float angularThrust = 4f;
    public float timeInterval = 2f;
    // Start is called before the first frame update
    void Awake()
    {
        thisBody = GetComponent<Rigidbody2D>();

        InvokeRepeating("angularTorque", timeInterval, timeInterval);


        if (Random.Range(0, 100) < 50)
        {
            angularThrust = angularThrust * -1f;
        }

        thisBody.AddTorque(angularThrust, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void angularTorque()
    {
        //thisBody.AddForce(transform.up * forwardThrustPower);

        if (Random.Range(0, 100) < 50)
        {
            angularThrust = angularThrust * -1f;
        }

        thisBody.AddTorque(angularThrust, ForceMode2D.Impulse);
    }
}
