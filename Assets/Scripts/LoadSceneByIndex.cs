using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneByIndex : MonoBehaviour
{
    private GameObject MainMenu;

    public float sceneTime;
    public int NextScene;

    // Use this for initialization
    void Start()
    {
        MainMenu = GameObject.FindGameObjectWithTag("MainMenuLogic");
        if (SceneManager.GetActiveScene().name == "Final Scene")
        {
            StartCoroutine(EndGame());
        }
        else
        {
            StartCoroutine(JumpToScene());
        }
    }

    IEnumerator JumpToScene()
    {
        yield return new WaitForSeconds(sceneTime);
        DontDestroyOnLoad(MainMenu);
        SceneManager.LoadScene(NextScene);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(sceneTime);
        Application.Quit();
    }
}
