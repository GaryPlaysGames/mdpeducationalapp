using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSounds : MonoBehaviour
{
    public AudioClip happySound1;
    public AudioClip happySound2;
    public AudioClip happySound3;
    public float timeBetweenSounds;
    private bool playSound;
    private bool firstSound;

    // Use this for initialization
    void Start ()
    {
        playSound = true;
        firstSound = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playSound)
        {
            StartCoroutine(PlaySound());
        }
	}

    IEnumerator PlaySound()
    {
        playSound = false;
        if (firstSound)
        {
            firstSound = false;
            yield return new WaitForSeconds(2);
        }
        float seed = Random.Range(0.0f, 1.0f);
        if (seed <= 0.3f)
        {
            GetComponent<AudioSource>().PlayOneShot(happySound1);
        }
        else if (seed > 0.3f && seed <= 0.6f)
        {
            GetComponent<AudioSource>().PlayOneShot(happySound2);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(happySound3);
        }
        yield return new WaitForSeconds(timeBetweenSounds);
        playSound = true;
    }
}
