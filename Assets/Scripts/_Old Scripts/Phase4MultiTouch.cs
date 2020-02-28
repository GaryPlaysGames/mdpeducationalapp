using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Text;

public class Phase4MultiTouch : MonoBehaviour
{
    public AudioClip pickUpSound;
    public AudioClip dropSound;
    private bool playPickUpSound;
    private bool movedObjects;
    public GameObject[] Circles;

    private GameObject infoHolder;
    [HideInInspector]
    public int phase;
    [HideInInspector]
    public int pickUpAttempt;
    [HideInInspector]
    public bool collectData;
    private string playerPath;

    // Use this for initialization
    void Start()
    {
        playPickUpSound = true;
        movedObjects = false;
        phase = 1;
        pickUpAttempt = 0;
        collectData = true;
        CreateDirectoryPath();
    }

    // Update is called once per frame
    void Update()
    {
        bool circle1Hit = false;
        bool circle2Hit = false;
        bool circle3Hit = false;
        bool circle4Hit = false;

        switch (phase)
        {
            case 1:
                CheckDoubleHit(ref circle1Hit, ref circle2Hit);
                if (circle1Hit && circle2Hit)
                {
                    if(collectData == true)
                    {
                        ++pickUpAttempt;
                    }
                    MoveObject();
                    if (collectData == true)
                    {
                        collectData = false;
                    }
                } else if (movedObjects)
                {
                    GetComponent<AudioSource>().PlayOneShot(dropSound);
                    movedObjects = false;
                    playPickUpSound = true;
                }
                break;
            case 2:
                CheckTripleHit(ref circle1Hit, ref circle2Hit, ref circle3Hit);
                if (circle1Hit && circle2Hit && circle3Hit)
                {
                    if (collectData == true)
                    {
                        ++pickUpAttempt;
                    }
                    MoveObject();
                    if (collectData == true)
                    {
                        collectData = false;
                    }
                } else if (movedObjects)
                {
                    GetComponent<AudioSource>().PlayOneShot(dropSound);
                    movedObjects = false;
                    playPickUpSound = true;
                }
                break;
            case 3:
                CheckQuadHit(ref circle1Hit, ref circle2Hit, ref circle3Hit, ref circle4Hit);
                if (circle1Hit && circle2Hit && circle3Hit && circle4Hit)
                {
                    if (collectData == true)
                    {
                        ++pickUpAttempt;
                    }
                    MoveObject();
                    if (collectData == true)
                    {
                        collectData = false;
                    }
                } else if (movedObjects)
                {
                    GetComponent<AudioSource>().PlayOneShot(dropSound);
                    movedObjects = false;
                    playPickUpSound = true;
                }
                break;
        }
    }

    void CreateDirectoryPath()
    {
        infoHolder = GameObject.FindGameObjectWithTag("Info");
        playerPath = Application.persistentDataPath + "/" + infoHolder.GetComponent<Phase0Info>().playerName +
                        "_Birthday_" + infoHolder.GetComponent<Phase0Info>().playerBirthday;
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
                case (TouchPhase.Ended):
                    collectData = true;
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
                    else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 2")
                    {
                        circle2Hit = true;
                    }
                    else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 3")
                    {
                        circle3Hit = true;
                    }
                    break;
                case (TouchPhase.Ended):
                    collectData = true;
                    break;
            }
        }
    }

    void CheckQuadHit(ref bool circle1Hit, ref bool circle2Hit, ref bool circle3Hit, ref bool circle4Hit)
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
                    else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 3")
                    {
                        circle3Hit = true;
                    }
                    else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Circle 4")
                    {
                        circle4Hit = true;
                    }
                    break;
                case (TouchPhase.Ended):
                    collectData = true;
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
        }
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
            RaycastHit hit;
            switch (Input.GetTouch(i).phase)
            {
                case (TouchPhase.Began):
                case (TouchPhase.Moved):
                case (TouchPhase.Stationary):
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (collectData == true)
                        {
                            CollectingData(hit, i);
                        }
                        hit.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x,
                            Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y, 0);
                    }
                    break;
            }
        }
    }

    void CollectingData(RaycastHit hit, int i)
    {
        float distance = Vector3.Distance(hit.transform.position, new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x,
                                                                                Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y, 0));
        if (!File.Exists(playerPath + "/phase4.txt"))
        {
            File.WriteAllText(playerPath + "/phase4.txt", "Distance finger was place from " + hit.transform.tag +
                " on pickup attempt " + pickUpAttempt + " during part " + phase + " was " + distance + " meters.\n");
        }
        else
        {
            File.AppendAllText(playerPath + "/phase4.txt", "Distance finger was place from " + hit.transform.tag +
                " on pickup attempt " + pickUpAttempt + " during part " + phase + " was " + distance + " meters.\n");
        }
    }
}
