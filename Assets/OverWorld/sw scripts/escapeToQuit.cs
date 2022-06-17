using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Quits the player when the user hits escape

public class escapeToQuit : MonoBehaviour
{
	public float restartHeight = -10f;
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

	
    }
}
