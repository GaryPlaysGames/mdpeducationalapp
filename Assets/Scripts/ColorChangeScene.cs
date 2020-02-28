using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeScene : MonoBehaviour
{
    public GameObject alienHand;
    public Sprite twoFingers;
    public Sprite threeFingers;
    public AudioClip colorChangeSound;

    public GameObject SceneManager;
    // Use this for initialization
    void Start ()
    {
        StartCoroutine(FlowerColorScene());
	}

    IEnumerator FlowerColorScene()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
        GetComponent<AudioSource>().PlayOneShot(colorChangeSound);
        yield return new WaitForSeconds(SceneManager.GetComponent<CutSceneHandExample>().fingerOneSound.length + 1);
        GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
        alienHand.GetComponent<SpriteRenderer>().sprite = twoFingers;
        GetComponent<AudioSource>().PlayOneShot(colorChangeSound);
        yield return new WaitForSeconds(SceneManager.GetComponent<CutSceneHandExample>().fingerTwoSound.length + 1);
        GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f);
        alienHand.GetComponent<SpriteRenderer>().sprite = threeFingers;
        GetComponent<AudioSource>().PlayOneShot(colorChangeSound);
    }
}
