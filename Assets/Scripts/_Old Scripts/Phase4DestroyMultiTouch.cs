using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Phase4DestroyMultiTouch : MonoBehaviour
{
    public AudioClip finishSound;
    public AudioClip bellSound;
    private bool finishPhase;
    public GameObject[] circles;
    public GameObject camera;
    private bool circle1;
    private bool circle2;
    private bool circle3;
    private bool circle4;

    // Use this for initialization
    void Start()
    {
        finishPhase = false;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (camera.GetComponent<Phase4MultiTouch>().phase)
        {
            case 1:
                CheckDoubleMultitouch(other);
                CheckPhase1();
                break;
            case 2:
                CheckTripleMultitouch(other);
                CheckPhase2();
                break;
            case 3:
                CheckQuadMultitouch(other);
                CheckPhase3();
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Circle 1")
        {
            circle1 = false;
        }
        else if (other.tag == "Circle 2")
        {
            circle2 = false;
        }
        else if (other.tag == "Circle 3")
        {
            circle3 = false;
        }
        else if (other.tag == "Circle 4")
        {
            circle4 = false;
        }
    }

    void CheckDoubleMultitouch(Collider other)
    {
        if (other.tag == "Circle 1")
        {
            circle1 = true;
        }
        else if (other.tag == "Circle 2")
        {
            circle2 = true;
        }
    }

    void CheckTripleMultitouch(Collider other)
    {
        if (other.tag == "Circle 1")
        {
            circle1 = true;
        }
        else if (other.tag == "Circle 2")
        {
            circle2 = true;
        }
        else if (other.tag == "Circle 3")
        {
            circle3 = true;
        }
    }

    void CheckQuadMultitouch(Collider other)
    {
        if (other.tag == "Circle 1")
        {
            circle1 = true;
        }
        else if (other.tag == "Circle 2")
        {
            circle2 = true;
        }
        else if (other.tag == "Circle 3")
        {
            circle3 = true;
        }
        else if (other.tag == "Circle 4")
        {
            circle4 = true;
        }
    }

    void CheckPhase1()
    {
        if (circle1 && circle2)
        {
            ++camera.GetComponent<Phase4MultiTouch>().phase;
            camera.GetComponent<Phase4MultiTouch>().pickUpAttempt = 0;
            camera.GetComponent<Phase4MultiTouch>().collectData = true;
            Phase1Reset();
        }
    }

    void CheckPhase2()
    {
        if (circle1 && circle2 && circle3)
        {
            ++camera.GetComponent<Phase4MultiTouch>().phase;
            camera.GetComponent<Phase4MultiTouch>().pickUpAttempt = 0;
            camera.GetComponent<Phase4MultiTouch>().collectData = true;
            Phase2Reset();
        }
    }

    void CheckPhase3()
    {
        if (!finishPhase && circle1 && circle2 && circle3 && circle4)
        {
            StartCoroutine(FinishPhase());
        }
    }

    void Phase1Reset()
    {
        GetComponent<AudioSource>().PlayOneShot(bellSound);
        circles[2].SetActive(true);
        circles[0].gameObject.transform.position = new Vector3(-6.5f, 2.0f, 0);
        circles[1].gameObject.transform.position = new Vector3(-6.5f, 0.0f, 0);
        circles[2].gameObject.transform.position = new Vector3(-6.5f, -2.0f, 0);
    }

    void Phase2Reset()
    {
        GetComponent<AudioSource>().PlayOneShot(bellSound);
        circles[3].SetActive(true);
        circles[0].gameObject.transform.position = new Vector3(-6.5f, 3.0f, 0);
        circles[1].gameObject.transform.position = new Vector3(-6.5f, 1.0f, 0);
        circles[2].gameObject.transform.position = new Vector3(-6.5f, -1.0f, 0);
        circles[3].gameObject.transform.position = new Vector3(-6.5f, -3.0f, 0);
    }

    IEnumerator FinishPhase()
    {
        finishPhase = true;
        foreach (GameObject circle in circles)
        {
            circle.SetActive(false);
        }
        GetComponent<AudioSource>().PlayOneShot(finishSound);
        yield return new WaitForSeconds(finishSound.length);
        Application.Quit();
    }
}
