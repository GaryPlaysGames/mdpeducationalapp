using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcMovement : MonoBehaviour
{
    public AudioClip flyingSound;
    public float trajectoryHeight;
    public float speed;
    public string nextScene;

    private Vector3 startPos;
    private Vector3 endPos;

    private float startOfScene;

    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(flyingSound);
        GetComponent<Animator>().SetTrigger(Animator.StringToHash("isScaling"));
        startPos = new Vector3(-5.38f, 2.33f, 1f);
        endPos = new Vector3(5f, -2.33f, 1f);
        transform.position = startPos;
        startOfScene = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float cTime = (Time.time - startOfScene) * speed;

        Vector3 currentPos = Vector3.Lerp(startPos, endPos, cTime);

        currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(cTime) * Mathf.PI);

        transform.position = currentPos;
        if(currentPos == endPos)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
