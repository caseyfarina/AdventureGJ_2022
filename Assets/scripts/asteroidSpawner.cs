using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidSpawner : MonoBehaviour
{
    public GameObject[]asteroidPrefabs;
    public float asteroidRate = 5f;
    public float xPosition = 50f;
    public float yPosition = 35f;
    public Vector3 spawnPosition; 
    // Start is called before the first frame update
    void Start()
    {
        Invoke("spawnAsteroid", asteroidRate);
    }

    // Update is called once per frame
    void Update()
    {
                    
    }
  void spawnAsteroid()
    {
        Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0f);
        float randomChoice = Random.Range(0, 100f);
        if(randomChoice < 35f) 
        {
            spawnPosition = new Vector3(xPosition, yPosition, 0f);
        }
        if(randomChoice > 35f || randomChoice < 50f)
        {
            spawnPosition = new Vector3(-xPosition, yPosition, 0f);
        }
        if (randomChoice > 35f || randomChoice < 75f)
        {
            spawnPosition = new Vector3(xPosition, -yPosition, 0f);
        }
        if (randomChoice > 75f)
        {
            spawnPosition = new Vector3(-xPosition, -yPosition, 0f);
        }
        //randomly select a prefab
        int prefabNumber = Random.Range(0, asteroidPrefabs.Length);
        //spawn asteroid
        Instantiate(asteroidPrefabs[prefabNumber], spawnPosition, transform.rotation);
        //make another asteroid
        if(gameObject.activeSelf == true)
        {
            Invoke("spawnAsteroid", asteroidRate);
        }
    }
 }

