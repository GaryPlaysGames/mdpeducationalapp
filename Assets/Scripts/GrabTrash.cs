using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Text;

public class GrabTrash : MonoBehaviour
{
    Vector3 v3;
    Vector3 offset;
    public double offset_Touch_Amount;

    public AudioClip pickUpSound;
    public AudioClip dropSound;
    private bool playPickUpSound;
    public GameObject[] Circles;

    public AudioClip oneSound;
    public AudioClip twoSound;
    public AudioClip threeSound;

    [HideInInspector]
    public bool movedObjects;

    [HideInInspector]
    public int phase;

    [HideInInspector]
    public bool allDestroyed;

    bool circle1Hit = false;
    bool circle2Hit = false;
    bool circle3Hit = false;

    // Use this for initialization
    void Start()
    {
        playPickUpSound = true;
        movedObjects = false;
        phase = 1;
        circle1Hit = false;
        circle2Hit = false;
        circle3Hit = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        circle1Hit = false;
        circle2Hit = false;
        circle3Hit = false;
        switch (phase)
        {
            case 1:
            case 5:
            case 7:
                CheckSingleHit(ref circle1Hit);
                if (circle1Hit)
                {
                    MoveObject();
                }
                else if (movedObjects)
                {
                    GetComponent<AudioSource>().PlayOneShot(dropSound);
                    movedObjects = false;
                    playPickUpSound = true;
                }
                break;
            case 2:
            case 4:
                CheckDoubleHit(ref circle1Hit, ref circle2Hit);
                if (circle1Hit && circle2Hit)
                {
                    MoveObject();
                }
                else if (movedObjects)
                {
                    GetComponent<AudioSource>().PlayOneShot(dropSound);
                    movedObjects = false;
                    playPickUpSound = true;
                }
                break;
            case 3:
            case 6:
                CheckTripleHit(ref circle1Hit, ref circle2Hit, ref circle3Hit);
                if (circle1Hit)
                {
                    MoveObject();
                }
                if (movedObjects)
                {
                    GetComponent<AudioSource>().PlayOneShot(dropSound);
                    movedObjects = false;
                    playPickUpSound = true;
                }
                break;

        }
    }

    void CheckSingleHit(ref bool circle1Hit)
    {
        if(Input.touchCount >= 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            switch (Input.GetTouch(0).phase)
            {
                case (TouchPhase.Began):
                case (TouchPhase.Moved):
                case (TouchPhase.Stationary):
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 1")
                    {
                        circle1Hit = true;
                    } else
                    {
                        circle1Hit = false;
                    }
                    break;
            }
        }
    }

    void CheckDoubleHit(ref bool circle1Hit, ref bool circle2Hit)
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
            RaycastHit hit;
            switch (Input.GetTouch(i).phase)
            {
                case (TouchPhase.Began):
                case (TouchPhase.Moved):
                case (TouchPhase.Stationary):
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 1")
                    {
                        circle1Hit = true;
                    }
                    else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 2")
                    {
                        circle2Hit = true;
                    }
                    break;
            }
        }
    }

    void CheckTripleHit(ref bool circle1Hit, ref bool circle2Hit, ref bool circle3Hit)
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
            RaycastHit hit;
            switch (Input.GetTouch(i).phase)
            {
                case (TouchPhase.Began):
                case (TouchPhase.Moved):
                case (TouchPhase.Stationary):
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 1")
                    {
                        circle1Hit = true;
                    }
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 2")
                    {
                        circle2Hit = true;
                    }
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 3")
                    {
                        circle3Hit = true;
                    }
                    break;
            }
        }
    }

    void MoveObject()
    {
        movedObjects = true;
        if (playPickUpSound)
        {
            GetComponent<AudioSource>().PlayOneShot(pickUpSound);
            playPickUpSound = false;
            StartCoroutine(playNumberSoundBaseOnPickUp());
        }
        if (Input.touchCount <= 3)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                switch (Input.GetTouch(i).phase)
                {
                    case (TouchPhase.Began):
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            v3 = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x,
                                Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y, 0);
                            offset = hit.transform.position - v3;
                        }
                        break;
                    case (TouchPhase.Stationary):
                    case (TouchPhase.Moved):
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            v3 = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x,
                                Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y, 0);
                            if (Mathf.Abs(offset.x) <= 0.5f && Mathf.Abs(offset.y) <= 0.5f)
                            {
                                hit.transform.position = v3 + offset;
                            }
                        }
                        break;
                }
            }
        }
    }

    IEnumerator playNumberSoundBaseOnPickUp()
    {
        yield return new WaitForSeconds(0.15f);
        switch (phase)
        {
            case 1:
            case 5:
            case 7:
                GetComponent<AudioSource>().PlayOneShot(oneSound);
                break;
            case 2:
            case 4:
                GetComponent<AudioSource>().PlayOneShot(twoSound);
                break;
            case 3:
            case 6:
                GetComponent<AudioSource>().PlayOneShot(threeSound);
                break;

        }
    }
}
