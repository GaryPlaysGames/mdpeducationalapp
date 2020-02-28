using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerColorChange : MonoBehaviour
{
    public AudioClip flowerChangeSound;
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(colorChange());
	}
	
	IEnumerator colorChange()
    {
        yield return new WaitForSeconds(1);
        GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
        GetComponent<AudioSource>().PlayOneShot(flowerChangeSound);
        yield return new WaitForSeconds(1.5f);
        GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
        GetComponent<AudioSource>().PlayOneShot(flowerChangeSound);
        yield return new WaitForSeconds(1.5f);
        GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f);
        GetComponent<AudioSource>().PlayOneShot(flowerChangeSound);
    }
}
