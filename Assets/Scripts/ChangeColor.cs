using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private int hitCount;
    private bool petal1Hit;
    private bool petal2Hit;
    private bool petal3Hit;
    private bool petal4Hit;
    private bool petal5Hit;

    [HideInInspector]
    public Color originalColor;

    private GameObject gameLogic;

    public AudioClip flowerSound;
    private bool playFlowerSound;

    public AudioClip oneSound;
    public AudioClip twoSound;
    public AudioClip threeSound;

    // Use this for initialization
    void Start()
    {
        hitCount = 0;
        petal1Hit = false;
        petal2Hit = false;
        petal3Hit = false;
        petal4Hit = false;
        petal5Hit = false;
        originalColor = GetComponent<SpriteRenderer>().color;
        gameLogic = GameObject.FindGameObjectWithTag("Logic");
        playFlowerSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameLogic.GetComponent<MiniGame1Logic>().touchOn)
        {
            hitCount = 0;
            for (int i = 0; i < Input.touchCount; ++i)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Petal 1" && petal1Hit == false)
                {
                    ++hitCount;
                    petal1Hit = true;
                }
                else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Petal 2" && petal2Hit == false)
                {
                    ++hitCount;
                    petal2Hit = true;
                }
                else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Petal 3" && petal3Hit == false)
                {
                    ++hitCount;
                    petal3Hit = true;
                }
                else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Petal 4" && petal4Hit == false)
                {
                    ++hitCount;
                    petal4Hit = true;
                }
                else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Petal 5" && petal5Hit == false)
                {
                    ++hitCount;
                    petal5Hit = true;
                }
            }
            petal1Hit = false;
            petal2Hit = false;
            petal3Hit = false;
            petal4Hit = false;
            petal5Hit = false;
            switch (hitCount)
            {
                case 0:
                    GetComponent<SpriteRenderer>().color = originalColor;
                    transform.localScale = new Vector3(3.0f, 3.0f, 0);
                    gameLogic.GetComponent<MiniGame1Logic>().holdTimer = 0.0f;
                    playFlowerSound = true;
                    break;
                case 1:
                    GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
                    transform.localScale = new Vector3(3.03f, 3.03f, 0);
                    if (playFlowerSound)
                    {
                        GetComponent<AudioSource>().PlayOneShot(flowerSound);
                        playFlowerSound = false;
                        StartCoroutine(playNumberSoundBaseOnColor());
                    }
                    break;
                case 2:
                    GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
                    transform.localScale = new Vector3(3.03f, 3.03f, 0);
                    if (playFlowerSound)
                    {
                        GetComponent<AudioSource>().PlayOneShot(flowerSound);
                        playFlowerSound = false;
                        StartCoroutine(playNumberSoundBaseOnColor());
                    }
                    break;
                case 3:
                case 4:
                case 5:
                    GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f);
                    transform.localScale = new Vector3(3.03f, 3.03f, 0);
                    if (playFlowerSound)
                    {
                        GetComponent<AudioSource>().PlayOneShot(flowerSound);
                        playFlowerSound = false;
                        StartCoroutine(playNumberSoundBaseOnColor());
                    }
                    break;
            }
        }
    }

    IEnumerator playNumberSoundBaseOnColor()
    {
        yield return new WaitForSeconds(0.15f);
        if (GetComponent<SpriteRenderer>().color == new Color(1f, 0f, 0f))
        {
            GetComponent<AudioSource>().PlayOneShot(oneSound);
        }
        else if (GetComponent<SpriteRenderer>().color == new Color(0f, 1f, 0f))
        {
            GetComponent<AudioSource>().PlayOneShot(twoSound);
        }
        else if (GetComponent<SpriteRenderer>().color == new Color(0f, 0f, 1f))
        {
            GetComponent<AudioSource>().PlayOneShot(threeSound);
        }
    }
}