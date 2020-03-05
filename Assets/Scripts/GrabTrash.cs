using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Text;

using UnityEngine.UI;  //its a must to access new UI in script

public class GrabTrash : MonoBehaviour
{

    public float velocityDragSpeed;

    [HideInInspector]
    public bool dragging;

    private Vector3[] worldPos = new Vector3[3];
    private Rigidbody[] toDragRigidbody = new Rigidbody[3];
    private Transform[] toDragPos = new Transform[3];

    public float soundDelay;
    private float soundDelayTimer;

    public AudioClip pickUpSound;
    public AudioClip dropSound;
    private bool playPickUpSound;

    public AudioClip oneSound;
    public AudioClip twoSound;
    public AudioClip threeSound;

    [HideInInspector]
    public bool movedObjects;

    [HideInInspector]
    public int phase;

    [HideInInspector]
    public bool allDestroyed;

    [HideInInspector]
    public bool circle1Hit;
    [HideInInspector]
    public bool circle2Hit;
    [HideInInspector]
    public bool circle3Hit;

    // Use this for initialization
    void Start()
    {
        soundDelayTimer = 0.0f;
        dragging = false;
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
        if(soundDelayTimer > 0.0f)
        {
            soundDelayTimer -= Time.deltaTime;
        }
        switch (phase)
        {
            case 1:
            case 5:
            case 7:
                CheckSingleHit(ref circle1Hit);
                if (dragging)
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
                if (dragging)
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
                if (dragging)
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
        }
    }

    void CheckSingleHit(ref bool circle1Hit)
    {
        if(Input.touchCount == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            switch (Input.GetTouch(0).phase)
            {
                case (TouchPhase.Began):
                case (TouchPhase.Stationary):
                case (TouchPhase.Moved):
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 1")
                    {
                        circle1Hit = true;
                        dragging = true;
                        toDragRigidbody[0] = hit.rigidbody;
                        toDragPos[0] = hit.transform;
                    }
                    break;
                case (TouchPhase.Ended):
                case (TouchPhase.Canceled):
                    if (circle1Hit)
                    {
                        circle1Hit = false;
                        dragging = false;
                        toDragRigidbody[0].velocity = Vector3.zero;
                    }
                    break;
            }
        }
        else
        {
            dragging = false;
        }
    }

    void CheckDoubleHit(ref bool circle1Hit, ref bool circle2Hit)
    {
        if(Input.touchCount == 2)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                switch (Input.GetTouch(i).phase)
                {
                    case (TouchPhase.Began):
                    case (TouchPhase.Stationary):
                    case (TouchPhase.Moved):
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 1")
                        {
                            circle1Hit = true;
                            toDragRigidbody[i] = hit.rigidbody;
                            toDragPos[i] = hit.transform;
                        }
                        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 2")
                        {
                            circle2Hit = true;
                            toDragRigidbody[i] = hit.rigidbody;
                            toDragPos[i] = hit.transform;
                        }
                        break;
                    case (TouchPhase.Ended):
                    case (TouchPhase.Canceled):
                        if (circle1Hit && circle2Hit)
                        {
                            circle1Hit = false;
                            circle2Hit = false;
                            dragging = false;
                            for (int k = 0; k < 2; ++k)
                            {
                                toDragRigidbody[k].velocity = Vector3.zero;
                            }
                        }
                        break;
                }
                if (circle1Hit && circle2Hit)
                {
                    dragging = true;
                }
            }
        }
        else
        {
            dragging = false;
        }
    }

    void CheckTripleHit(ref bool circle1Hit, ref bool circle2Hit, ref bool circle3Hit)
    {
        if (Input.touchCount == 3)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                switch (Input.GetTouch(i).phase)
                {
                    case (TouchPhase.Began):
                    case (TouchPhase.Stationary):
                    case (TouchPhase.Moved):
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 1")
                        {
                            circle1Hit = true;
                            toDragRigidbody[i] = hit.rigidbody;
                            toDragPos[i] = hit.transform;
                        }
                        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 2")
                        {
                            circle2Hit = true;
                            toDragRigidbody[i] = hit.rigidbody;
                            toDragPos[i] = hit.transform;
                        }
                        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 3")
                        {
                            circle3Hit = true;
                            toDragRigidbody[i] = hit.rigidbody;
                            toDragPos[i] = hit.transform;
                        }
                        break;
                    case (TouchPhase.Ended):
                    case (TouchPhase.Canceled):
                        if (circle1Hit && circle2Hit && circle3Hit)
                        {
                            circle1Hit = false;
                            circle2Hit = false;
                            circle3Hit = false;
                            dragging = false;
                            for (int k = 0; k < 3; ++k)
                            {
                                toDragRigidbody[k].velocity = Vector3.zero;
                            }
                        }
                        break;
                }
            }
            if (circle1Hit && circle2Hit && circle3Hit)
            {
                dragging = true;
            }
        }
        else
        {
            circle1Hit = false;
            circle2Hit = false;
            circle3Hit = false;
            dragging = false;
        }
    }

    void MoveObject()
    {
        movedObjects = true;
        if (playPickUpSound)
        {
            GetComponent<AudioSource>().PlayOneShot(pickUpSound);
            playPickUpSound = false;
            if(soundDelayTimer <= 0.0f)
            {
                soundDelayTimer = soundDelay;
                StartCoroutine(playNumberSoundBaseOnPickUp());
            }
        }
        if (Input.touchCount <= 3)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                switch (Input.GetTouch(i).phase)
                {
                    case (TouchPhase.Began):
                    case (TouchPhase.Stationary):
                    case (TouchPhase.Moved):
                        worldPos[i] = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x,
                            Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y, 0);
                        toDragRigidbody[i].velocity = (worldPos[i] - toDragPos[i].position) * Time.deltaTime * velocityDragSpeed;
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
