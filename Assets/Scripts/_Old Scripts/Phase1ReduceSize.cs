using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using System;
using System.IO;
using System.Text;

public class Phase1ReduceSize : MonoBehaviour
{
    public AudioClip finishSound;
    private Ray ray;
    private RaycastHit hit;
    private bool finishPhase;

    private GameObject infoHolder;
    private int tapCount;
    private double total_dist;
    private string playerPath;

    // Use this for initialization
    void Start()
    {
        finishPhase = false;
        tapCount = 0;
        total_dist = 0;
        CreateDirectory();
    }

    // Update is called once per frame
    void Update ()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity) && !finishPhase && 
                hit.transform.localScale.x > 0.2f && hit.transform.localScale.y > 0.2f) // out means hit becomes an output of this function, makes it so the object hit is stored in the hit varible
            {
                CollectData();
                GetComponent<AudioSource>().Play();
                hit.transform.localScale += new Vector3(-0.2f, -0.2f, 0);
            } else if (!finishPhase && transform.localScale.x <= 0.4f && transform.localScale.y <= 0.4f)
            {
                    StartCoroutine(FinishPhase());
            }
        }
    }

    void CreateDirectory()
    {
        infoHolder = GameObject.FindGameObjectWithTag("Info");
        playerPath = Application.persistentDataPath + "/" + infoHolder.GetComponent<Phase0Info>().playerName +
                        "_Birthday_" + infoHolder.GetComponent<Phase0Info>().playerBirthday;
        if (!Directory.Exists(playerPath))
        {
            Directory.CreateDirectory(playerPath);
        }
    }

    void CollectData()
    {
        Vector3 dist = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x,
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, 0.0f);
        ++tapCount;
        File.AppendAllText(playerPath + "/phase1.txt", "Distance away from tap: " + tapCount + " was " + dist.magnitude + 
            " and size of circle was " + Math.Round(transform.localScale.x, 2) + " meters\n");
        total_dist += dist.magnitude;
    }

    void CollectFinalData()
    {
        File.AppendAllText(playerPath + "/phase1.txt", "Number of taps to complete phase 1: " + tapCount + "\n");
        File.AppendAllText(playerPath + "/phase1.txt", "Average distance from center of circle in phase 1: "
                            + Math.Round(total_dist / tapCount, 3).ToString() + " meters\n");
    }

    IEnumerator FinishPhase()
    {
        finishPhase = true;
        GetComponent<AudioSource>().PlayOneShot(finishSound);
        yield return new WaitForSeconds(finishSound.length);
        CollectFinalData();
        DontDestroyOnLoad(infoHolder);
        SceneManager.LoadScene("Continue");
    }
}
