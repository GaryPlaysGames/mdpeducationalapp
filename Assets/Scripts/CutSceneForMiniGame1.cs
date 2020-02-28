using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneForMiniGame1 : MonoBehaviour
{
    public AudioClip whatsThisSound;
    public AudioClip lookAtFlowerSound;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(FlowerColorScene());
    }

    IEnumerator FlowerColorScene()
    {
        GetComponent<AudioSource>().PlayOneShot(whatsThisSound);
        yield return new WaitForSeconds(whatsThisSound.length + 1);
        GetComponent<AudioSource>().PlayOneShot(lookAtFlowerSound);
    }
}
