using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    private GameObject infoHolder;

	// Use this for initialization
	void Start ()
    {
        infoHolder = GameObject.FindGameObjectWithTag("Info");
    }
	
    public void StartContinue()
    {
        switch (infoHolder.GetComponent<Phase0Info>().continueCounter)
        {
            case 0:
                ++infoHolder.GetComponent<Phase0Info>().continueCounter;
                DontDestroyOnLoad(infoHolder);
                SceneManager.LoadScene("Phase 2");
                break;
            case 1:
                ++infoHolder.GetComponent<Phase0Info>().continueCounter;
                DontDestroyOnLoad(infoHolder);
                SceneManager.LoadScene("Phase 3");
                break;
            case 2:
                DontDestroyOnLoad(infoHolder);
                SceneManager.LoadScene("Phase 4");
                break;
        }
    }
}
