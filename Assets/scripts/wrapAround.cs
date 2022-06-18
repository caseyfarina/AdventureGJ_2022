using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrapAround : MonoBehaviour
{
    public float xWrapAround = 25f;
    public float yWrapAround = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > xWrapAround )
        {
            transform.position = new Vector3(-xWrapAround, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -xWrapAround)
        {
            transform.position = new Vector3(xWrapAround, transform.position.y, transform.position.z);
        }

        if (transform.position.y > yWrapAround)
        {
            transform.position = new Vector3(transform.position.x, -yWrapAround, transform.position.z);
        }

        if (transform.position.y < -yWrapAround)
        {
            transform.position = new Vector3(transform.position.x,yWrapAround, transform.position.z);
        }

    }
}
