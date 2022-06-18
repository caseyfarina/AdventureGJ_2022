using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialRandomVelocity : MonoBehaviour
{
    Rigidbody2D thisBody;
    public float thrustMin = 10f;
    public float thrustMax = 200f;
    // Start is called before the first frame update
    void Awake()
    {
        thisBody = GetComponent<Rigidbody2D>();

        thisBody.AddForce(
            new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
                )
            * Random.Range(
                thrustMin,
                thrustMax
                )
            );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
