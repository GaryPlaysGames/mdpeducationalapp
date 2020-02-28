using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phase0StartTest : MonoBehaviour
{
    private GameObject infoHolder;

    // Use this for initialization
    void Start()
    {
        infoHolder = GameObject.FindGameObjectWithTag("Info");
    }

    public void NextPhase()
    {
        DontDestroyOnLoad(infoHolder);
        SceneManager.LoadScene("Phase 1");
    }
}
