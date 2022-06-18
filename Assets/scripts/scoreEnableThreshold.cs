using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreEnableThreshold : MonoBehaviour
{

    public GameObject scoreKeeperObject;
    public GameObject asteroidSpawnerObject;

    public int enableThreshold = 50;
    public int disableThreshold = 100;

    private scoreKeeper thisScoreKeeper;


    // Start is called before the first frame update
    void Awake()
    {
        thisScoreKeeper = scoreKeeperObject.GetComponent<scoreKeeper>();

    }

    // Update is called once per frame
    void Update()
    {
        if(thisScoreKeeper.startingScore >= enableThreshold)
        {
            asteroidSpawnerObject.SetActive(true);
        }

        if (thisScoreKeeper.startingScore >= disableThreshold)
        {
            asteroidSpawnerObject.SetActive(false);
        }
        
    }
}
