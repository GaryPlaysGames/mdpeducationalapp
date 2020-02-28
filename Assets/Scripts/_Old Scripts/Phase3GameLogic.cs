using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using System;
using System.IO;
using System.Text;

public class Phase3GameLogic : MonoBehaviour
{
    public AudioClip redSound;
    public AudioClip blueSound;
    public AudioClip greenSound;
    public AudioClip yellowSound;
    public AudioClip finishSound;
    private bool playColorSound;
    private bool finishPhase;
    private bool continuePhase;
    private Ray ray;
    private RaycastHit hit;
    private int phase;
    private bool touchOn;
    private char[] state;
    private int stateCounter;
    private bool phaseTimer1;
    private bool phaseTimer2;
    private bool phaseTimer3;
    private bool phaseTimer4;

    public float waitTime;
    public GameObject continueButton;
    public GameObject redCircle;
    public GameObject blueCircle;
    public GameObject greenCircle;
    public GameObject yellowCircle;
    public Material[] material;

    private GameObject infoHolder;
    private int phase1Attempts;
    private int phase2Attempts;
    private int phase3Attempts;
    private int phase4Attempts;
    private string playerPath;

    // Use this for initialization
    void Start()
    {
        playColorSound = true;
        finishPhase = false;
        continuePhase = false;
        redCircle.GetComponent<Renderer>().sharedMaterial = material[0];
        blueCircle.GetComponent<Renderer>().sharedMaterial = material[2];
        greenCircle.GetComponent<Renderer>().sharedMaterial = material[4];
        yellowCircle.GetComponent<Renderer>().sharedMaterial = material[6];
        state = new char[4];
        phase = 1;
        touchOn = false;
        phaseTimer1 = true;
        phaseTimer2 = false;
        phaseTimer3 = false;
        phaseTimer4 = false;
        phase1Attempts = 0;
        phase2Attempts = 0;
        phase3Attempts = 0;
        phase4Attempts = 0;
        CreateDirectoryPath();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForNextPhase();
        if (phase == 5 && !finishPhase)
        {
            StartCoroutine(FinishPhase());
        }
        if (!finishPhase && Input.touchCount == 1 && touchOn == true)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            switch (Input.GetTouch(0).phase)
            {
                case (TouchPhase.Began):
                case (TouchPhase.Moved):
                case (TouchPhase.Stationary):
                    HitCircleEffect();
                    break;
                case (TouchPhase.Ended):
                    CheckHitAgainstLogic();
                    break;
            }
        }
        if (!finishPhase && touchOn == false && continuePhase)
        {
            SetLogicAndShow();
        }
    }

    void CreateDirectoryPath()
    {
        infoHolder = GameObject.FindGameObjectWithTag("Info");
        playerPath = Application.persistentDataPath + "/" + infoHolder.GetComponent<Phase0Info>().playerName +
                        "_Birthday_" + infoHolder.GetComponent<Phase0Info>().playerBirthday;
    }

    void CheckForNextPhase()
    {
        if (stateCounter == 4)
        {
            continuePhase = false;
            ++phase;
            if(phase != 5)
            {
                continueButton.SetActive(true);
            }
            stateCounter = 0;
            touchOn = false;
        }
    }

    void CollectData()
    {
        if (!File.Exists(playerPath + "/phase3.txt"))
        {
            File.WriteAllText(playerPath + "/phase3.txt", "Attempts to complete first simon says iteration: " + phase1Attempts + "\n");
            File.AppendAllText(playerPath + "/phase3.txt", "Attempts to complete second simon says iteration: " + phase2Attempts + "\n");
            File.AppendAllText(playerPath + "/phase3.txt", "Attempts to complete third simon says iteration: " + phase3Attempts + "\n");
            File.AppendAllText(playerPath + "/phase3.txt", "Attempts to complete fouth simon says iteration: " + phase4Attempts + "\n");
        }
    }

    void HitCircleEffect()
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == redCircle.tag)
        {
            if (playColorSound)
            {
                GetComponent<AudioSource>().PlayOneShot(redSound);
                playColorSound = false;
            }
            hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial = material[1];
            hit.transform.localScale = new Vector3(1.7f, 1.7f, 0);
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == blueCircle.tag)
        {
            if (playColorSound)
            {
                GetComponent<AudioSource>().PlayOneShot(blueSound);
                playColorSound = false;
            }
            hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial = material[3];
            hit.transform.localScale = new Vector3(1.7f, 1.7f, 0);
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == greenCircle.tag)
        {
            if (playColorSound)
            {
                GetComponent<AudioSource>().PlayOneShot(greenSound);
                playColorSound = false;
            }
            hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial = material[5];
            hit.transform.localScale = new Vector3(1.7f, 1.7f, 0);
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == yellowCircle.tag)
        {
            if (playColorSound)
            {
                GetComponent<AudioSource>().PlayOneShot(yellowSound);
                playColorSound = false;
            }
            hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial = material[7];
            hit.transform.localScale = new Vector3(1.7f, 1.7f, 0);
        }
    }

    void CheckHitAgainstLogic()
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == redCircle.tag)
        {
            playColorSound = true;
            HitCheck('R', 0);
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == blueCircle.tag)
        {
            playColorSound = true;
            HitCheck('B', 2);
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == greenCircle.tag)
        {
            playColorSound = true;
            HitCheck('G', 4);
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.tag == yellowCircle.tag)
        {
            playColorSound = true;
            HitCheck('Y', 6);
        }
    }

    void HitCheck(char circleColor, int materalID)
    {
        hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial = material[materalID];
        hit.transform.localScale = new Vector3(1.5f, 1.5f, 0);
        if (state[stateCounter] != circleColor)
        {
            switch (phase)
            {
                case 1:
                    ++phase1Attempts;
                    break;
                case 2:
                    ++phase2Attempts;
                    break;
                case 3:
                    ++phase3Attempts;
                    break;
                case 4:
                    ++phase4Attempts;
                    break;
            }
            stateCounter = 0;
        }
        else
        {
            ++stateCounter;
        }
    }

    void SetLogicAndShow()
    {
        switch (phase)
        {
            case 1:
                if (phaseTimer1 == true)
                {
                    state = new char[] { 'R', 'B', 'G', 'Y' };
                    StartCoroutine(Phase1Logic());
                }
                break;
            case 2:
                if (phaseTimer2 == true)
                {
                    state = new char[] { 'Y', 'G', 'R', 'B' };
                    StartCoroutine(Phase2Logic());
                }
                break;
            case 3:
                if (phaseTimer3 == true)
                {
                    state = new char[] { 'R', 'Y', 'B', 'G' };
                    StartCoroutine(Phase3Logic());
                }
                break;
            case 4:
                if (phaseTimer4 == true)
                {
                    state = new char[] { 'Y', 'B', 'G', 'R' };
                    StartCoroutine(Phase4Logic());
                }
                break;
        }
    }

    IEnumerator Phase1Logic()
    {
        phaseTimer1 = false;

        GetComponent<AudioSource>().PlayOneShot(redSound);
        redCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        redCircle.GetComponent<Renderer>().sharedMaterial = material[1];

        yield return new WaitForSeconds(waitTime);

        redCircle.GetComponent<Renderer>().sharedMaterial = material[0];
        redCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        GetComponent<AudioSource>().PlayOneShot(blueSound);
        blueCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        blueCircle.GetComponent<Renderer>().sharedMaterial = material[3];

        yield return new WaitForSeconds(waitTime);

        blueCircle.GetComponent<Renderer>().sharedMaterial = material[2];
        blueCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        GetComponent<AudioSource>().PlayOneShot(greenSound);
        greenCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        greenCircle.GetComponent<Renderer>().sharedMaterial = material[5];

        yield return new WaitForSeconds(waitTime);

        greenCircle.GetComponent<Renderer>().sharedMaterial = material[4];
        greenCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        GetComponent<AudioSource>().PlayOneShot(yellowSound);
        yellowCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        yellowCircle.GetComponent<Renderer>().sharedMaterial = material[7];

        yield return new WaitForSeconds(waitTime);

        yellowCircle.GetComponent<Renderer>().sharedMaterial = material[6];
        yellowCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        phaseTimer2 = true;
        touchOn = true;
    }

    IEnumerator Phase2Logic()
    {
        phaseTimer2 = false;

        GetComponent<AudioSource>().PlayOneShot(yellowSound);
        yellowCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        yellowCircle.GetComponent<Renderer>().sharedMaterial = material[7];

        yield return new WaitForSeconds(waitTime);

        yellowCircle.GetComponent<Renderer>().sharedMaterial = material[6];
        yellowCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        GetComponent<AudioSource>().PlayOneShot(greenSound);
        greenCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        greenCircle.GetComponent<Renderer>().sharedMaterial = material[5];

        yield return new WaitForSeconds(waitTime);

        greenCircle.GetComponent<Renderer>().sharedMaterial = material[4];
        greenCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        GetComponent<AudioSource>().PlayOneShot(redSound);
        redCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        redCircle.GetComponent<Renderer>().sharedMaterial = material[1];

        yield return new WaitForSeconds(waitTime);

        redCircle.GetComponent<Renderer>().sharedMaterial = material[0];
        redCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        GetComponent<AudioSource>().PlayOneShot(blueSound);
        blueCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        blueCircle.GetComponent<Renderer>().sharedMaterial = material[3];

        yield return new WaitForSeconds(waitTime);

        blueCircle.GetComponent<Renderer>().sharedMaterial = material[2];
        blueCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        phaseTimer3 = true;
        touchOn = true;
    }

    IEnumerator Phase3Logic()
    {
        phaseTimer3 = false;

        GetComponent<AudioSource>().PlayOneShot(redSound);
        redCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        redCircle.GetComponent<Renderer>().sharedMaterial = material[1];

        yield return new WaitForSeconds(waitTime);

        redCircle.GetComponent<Renderer>().sharedMaterial = material[0];
        redCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        GetComponent<AudioSource>().PlayOneShot(yellowSound);
        yellowCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        yellowCircle.GetComponent<Renderer>().sharedMaterial = material[7];

        yield return new WaitForSeconds(waitTime);

        yellowCircle.GetComponent<Renderer>().sharedMaterial = material[6];
        yellowCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        GetComponent<AudioSource>().PlayOneShot(blueSound);
        blueCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        blueCircle.GetComponent<Renderer>().sharedMaterial = material[3];

        yield return new WaitForSeconds(waitTime);

        blueCircle.GetComponent<Renderer>().sharedMaterial = material[2];
        blueCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        GetComponent<AudioSource>().PlayOneShot(greenSound);
        greenCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        greenCircle.GetComponent<Renderer>().sharedMaterial = material[5];

        yield return new WaitForSeconds(waitTime);

        greenCircle.GetComponent<Renderer>().sharedMaterial = material[4];
        greenCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        phaseTimer4 = true;
        touchOn = true;
    }

    IEnumerator Phase4Logic()
    {
        phaseTimer4 = false;

        GetComponent<AudioSource>().PlayOneShot(yellowSound);
        yellowCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        yellowCircle.GetComponent<Renderer>().sharedMaterial = material[7];

        yield return new WaitForSeconds(waitTime);

        yellowCircle.GetComponent<Renderer>().sharedMaterial = material[6];
        yellowCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        GetComponent<AudioSource>().PlayOneShot(blueSound);
        blueCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        blueCircle.GetComponent<Renderer>().sharedMaterial = material[3];

        yield return new WaitForSeconds(waitTime);

        blueCircle.GetComponent<Renderer>().sharedMaterial = material[2];
        blueCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        GetComponent<AudioSource>().PlayOneShot(greenSound);
        greenCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        greenCircle.GetComponent<Renderer>().sharedMaterial = material[5];

        yield return new WaitForSeconds(waitTime);

        greenCircle.GetComponent<Renderer>().sharedMaterial = material[4];
        greenCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        GetComponent<AudioSource>().PlayOneShot(redSound);
        redCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0);
        redCircle.GetComponent<Renderer>().sharedMaterial = material[1];

        yield return new WaitForSeconds(waitTime);

        redCircle.GetComponent<Renderer>().sharedMaterial = material[0];
        redCircle.transform.localScale += new Vector3(-0.2f, -0.2f, 0);

        touchOn = true;
    }

    public void Restart()
    {
        switch (phase)
        {
            case 1:
                phaseTimer1 = true;
                phaseTimer2 = false;
                break;
            case 2:
                phaseTimer2 = true;
                phaseTimer3 = false;
                break;
            case 3:
                phaseTimer3 = true;
                phaseTimer4 = false;
                break;
            case 4:
                phaseTimer4 = true;
                break;
        }
        touchOn = false;
        stateCounter = 0;
    }

    public void ContinuePhase()
    {
        continuePhase = true;
        continueButton.SetActive(false);
    }

    IEnumerator FinishPhase()
    {
        finishPhase = true;
        GetComponent<AudioSource>().PlayOneShot(finishSound);
        yield return new WaitForSeconds(finishSound.length);
        CollectData();
        DontDestroyOnLoad(infoHolder);
        SceneManager.LoadScene("Continue");
    }
}
