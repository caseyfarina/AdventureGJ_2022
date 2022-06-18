using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linearRotation : MonoBehaviour
{
    [Header("Initial State")]
    public bool toggleActive = true;
 
    [Space(10)] // 10 pixels of spacing here.
    [Header("Rotation Speed Per Axis")]
    [Range(-500, 500f)]
    public float xRotation = 90f;
    [Range(-500, 500f)]
    public float yRotation = 0f;
    [Range(-500, 500f)]
    public float zRotation = 0f;

    [Header("Percentage of Randomness")]
    [Range(0f, 1f)]
    public float randomPercentage = .1f;
    private Vector3 rotationVector;
  
    

    // Start is called before the first frame update
    void Start()
    {

        rotationVector = new Vector3(
                xRotation + Random.Range(-(xRotation * randomPercentage), (xRotation * randomPercentage)),
                yRotation + Random.Range(-(yRotation * randomPercentage), (yRotation * randomPercentage)),
                zRotation + Random.Range(-(zRotation * randomPercentage), (zRotation * randomPercentage))
                );

    }



    void Update()
    {
        if (toggleActive)
        {
            transform.Rotate(rotationVector * Time.deltaTime);
        }

    }

}
