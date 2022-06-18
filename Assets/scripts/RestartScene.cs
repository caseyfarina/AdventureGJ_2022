using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartScene : MonoBehaviour
{

    public KeyCode restartKey = KeyCode.R;
    void Update()
    {
        if (Input.GetKey(restartKey))
        {
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}