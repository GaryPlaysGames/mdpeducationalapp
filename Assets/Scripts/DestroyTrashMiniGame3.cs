using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DestroyTrashMiniGame3 : MonoBehaviour
{
    private bool circle1;
    private bool circle2;
    private bool circle3;
    public GameObject[] Circles;

    [HideInInspector]
    public bool destroyedObjects;

    public Sprite trashBin;
    public Sprite recycleBin;
    public Sprite compostBin;

    public AudioClip bellSound;

    // Use this for initialization
    void Start()
    {
        destroyedObjects = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (GetComponent<SpriteRenderer>().sprite == trashBin && other.tag == "Trash")
        {
            CheckSingleTouch(other);
            SingleCheckPhase(other);
        }
        else if (GetComponent<SpriteRenderer>().sprite == recycleBin && (other.tag == "Recycle 1" || other.tag == "Recycle 2"))
        {
            CheckDoubleMultitouch(other);
            DoubleCheckPhase(other);
        }
        else if (GetComponent<SpriteRenderer>().sprite == compostBin && (other.tag == "Compost 1" || other.tag == "Compost 2" || other.tag == "Compost 3"))
        {
            CheckTripleMultitouch(other);
            TripleCheckPhase(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Trash" || other.tag == "Recycle 1" || other.tag == "Compost 1")
        {
            circle1 = false;
        }
        else if (other.tag == "Recycle 2" || other.tag == "Compost 2")
        {
            circle2 = false;
        }
        else if (other.tag == "Compost 3")
        {
            circle3 = false;
        }
    }

    void CheckSingleTouch(Collider other)
    {
        if (other.tag == "Trash")
        {
            circle1 = true;
        }
    }

    void CheckDoubleMultitouch(Collider other)
    {
        if (other.tag == "Recycle 1")
        {
            circle1 = true;
        }
        else if (other.tag == "Recycle 2")
        {
            circle2 = true;
        }
    }

    void CheckTripleMultitouch(Collider other)
    {
        if (other.tag == "Compost 1")
        {
            circle1 = true;
        }
        else if (other.tag == "Compost 2")
        {
            circle2 = true;
        }
        else if (other.tag == "Compost 3")
        {
            circle3 = true;
        }
    }

    void SingleCheckPhase(Collider other)
    {
        if (circle1)
        {
            GetComponent<AudioSource>().PlayOneShot(bellSound);
            for (int i = 0; i < Circles.Length; ++i)
            {
                if(Circles[i].tag == "Trash")
                {
                    other.gameObject.SetActive(false);
                    continue;
                }
                Circles[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                Circles[i].GetComponent<BoxCollider>().enabled = true;
                Circles[i].transform.position = new Vector3(Circles[i].transform.position.x, Circles[i].transform.position.y, 0f);
            }
            destroyedObjects = true;
        }
    }

    void DoubleCheckPhase(Collider other)
    {
        if (circle1 && circle2)
        {
            GetComponent<AudioSource>().PlayOneShot(bellSound);
            for (int i = 0; i < Circles.Length; ++i)
            {
                if(Circles[i].tag == "Recycle 1" || Circles[i].tag == "Recycle 2")
                {
                    Circles[i].SetActive(false);
                    continue;
                }
                Circles[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                Circles[i].GetComponent<BoxCollider>().enabled = true;
                Circles[i].transform.position = new Vector3(Circles[i].transform.position.x, Circles[i].transform.position.y, 0f);
            }
            destroyedObjects = true;
        }
    }

    void TripleCheckPhase(Collider other)
    {
        if (circle1 && circle2 && circle3)
        {
            GetComponent<AudioSource>().PlayOneShot(bellSound);
            for (int i = 0; i < Circles.Length; ++i)
            {
                if (Circles[i].tag == "Compost 1" || Circles[i].tag == "Compost 2" || Circles[i].tag == "Compost 3")
                {
                    Circles[i].SetActive(false);
                    continue;
                }
                Circles[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                Circles[i].GetComponent<BoxCollider>().enabled = true;
                Circles[i].transform.position = new Vector3(Circles[i].transform.position.x, Circles[i].transform.position.y, 0f);
            }
            destroyedObjects = true;
        }
    }
}
