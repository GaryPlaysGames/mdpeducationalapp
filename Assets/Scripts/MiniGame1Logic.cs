using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using System;
using System.IO;
using System.Text;

public class MiniGame1Logic : MonoBehaviour
{
    private GameObject MainMenu;

    public GameObject Game;

    public float demoTime;
    public GameObject demoFlower;
    public GameObject demoAlienHand;
    public Sprite alienTwoFingers;
    public Sprite alienThreeFingers;
    private Color originalDemoColor;

    public float timeToShowLogic;
    private float showTimer;
    private bool correctPlayerPress;

    private bool playExampleColorSound;
    public AudioClip redSound;
    public AudioClip greenSound;
    public AudioClip blueSound;
    public AudioClip finishSound;

    public AudioClip greatJobSound;
    public AudioClip youreDoingGreatSound;
    public AudioClip youDidItSound;

    private bool playColorSound;
    private bool finishPhase;
    private bool continuePhase;
    private Ray ray;
    private RaycastHit hit;
    private int phase;
    private char state;
    private int stateCounter;
    private bool phaseTimer1;
    private bool phaseTimer2;
    private bool phaseTimer3;
    private bool phaseTimer4;
    private bool phaseTimer5;
    private bool phaseTimer6;

    public float holdTime;
    [HideInInspector]
    public float holdTimer;
    [HideInInspector]
    public bool touchOn;

    private GameObject flower;
    public GameObject alien;
    public GameObject answerFlower;
    public GameObject answerHand;
    public GameObject answerNumber;
    private Sprite oneFinger;
    public Sprite twoFingers;
    public Sprite threeFingers;
    public Sprite oneNumber;
    public Sprite twoNumber;
    public Sprite threeNumber;

    private bool playCurrentColorSound;
    public AudioClip redColorSound;
    public AudioClip greenColorSound;
    public AudioClip blueColorSound;

    // Use this for initialization
    void Start()
    {
        MainMenu = GameObject.FindGameObjectWithTag("MainMenuLogic");
        originalDemoColor = demoFlower.GetComponent<SpriteRenderer>().color;
        oneFinger = answerHand.GetComponent<SpriteRenderer>().sprite;
        oneNumber = answerNumber.GetComponent<SpriteRenderer>().sprite;
        showTimer = 0.0f;
        correctPlayerPress = false;
        flower = GameObject.FindGameObjectWithTag("Flower");
        playColorSound = true;
        playExampleColorSound = true;
        finishPhase = false;
        continuePhase = true;
        state = 'R';
        stateCounter = 0;
        phase = 1;
        touchOn = true;
        phaseTimer1 = true;
        phaseTimer2 = false;
        phaseTimer3 = false;
        phaseTimer4 = false;
        phaseTimer5 = false;
        phaseTimer6 = false;
        holdTimer = 0.0f;
        alien.SetActive(false);
        playCurrentColorSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (continuePhase && !correctPlayerPress)
        {
            showTimer += Time.deltaTime;
        }
        if(showTimer >= timeToShowLogic)
        {
            switch (state)
            {
                case 'R':
                    StartCoroutine(ColorLogic('R'));
                    break;
                case 'G':
                    StartCoroutine(ColorLogic('G'));
                    break;
                case 'B':
                    StartCoroutine(ColorLogic('B'));
                    break;
            }
        }
        CheckForNextPhase();
        if (phase == 7 && !finishPhase)
        {
            StartCoroutine(FinishPhase());
        }
        if (!finishPhase && touchOn == true)
        {
            CheckHitAgainstLogic();
        }
        if (!finishPhase && touchOn == true && continuePhase)
        {
            SetLogicAndShow();
        }
    }

    void CheckForNextPhase()
    {
        if (stateCounter == 1)
        {
            continuePhase = false;
            ++phase;
            if (phase != 7)
            {
                StartCoroutine(ContinuePhase());
            }
            stateCounter = 0;
            touchOn = false;
        }
    }

    void CheckHitAgainstLogic()
    {
        if (flower.GetComponent<SpriteRenderer>().color == new Color(1f, 0f, 0f))
        {
            playColorSound = true;
            HitCheck('R');
        }
        else if (flower.GetComponent<SpriteRenderer>().color == new Color(0f, 1f, 0f))
        {
            playColorSound = true;
            HitCheck('G');
        }
        else if (flower.GetComponent<SpriteRenderer>().color == new Color(0f, 0f, 1f))
        {
            playColorSound = true;
            HitCheck('B');
        }
    }

    void HitCheck(char circleColor)
    {
        if (state != circleColor)
        {
            correctPlayerPress = false;
            stateCounter = 0;
        }
        else
        {
            correctPlayerPress = true;
            holdTimer += Time.deltaTime;
            if(holdTimer >= holdTime)
            {
                ++stateCounter;
                holdTimer = 0.0f;
            }
        }
    }

    void SetLogicAndShow()
    {
        switch (phase)
        {
            case 1:
                if (phaseTimer1 == true)
                {
                    state = 'R';
                    answerFlower.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
                }
                break;
            case 2:
                if (phaseTimer2 == true)
                {
                    state = 'G';
                    answerFlower.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
                    if (playCurrentColorSound)
                    {
                        GetComponent<AudioSource>().PlayOneShot(greenColorSound);
                        playCurrentColorSound = false;
                    }
                    answerHand.GetComponent<SpriteRenderer>().sprite = twoFingers;
                    answerNumber.GetComponent<SpriteRenderer>().sprite = twoNumber;
                }
                break;
            case 3:
                if (phaseTimer3 == true)
                {
                    state = 'B';
                    answerFlower.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f);
                    if (playCurrentColorSound)
                    {
                        GetComponent<AudioSource>().PlayOneShot(blueColorSound);
                        playCurrentColorSound = false;
                    }
                    answerHand.GetComponent<SpriteRenderer>().sprite = threeFingers;
                    answerNumber.GetComponent<SpriteRenderer>().sprite = threeNumber;
                }
                break;
            case 4:
                if (phaseTimer4 == true)
                {
                    state = 'G';
                    answerFlower.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
                    if (playCurrentColorSound)
                    {
                        GetComponent<AudioSource>().PlayOneShot(greenColorSound);
                        playCurrentColorSound = false;
                    }
                    answerHand.GetComponent<SpriteRenderer>().sprite = twoFingers;
                    answerNumber.GetComponent<SpriteRenderer>().sprite = twoNumber;
                }
                break;
            case 5:
                if (phaseTimer5 == true)
                {
                    state = 'R';
                    answerFlower.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
                    if (playCurrentColorSound)
                    {
                        GetComponent<AudioSource>().PlayOneShot(redColorSound);
                        playCurrentColorSound = false;
                    }
                    answerHand.GetComponent<SpriteRenderer>().sprite = oneFinger;
                    answerNumber.GetComponent<SpriteRenderer>().sprite = oneNumber;
                }
                break;
            case 6:
                if (phaseTimer6 == true)
                {
                    state = 'B';
                    answerFlower.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f);
                    if (playCurrentColorSound)
                    {
                        GetComponent<AudioSource>().PlayOneShot(blueColorSound);
                        playCurrentColorSound = false;
                    }
                    answerHand.GetComponent<SpriteRenderer>().sprite = threeFingers;
                    answerNumber.GetComponent<SpriteRenderer>().sprite = threeNumber;
                }
                break;
        }
    }

    IEnumerator ColorLogic(char color)
    {
        Game.SetActive(false);
        demoFlower.SetActive(true);
        demoAlienHand.SetActive(true);
        switch (color)
        {
            case 'R':
                if (playExampleColorSound)
                {
                    playExampleColorSound = false;
                    GetComponent<AudioSource>().PlayOneShot(redSound);
                }
                break;
            case 'G':
                demoAlienHand.GetComponent<Animator>().SetTrigger(Animator.StringToHash("isTwo"));
                if (playExampleColorSound)
                {
                    playExampleColorSound = false;
                    GetComponent<AudioSource>().PlayOneShot(greenSound);
                }
                break;
            case 'B':
                demoAlienHand.GetComponent<Animator>().SetTrigger(Animator.StringToHash("isTwo"));
                demoAlienHand.GetComponent<Animator>().SetTrigger(Animator.StringToHash("isThree"));
                if (playExampleColorSound)
                {
                    playExampleColorSound = false;
                    GetComponent<AudioSource>().PlayOneShot(blueSound);
                }
                break;
        }
        yield return new WaitForSeconds(1.0f);
        switch (color)
        {
            case 'R':
                demoFlower.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f);
                break;
            case 'G':
                demoFlower.GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f);
                break;
            case 'B':
                demoFlower.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 1.0f);
                break;
        }
        yield return new WaitForSeconds(demoTime);
        demoFlower.GetComponent<SpriteRenderer>().color = originalDemoColor;
        demoAlienHand.SetActive(false);
        demoFlower.SetActive(false);
        Game.SetActive(true);
        showTimer = 0.0f;
        playExampleColorSound = true;
    }

    IEnumerator ContinuePhase()
    {
        float seed = UnityEngine.Random.Range(0.0f, 1.0f);
        answerFlower.SetActive(false);
        answerHand.SetActive(false);
        answerNumber.SetActive(false);
        flower.SetActive(false);
        alien.SetActive(true);

        if (seed <= 0.3f)
        {
            GetComponent<AudioSource>().PlayOneShot(greatJobSound);
        }
        else if (seed > 0.3f && seed <= 0.6f)
        {
            GetComponent<AudioSource>().PlayOneShot(youreDoingGreatSound);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(youDidItSound);
        }

        yield return new WaitForSeconds(alien.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1.0f);
        alien.SetActive(false);
        flower.SetActive(true);
        answerFlower.SetActive(true);
        answerHand.SetActive(true);
        answerNumber.SetActive(true);
        correctPlayerPress = false;
        showTimer = 0.0f;

        flower.GetComponent<SpriteRenderer>().color = flower.GetComponent<ChangeColor>().originalColor;
        flower.transform.localScale = new Vector3(3.0f, 3.0f, 0);
        continuePhase = true;
        touchOn = true;
        playCurrentColorSound = true;

        if(state == 'R' && phaseTimer1)
        {
            phaseTimer1 = false;
            phaseTimer2 = true;
        }
        else if(state == 'G' && phaseTimer2)
        {
            phaseTimer2 = false;
            phaseTimer3 = true;
        }
        else if (state == 'B' && phaseTimer3)
        {
            phaseTimer3 = false;
            phaseTimer4 = true;
        }
        else if (state == 'G' && phaseTimer4)
        {
            phaseTimer4 = false;
            phaseTimer5 = true;
        }
        else if (state == 'R' && phaseTimer5)
        {
            phaseTimer5 = false;
            phaseTimer6 = true;
        }
    }

    IEnumerator FinishPhase()
    {
        finishPhase = true;
        flower.SetActive(false);
        answerFlower.SetActive(false);
        answerHand.SetActive(false);
        answerNumber.SetActive(false);
        alien.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(finishSound);
        yield return new WaitForSeconds(finishSound.length);

        if (MainMenu.GetComponent<MainMenuData>().levelsUnlocked == 1)
        {
            ++(MainMenu.GetComponent<MainMenuData>().levelsUnlocked);
        }

        DontDestroyOnLoad(MainMenu);
        SceneManager.LoadScene("Main Menu");
    }

    public void BackToMainMenu()
    {
        DontDestroyOnLoad(MainMenu);
        SceneManager.LoadScene("Main Menu");
    }
}
