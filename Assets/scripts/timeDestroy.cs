using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeDestroy : MonoBehaviour
{
    
    public float lifeTimeMin = 10f;
    public float lifeTimeMax = 12f;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke a function after a certain delay time
        Invoke("cleanUp", Random.Range(lifeTimeMin, lifeTimeMax));
    }

    void cleanUp()
    {
        //this code destroys the gameobject that it is attached to
        Destroy(gameObject);
    }
}
