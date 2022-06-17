using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TriggerMusic : MonoBehaviour
{ 
    public AudioClip newTrack;
    private AudioManager theAM;

    void Start()
    {
        theAM = FindObjectOfType<AudioManager>();
    }
    
    void Update ()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (newTrack != null)
                theAM.ChangeBGM(newTrack);
        }
    }
    
}

