using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phase0Info : MonoBehaviour
{
    [HideInInspector]
    public int continueCounter;

    [HideInInspector]
    public string playerName;
    [HideInInspector]
    public string playerBirthday;

    // Use this for initialization
    void Start()
    {
        continueCounter = 0;
    }

    public void ContinousNameListener(string name)
    {
        playerName = name;
    }

    public void ContinousBirthdayListener(string birthday)
    {
        playerBirthday = birthday;
    }
}
