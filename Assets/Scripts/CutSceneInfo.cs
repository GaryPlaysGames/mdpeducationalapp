using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneInfo : MonoBehaviour
{
    public float secondsBetweenColors;

    public AudioClip backgroundSound;
    public AudioClip computerHum;
    public AudioClip alienHappySound;
    public AudioClip landingSound;
    public AudioClip transitSound;
    public AudioClip oSound;

    public float sceneTime;
    public string NextScene;

    private GameObject mainScene;
    private GameObject alternativeScene;

    public GameObject alien;
    private bool spawnAlien;

    // Use this for initialization
    void Start ()
    {
        if (SceneManager.GetActiveScene().name == "CutScene2")
        {
            mainScene = GameObject.FindGameObjectWithTag("Main Scene");
            alternativeScene = GameObject.FindGameObjectWithTag("Alternative Scene");
            mainScene.SetActive(true);
            alternativeScene.SetActive(false);
            StartCoroutine(SceneCut());
        }
        else if (SceneManager.GetActiveScene().name == "CutScene4")
        {
            spawnAlien = true;
            mainScene = GameObject.FindGameObjectWithTag("Main Scene");
            alternativeScene = GameObject.FindGameObjectWithTag("Alternative Scene");
            mainScene.SetActive(true);
            alternativeScene.SetActive(false);
            GetComponent<AudioSource>().PlayOneShot(landingSound);
            GameObject.FindGameObjectWithTag("Space Ship").GetComponent<Animator>().SetTrigger(Animator.StringToHash("isLanding"));
            StartCoroutine(SceneCut2());
        }
        StartCoroutine(JumpToScene());
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "CutScene4")
        {
            if (spawnAlien && GameObject.FindGameObjectWithTag("Space Ship").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Hover"))
            {
                spawnAlien = false;
                alien.SetActive(true);
                GetComponent<AudioSource>().PlayOneShot(transitSound);
                GetComponent<AudioSource>().PlayOneShot(oSound);
                alien.GetComponent<Animator>().SetTrigger("isWalking");
            }
        }
    }

    IEnumerator SceneCut()
    {
        GetComponent<AudioSource>().PlayOneShot(backgroundSound);
        GetComponent<AudioSource>().PlayOneShot(computerHum);
        yield return new WaitForSeconds(4);
        mainScene.SetActive(false);
        alternativeScene.SetActive(true);
        yield return new WaitForSeconds(3);
        mainScene.SetActive(true);
        alternativeScene.SetActive(false);
        GameObject.FindGameObjectWithTag("Alien").GetComponent<Animator>().speed = 1;
        GameObject.FindGameObjectWithTag("Alien").GetComponent<Animator>().SetTrigger(Animator.StringToHash("isCheering"));
        GetComponent<AudioSource>().PlayOneShot(alienHappySound);
        yield return new WaitForSeconds(3);
    }

    IEnumerator SceneCut2()
    {
        yield return new WaitForSeconds(5);
        mainScene.SetActive(false);
        alternativeScene.SetActive(true);
    }

    IEnumerator JumpToScene()
    {
        yield return new WaitForSeconds(sceneTime);
        SceneManager.LoadScene(NextScene);
    }
}
