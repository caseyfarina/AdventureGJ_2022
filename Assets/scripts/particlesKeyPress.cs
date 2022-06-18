using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlesKeyPress : MonoBehaviour
{

    public KeyCode thisKey = KeyCode.Space;
    ParticleSystem thispart;
    public int numberOfParticlesToEmit = 100;

    public int burstLength = 20;
    private int originalBurstLength;

    // Start is called before the first frame update
    void Awake()
    {
        thispart = GetComponent<ParticleSystem>();

        originalBurstLength = burstLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(thisKey))
        {
            burstLength = 0;


       
        }

        if(burstLength < originalBurstLength)
        {
            thispart.Emit(numberOfParticlesToEmit);
            burstLength++;
        }
       
        

    }
}
