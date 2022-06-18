using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleSpawner : MonoBehaviour
{
    //this will make a field to add the spaw object
    public GameObject[] spawnThings;

    public float spawnRateMin = 1f;
    public float spawnRateMax = 5f;
    
    void Start()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        float wait_time = Random.Range(spawnRateMin, spawnRateMax);
        yield return new WaitForSeconds(wait_time);
        spawnObject();
        StartCoroutine(waiter());
    }


    void spawnObject()
    {
        //choose a random spawn from the spawn list
        int spawnNumber = (int)Mathf.Floor(Random.Range(0, spawnThings.Length));
        //this is the code for making a new object at the position of this spawnpoint
        Instantiate(spawnThings[spawnNumber], transform.position, transform.rotation);
    }


}
