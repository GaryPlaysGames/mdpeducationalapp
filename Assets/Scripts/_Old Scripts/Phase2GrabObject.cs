using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Text;

public class Phase2GrabObject : MonoBehaviour
{
    public AudioClip pickUpSound;
    public AudioClip dropSound;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 StartPos;

    public GameObject destroyer;

    private GameObject infoHolder;
    [HideInInspector]
    public string currentObject;
    [HideInInspector]
    public float holdTimeCounter;
    [HideInInspector]
    public bool playPickUpSound;
    [HideInInspector]
    public string playerPath;

    // Use this for initialization
    void Start()
    {
        playPickUpSound = true;
        StartPos = transform.position;
        holdTimeCounter = 0;
        CreateDirectoryPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            PhaseCase();
        }
        CollectData();
    }

    void CreateDirectoryPath()
    {
        infoHolder = GameObject.FindGameObjectWithTag("Info");
        playerPath = Application.persistentDataPath + "/" + infoHolder.GetComponent<Phase0Info>().playerName +
                        "_Birthday_" + infoHolder.GetComponent<Phase0Info>().playerBirthday;
    }

    void PhaseCase()
    {
        switch (Input.GetTouch(0).phase)
        {
            case (TouchPhase.Began):
            case (TouchPhase.Moved):
            case (TouchPhase.Stationary):
                MoveObject();
                break;
            case (TouchPhase.Ended):
                ResetObject();
                break;
        }
    }

    void MoveObject()
    {
        holdTimeCounter += Time.deltaTime;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == gameObject.tag)
        {
            if (playPickUpSound)
            {
                GetComponent<AudioSource>().PlayOneShot(pickUpSound);
                playPickUpSound = false;
            }
            hit.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x,
                Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, 0);
        }
    }

    void ResetObject()
    {
        holdTimeCounter = 0;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == gameObject.tag && gameObject.activeInHierarchy == true)
        {
            playPickUpSound = true;
            GetComponent<AudioSource>().PlayOneShot(dropSound);
            gameObject.transform.position = StartPos;
        }
    }

    void CollectData()
    {
        if (destroyer.GetComponent<Phase2DestroyObject>().destroyed)
        {
            currentObject = destroyer.GetComponent<Phase2DestroyObject>().destroyedObject;
            if (destroyer.GetComponent<Phase2DestroyObject>().ballsDestroyed <= 4)
            {
                CollectDataOnPart1();
            }
            else if (destroyer.GetComponent<Phase2DestroyObject>().ballsDestroyed >= 5 && destroyer.GetComponent<Phase2DestroyObject>().ballsDestroyed <= 7)
            {
                CollectDataOnPart2();
            }
            destroyer.GetComponent<Phase2DestroyObject>().destroyed = false;
        }
    }

    void CollectDataOnPart1()
    {
        if (!File.Exists(playerPath + "/phase2.txt"))
        {
            File.WriteAllText(playerPath + "/phase2.txt", "Time holding " + currentObject +
                " to destroy it was " + holdTimeCounter + " seconds\n");
        }
        else
        {
            File.AppendAllText(playerPath + "/phase2.txt", "Time holding " + currentObject +
                " to destroy it was " + holdTimeCounter + " seconds\n");
        }
    }

    void CollectDataOnPart2()
    {
        File.AppendAllText(playerPath + "/phase2.txt", "Time holding " + currentObject +
            " to destroy it the seccond time was " + holdTimeCounter + " seconds\n");
    }
}
