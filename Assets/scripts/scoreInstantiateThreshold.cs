using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreInstantiateThreshold : MonoBehaviour
{

    public GameObject scoreKeeperObject;
    public GameObject prefabToInstantiate;

    public int enableThreshold = 50;
   

    private scoreKeeper thisScoreKeeper;

    private bool offSwitch = true;


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

            if (prefabToInstantiate != null && offSwitch)
            {
                Instantiate(prefabToInstantiate, transform.position, transform.rotation);
                //objectToInstantiate.SetActive(true);

            }

            offSwitch = false;
        }

        

    }
}
