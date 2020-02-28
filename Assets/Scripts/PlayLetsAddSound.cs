using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLetsAddSound : MonoBehaviour
{
    public AudioClip borgSound;
    public AudioClip letsAddSound;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(playLetsAddSound());
	}

    IEnumerator playLetsAddSound()
    {
        yield return new WaitForSeconds(borgSound.length + 1.0f);
        GetComponent<AudioSource>().PlayOneShot(letsAddSound);
    }
}
