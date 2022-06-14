using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadSceneOnTrigger : MonoBehaviour
{

    public string sceneName;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //int thisBuildIndex = loadScene.buildIndex;
        SceneManager.LoadScene(sceneName);
    }

    void loadThisScene()
    {

    }
}
