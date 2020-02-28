using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandChangeScene : MonoBehaviour
{
    public Sprite twoFingers;
    public Sprite threeFingers;

    public float secondsBetweenHands;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(HandNumberScene());
    }

    IEnumerator HandNumberScene()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<SpriteRenderer>().sprite = twoFingers;
        yield return new WaitForSeconds(secondsBetweenHands);
        GetComponent<SpriteRenderer>().sprite = threeFingers;
    }
}
