using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using System;
using System.IO;
using System.Text;

public class Phase2DestroyObject : MonoBehaviour
{
    public AudioClip finishSound;
    public AudioClip bellSound;
    private bool finishPhase;
    public GameObject[] circles;
    private bool ResetBalls;

    public float CircleSpeed;
    private float CircleWidth;
    private float CircleHeight;
    private float TimeCounter;

    private GameObject infoHolder;
    [HideInInspector]
    public int ballsDestroyed;
    [HideInInspector]
    public bool destroyed;
    [HideInInspector]
    public string destroyedObject;

    // Use this for initialization
    void Start()
    {
        finishPhase = false;
        destroyed = false;
        ballsDestroyed = 0;
        TimeCounter = 0;
        CircleWidth = 1.7f;
        CircleHeight = 1.7f;
        infoHolder = GameObject.FindGameObjectWithTag("Info");
    }

    // Update is called once per frame
    void Update()
    {    
        if (ballsDestroyed >= 4)
        {
            if(ResetBalls == true)
            {
                ResetCircles();
                ResetBalls = false;
            }
            RotateDestroyer();
        }
        if(ballsDestroyed == 8 && !finishPhase)
        {
            StartCoroutine(FinishPhase());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        destroyedObject = other.gameObject.tag;
        other.gameObject.SetActive(false);
        destroyed = true;
        ++ballsDestroyed;
        if(ballsDestroyed != 8)
        {
            GetComponent<AudioSource>().PlayOneShot(bellSound);
        }
        if (ballsDestroyed == 4)
        {
            ResetBalls = true;
        }
    }

    void ResetCircles()
    {
        for (int i = 0; i < 4; ++i)
        {
            circles[i].GetComponent<Phase2GrabObject>().playPickUpSound = true;
            circles[i].SetActive(true);
        }
        circles[0].gameObject.transform.position = new Vector3(6.5f, 3.5f, 0);
        circles[1].gameObject.transform.position = new Vector3(-6.5f, 3.5f, 0);
        circles[2].gameObject.transform.position = new Vector3(-6.5f, -3.5f, 0);
        circles[3].gameObject.transform.position = new Vector3(6.5f, -3.5f, 0);
    }

    void RotateDestroyer()
    {
        TimeCounter += Time.deltaTime * CircleSpeed;
        float x = Mathf.Cos(TimeCounter) * CircleWidth;
        float y = Mathf.Sin(TimeCounter) * CircleHeight;
        float z = 0;
        transform.position = new Vector3(x, y, z);
    }

    IEnumerator FinishPhase()
    {
        finishPhase = true;
        GetComponent<AudioSource>().PlayOneShot(finishSound);
        yield return new WaitForSeconds(finishSound.length);
        File.AppendAllText(circles[0].GetComponent<Phase2GrabObject>().playerPath + "/phase2.txt", "Time holding " + destroyedObject +
    " to destroy it the seccond time was " + circles[0].GetComponent<Phase2GrabObject>().holdTimeCounter + " seconds\n");
        DontDestroyOnLoad(infoHolder);
        SceneManager.LoadScene("Continue");
    }
}
