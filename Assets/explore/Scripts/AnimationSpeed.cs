using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeed : MonoBehaviour {

    public float AnimationSpeedMin = 1f;
    public float AnimationSpeedMax = 1f;
	
	
	// Use this for initialization
	void Start () {
        GetComponent<Animator>().speed = Random.Range(AnimationSpeedMin, AnimationSpeedMax);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
