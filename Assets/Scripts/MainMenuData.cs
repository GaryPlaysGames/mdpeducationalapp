using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuData : MonoBehaviour
{
    public int levelsUnlocked;
    public bool playUnlockLevelOneSound;
    public bool playUnlockLevelTwoSound;
    public bool playUnlockLevelThreeSound;

    // Use this for initialization
    void Start ()
    {
        levelsUnlocked = 1;
        playUnlockLevelOneSound = true;
        playUnlockLevelTwoSound = true;
        playUnlockLevelThreeSound = true;
    }
}
