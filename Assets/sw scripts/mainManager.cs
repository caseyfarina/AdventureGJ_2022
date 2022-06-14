using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static mainManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
