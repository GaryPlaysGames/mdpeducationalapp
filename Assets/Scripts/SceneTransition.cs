using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private GameObject part1;
    private GameObject part2;
    private GameObject part3;

    public float sceneCut1;
    public float sceneCut2;
    // Use this for initialization
    void Start ()
    {
        part1 = GameObject.FindGameObjectWithTag("Part 1");
        part2 = GameObject.FindGameObjectWithTag("Part 2");

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(15))
        {
            part3 = GameObject.FindGameObjectWithTag("Part 3");
            part3.SetActive(false);
        }

        part2.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        StartCoroutine(SceneCuts());
	}

    IEnumerator SceneCuts()
    {
        yield return new WaitForSeconds(sceneCut1);
        part1.SetActive(false);
        part2.SetActive(true);
        yield return new WaitForSeconds(sceneCut2);
        part2.SetActive(false);

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(16))
        {
            part3.SetActive(true);
        }
    }
}
