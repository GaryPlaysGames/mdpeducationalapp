using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneHandExample : MonoBehaviour
{
    public AudioClip fingerOneSound;
    public AudioClip fingerTwoSound;
    public AudioClip fingerThreeSound;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(FlowerColorScene());
    }

    IEnumerator FlowerColorScene()
    {
        GetComponent<AudioSource>().PlayOneShot(fingerOneSound);
        yield return new WaitForSeconds(fingerOneSound.length + 1);
        GetComponent<AudioSource>().PlayOneShot(fingerTwoSound);
        yield return new WaitForSeconds(fingerTwoSound.length + 1);
        GetComponent<AudioSource>().PlayOneShot(fingerThreeSound);
    }
}
