using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreKeeper : MonoBehaviour
{

    public int startingScore = 0;

    TextMeshProUGUI scoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        scoreDisplay = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = startingScore.ToString();
    }
}

