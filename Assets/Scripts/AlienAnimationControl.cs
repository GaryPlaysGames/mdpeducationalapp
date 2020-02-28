using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAnimationControl : MonoBehaviour {
    public AudioClip keyboardSound;
    public AudioClip alienWaitingSound;
    // Use this for initialization
    void Start ()
    {
        StartCoroutine(AnimationControl());
    }
	
	IEnumerator AnimationControl()
    {
        GetComponent<AudioSource>().PlayOneShot(keyboardSound);
        GetComponent<AudioSource>().PlayOneShot(alienWaitingSound);
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().speed = 0;
    }
}
