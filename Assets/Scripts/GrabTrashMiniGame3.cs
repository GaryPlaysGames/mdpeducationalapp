using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Text;

public class GrabTrashMiniGame3 : MonoBehaviour
{
    Vector3 v3;
    Vector3 offset;

    public GameObject[] trashBins;

    public AudioClip pickUpSound;
    public AudioClip dropSound;
    private bool playPickUpSound;
    public GameObject[] Circles;

    public AudioClip oneSound;
    public AudioClip twoSound;
    public AudioClip threeSound;

    private bool trashOnBoard;
    private bool recycleOnBoard;
    private bool compostOnBoard;

    public Sprite trashSprite;
    public Sprite recycleSprite;
    public Sprite compostSprite;

    private GameObject recycle1;
    private GameObject recycle2;
    private GameObject compost1;
    private GameObject compost2;
    private GameObject compost3;

    private Sprite currentObjectMoved;

    bool trashHit;
    bool recycle1Hit;
    bool recycle2Hit;
    bool compost1Hit;
    bool compost2Hit;
    bool compost3Hit;

    [HideInInspector]
    public bool movedObjects;

    [HideInInspector]
    public int phase;

    [HideInInspector]
    public bool allDestroyed;

    // Use this for initialization
    void Start()
    {
        playPickUpSound = true;
        trashOnBoard = false;
        recycleOnBoard = false;
        compostOnBoard = false;
        movedObjects = false;
        currentObjectMoved = null;
        phase = 1;

        trashHit = false;
        recycle1Hit = false;
        recycle2Hit = false;
        compost1Hit = false;
        compost2Hit = false;
        compost3Hit = false;

        trashOnBoard = false;
        recycleOnBoard = false;
        compostOnBoard = false;
    }

    // Update is called once per frame
    void Update()
    {
        trashOnBoard = false;
        recycleOnBoard = false;
        compostOnBoard = false;

        for (int i = 0; i < Circles.Length; ++i)
        {
            if (Circles[i].activeInHierarchy)
            {
                if (Circles[i].GetComponent<SpriteRenderer>().sprite == trashSprite)
                {
                    trashOnBoard = true;
                }
                else if (Circles[i].GetComponent<SpriteRenderer>().sprite == recycleSprite)
                {
                    recycleOnBoard = true;
                }
                else if (Circles[i].GetComponent<SpriteRenderer>().sprite == compostSprite)
                {
                    compostOnBoard = true;
                }
            }
        }

        if (trashOnBoard)
        {
            CheckSingleHit(ref trashHit);
            if (trashHit)
            {
                MoveObject(ref currentObjectMoved);
            }
            else if (movedObjects && currentObjectMoved == trashSprite)
            {
                GetComponent<AudioSource>().PlayOneShot(dropSound);
                movedObjects = false;
                playPickUpSound = true;
                for (int i = 0; i < Circles.Length; ++i)
                {
                    Circles[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    Circles[i].GetComponent<BoxCollider>().enabled = true;
                    Circles[i].transform.position = new Vector3(Circles[i].transform.position.x, Circles[i].transform.position.y, 0f);
                }
            }
        }

        if (recycleOnBoard)
        {
            CheckDoubleHit(ref recycle1Hit, ref recycle2Hit);
            if (recycle1Hit && recycle2Hit)
            {
                MoveObject(ref currentObjectMoved);
            }
            else if (movedObjects && currentObjectMoved == recycleSprite)
            {
                GetComponent<AudioSource>().PlayOneShot(dropSound);
                movedObjects = false;
                playPickUpSound = true;
                for(int i = 0; i < Circles.Length; ++i)
                {
                    if(Circles[i].tag == "Recycle 1" || Circles[i].tag == "Recycle 2")
                    {
                        Circles[i].tag = "Recycle";
                    }
                    else
                    {
                        Circles[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                        Circles[i].GetComponent<BoxCollider>().enabled = true;
                        Circles[i].transform.position = new Vector3(Circles[i].transform.position.x, Circles[i].transform.position.y, 0f);
                    }
                }
            }
        }

        if (compostOnBoard)
        {
            CheckTripleHit(ref compost1Hit, ref compost2Hit, ref compost3Hit);
            if (compost1Hit && compost2Hit && compost3Hit)
            {
                MoveObject(ref currentObjectMoved);

            }
            else if (movedObjects && currentObjectMoved == compostSprite)
            {
                GetComponent<AudioSource>().PlayOneShot(dropSound);
                movedObjects = false;
                playPickUpSound = true;
                for (int i = 0; i < Circles.Length; ++i)
                {
                    if (Circles[i].tag == "Compost 1" || Circles[i].tag == "Compost 2" || Circles[i].tag == "Compost 3")
                    {
                        Circles[i].tag = "Compost";
                    }
                    else
                    {
                        Circles[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                        Circles[i].GetComponent<BoxCollider>().enabled = true;
                        Circles[i].transform.position = new Vector3(Circles[i].transform.position.x, Circles[i].transform.position.y, 0f);
                    }
                }
            }
        }
    }

    void CheckSingleHit(ref bool circle1Hit)
    {
        GameObject trash1Hit = null;
        bool destroyedTrash = false;

        for(int i = 0; i < trashBins.Length; ++i)
        {
            if (trashBins[i].GetComponent<DestroyTrashMiniGame3>().destroyedObjects)
            {
                destroyedTrash = true;
            }
        }

        if (Input.touchCount >= 1 && !destroyedTrash)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            switch (Input.GetTouch(0).phase)
            {
                case (TouchPhase.Began):
                case (TouchPhase.Moved):
                case (TouchPhase.Stationary):
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Trash")
                    {
                        circle1Hit = true;
                        trash1Hit = hit.collider.gameObject;
                    }
                    break;
                case (TouchPhase.Ended):
                    circle1Hit = false;
                    break;
            }
            if (trash1Hit != null)
            {
                for (int i = 0; i < Circles.Length; ++i)
                {
                    if (Circles[i] != trash1Hit)
                    {
                        Circles[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
                        Circles[i].GetComponent<BoxCollider>().enabled = false;
                        Circles[i].transform.position = new Vector3(Circles[i].transform.position.x, Circles[i].transform.position.y, 1f);
                    }
                }
            }
        }
        else if (destroyedTrash)
        {
            for (int i = 0; i < Circles.Length; ++i)
            {
                Circles[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                Circles[i].GetComponent<BoxCollider>().enabled = true;
                Circles[i].transform.position = new Vector3(Circles[i].transform.position.x, Circles[i].transform.position.y, 0f);
            }
            for (int i = 0; i < trashBins.Length; ++i)
            {
                trashBins[i].GetComponent<DestroyTrashMiniGame3>().destroyedObjects = false;
            }
        }
    }

    void CheckDoubleHit(ref bool circle1Hit, ref bool circle2Hit)
    {
        GameObject firstRecycleHit = null;
        GameObject secondRecycleHit = null;
        string recycleHit1 = "";
        bool destroyedTrash = false;

        for (int i = 0; i < trashBins.Length; ++i)
        {
            if (trashBins[i].GetComponent<DestroyTrashMiniGame3>().destroyedObjects)
            {
                destroyedTrash = true;
            }
        }
        if (Input.touchCount != 2)
        {
            for (int i = 0; i < Circles.Length; ++i)
            {
                if (Circles[i].GetComponent<SpriteRenderer>().sprite == recycleSprite)
                {
                    Circles[i].tag = "Recycle";
                }
            }
        }
        else if (Input.touchCount >= 2 && !destroyedTrash)
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
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && (hit.transform.tag == "Recycle") && i == 0)
                        {
                            hit.collider.gameObject.tag = "Recycle 1";
                            recycleHit1 = hit.collider.gameObject.name;
                        }
                        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && (hit.transform.tag == "Recycle") && i == 1
                          && !String.Equals(hit.collider.gameObject.name, recycleHit1))
                        {
                            hit.collider.gameObject.tag = "Recycle 2";
                        }
                        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Recycle 1")
                        {
                            circle1Hit = true;
                            firstRecycleHit = hit.collider.gameObject;
                        }
                        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Recycle 2"
                            && circle1Hit)
                        {
                            circle2Hit = true;
                            secondRecycleHit = hit.collider.gameObject;
                        }
                        break;
                    case (TouchPhase.Ended):
                        circle1Hit = false;
                        circle2Hit = false;
                        break;
                }
            }
            if(firstRecycleHit != null && secondRecycleHit != null)
            {
                for (int k = 0; k < Circles.Length; ++k)
                {
                    if (Circles[k] != firstRecycleHit && Circles[k] != secondRecycleHit)
                    {
                        Circles[k].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
                        Circles[k].GetComponent<BoxCollider>().enabled = false;
                        Circles[k].transform.position = new Vector3(Circles[k].transform.position.x, Circles[k].transform.position.y, 1f);
                    }
                }
            }
        }
        else if (destroyedTrash)
        {
            for (int i = 0; i < Circles.Length; ++i)
            {
                Circles[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                Circles[i].GetComponent<BoxCollider>().enabled = true;
                Circles[i].transform.position = new Vector3(Circles[i].transform.position.x, Circles[i].transform.position.y, 0f);
            }
            for (int i = 0; i < trashBins.Length; ++i)
            {
                trashBins[i].GetComponent<DestroyTrashMiniGame3>().destroyedObjects = false;
            }
        }
    }

    void CheckTripleHit(ref bool circle1Hit, ref bool circle2Hit, ref bool circle3Hit)
    {
        GameObject firstCompostHit = null;
        GameObject secondCompostHit = null;
        GameObject thirdCompostHit = null;
        string compostHit1 = "";
        string compostHit2 = "";
        bool destroyedTrash = false;

        for (int i = 0; i < trashBins.Length; ++i)
        {
            if (trashBins[i].GetComponent<DestroyTrashMiniGame3>().destroyedObjects)
            {
                destroyedTrash = true;
            }
        }
        if (Input.touchCount != 3)
        {
            for(int i = 0; i < Circles.Length; ++i)
            {
                if(Circles[i].GetComponent<SpriteRenderer>().sprite == compostSprite)
                {
                    Circles[i].tag = "Compost";
                }
            }
        }
        else if (Input.touchCount >= 3 && !destroyedTrash)
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
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && (hit.transform.tag == "Compost") && i == 0)
                        {
                            hit.collider.gameObject.tag = "Compost 1";
                            compostHit1 = hit.collider.gameObject.name;
                        }
                        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && (hit.transform.tag == "Compost") && i == 1
                          && !String.Equals(hit.collider.gameObject.name, compostHit1))
                        {
                            hit.collider.gameObject.tag = "Compost 2";
                            compostHit2 = hit.collider.gameObject.name;
                        }
                        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && (hit.transform.tag == "Compost") && i == 2
                          && !String.Equals(hit.collider.gameObject.name, compostHit1) && !String.Equals(hit.collider.gameObject.name, compostHit2))
                        {
                            hit.collider.gameObject.tag = "Compost 3";
                        }
                        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Compost 1")
                        {
                            circle1Hit = true;
                            firstCompostHit = hit.collider.gameObject;
                        }
                        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Compost 2")
                        {
                            circle2Hit = true;
                            secondCompostHit = hit.collider.gameObject;
                        }
                        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == "Compost 3")
                        {
                            circle3Hit = true;
                            thirdCompostHit = hit.collider.gameObject;
                        }
                        break;
                    case (TouchPhase.Ended):
                        if(firstCompostHit != null)
                        {
                            firstCompostHit.tag = "Compost";
                        } else if(secondCompostHit != null)
                        {
                            secondCompostHit.tag = "Compost";
                        } else if(thirdCompostHit != null)
                        {
                            thirdCompostHit.tag = "Compost";
                        }
                        circle1Hit = false;
                        circle2Hit = false;
                        circle3Hit = false;
                        break;
                    case (TouchPhase.Canceled):
                        if (firstCompostHit != null)
                        {
                            firstCompostHit.tag = "Compost";
                        }
                        else if (secondCompostHit != null)
                        {
                            secondCompostHit.tag = "Compost";
                        }
                        else if (thirdCompostHit != null)
                        {
                            thirdCompostHit.tag = "Compost";
                        }
                        break;
                }
            }
            if (firstCompostHit != null && secondCompostHit != null && thirdCompostHit != null)
            {
                for (int k = 0; k < Circles.Length; ++k)
                {
                    if (Circles[k] != firstCompostHit && Circles[k] != secondCompostHit && Circles[k] != thirdCompostHit)
                    {
                        Circles[k].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
                        Circles[k].GetComponent<BoxCollider>().enabled = false;
                        Circles[k].transform.position = new Vector3(Circles[k].transform.position.x, Circles[k].transform.position.y, 1f);
                    }
                }
            }
        }
        else if (destroyedTrash)
        {
            for (int i = 0; i < Circles.Length; ++i)
            {
                Circles[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                Circles[i].GetComponent<BoxCollider>().enabled = true;
                Circles[i].transform.position = new Vector3(Circles[i].transform.position.x, Circles[i].transform.position.y, 0f);
            }
            for (int i = 0; i < trashBins.Length; ++i)
            {
                trashBins[i].GetComponent<DestroyTrashMiniGame3>().destroyedObjects = false;
            }
        }
    }

    void MoveObject(ref Sprite currentlyMovedObject)
    {
        movedObjects = true;
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
                case (TouchPhase.Moved):
                case (TouchPhase.Stationary):
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity) && (hit.collider.tag == "Trash" || 
                        hit.collider.tag == "Recycle 1" || hit.collider.tag == "Recycle 2" ||
                        hit.collider.tag == "Compost 1" || hit.collider.tag == "Compost 2" || hit.collider.tag == "Compost 3"))
                    {
                        currentlyMovedObject = hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite;
                        v3 = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x,
                            Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y, 0);
                        hit.transform.position = v3 + offset;
                    }
                    break;
            }
        }
        if (playPickUpSound)
        {
            GetComponent<AudioSource>().PlayOneShot(pickUpSound);
            playPickUpSound = false;
            StartCoroutine(playNumberSoundBaseOnPickUp());
        }
    }

    IEnumerator playNumberSoundBaseOnPickUp()
    {
        yield return new WaitForSeconds(0.15f);
        if(currentObjectMoved == trashSprite)
        {
            GetComponent<AudioSource>().PlayOneShot(oneSound);
        }
        else if(currentObjectMoved == recycleSprite)
        {
            GetComponent<AudioSource>().PlayOneShot(twoSound);
        }
        else if (currentObjectMoved == compostSprite)
        {
            GetComponent<AudioSource>().PlayOneShot(threeSound);
        }
    }
}
