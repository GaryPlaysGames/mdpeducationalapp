using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColors : MonoBehaviour
{
    public AudioClip flowerSound;
    public float colorChangeTime;
    private bool colorChange;
    // Use this for initialization
    void Start ()
    {
        colorChange = true;
    }

	// Update is called once per frame
	void Update ()
    {
        if (colorChange)
        {
            colorChange = false;
            StartCoroutine(ColorChange());
        }
	}

    IEnumerator ColorChange()
    {
        Color lastColor = GetComponent<SpriteRenderer>().color;
        while (GetComponent<SpriteRenderer>().color == lastColor)
        {
            float seed = Random.Range(0.0f, 1.0f);
            if (seed <= 0.3f)
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
            }
            else if (seed > 0.3f && seed <= 0.6f)
            {
                GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f);
            }
        }
        GetComponent<AudioSource>().PlayOneShot(flowerSound);
        yield return new WaitForSeconds(colorChangeTime);
        colorChange = true;
    }
}
