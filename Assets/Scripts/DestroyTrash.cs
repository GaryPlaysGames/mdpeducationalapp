using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DestroyTrash : MonoBehaviour
{
    private GameObject MainMenu;

    public GameObject Game;
    public GameObject Alien;

    public GameObject AlienHand;
    public Sprite AlienHand1;
    public Sprite AlienHand2;
    public Sprite AlienHand3;

    public GameObject ExampleObject1;
    public GameObject ExampleObject2;
    public GameObject ExampleObject3;
    public GameObject ExampleBin1;
    public Sprite trashSprite;
    public Sprite compostSprite;
    public Sprite recycleSprite;

    private bool gamePaused;

    public AudioClip greatJobSound;
    public AudioClip youreDoingGreatSound;
    public AudioClip youDidItSound;

    public AudioClip finishSound;
    public AudioClip bellSound;

    public GameObject trashBin1;

    public Sprite trashBin;
    public Sprite recycleBin;
    public Sprite compostBin;

    public float timeToShowLogic;
    private float showTimer;

    private bool finishPhase;
    private bool phaseChange;
    public GameObject[] objectsToToss;
    public GameObject camera;
    private bool circle1;
    private bool circle2;
    private bool circle3;

    public AudioClip helpTrashSound;
    public AudioClip helpRecycleSound;
    public AudioClip helpCompostSound;
    private bool playHelpSound;

    public AudioClip dragTrashSound;
    public AudioClip dragRecycleSound;
    public AudioClip dragCompostSound;
    private bool playPromptSound;

    public GameObject helpHand;
    public GameObject helpNumber;
    public Sprite helpHand1;
    public Sprite helpHand2;
    public Sprite helpHand3;
    public Sprite helpNumber1;
    public Sprite helpNumber2;
    public Sprite helpNumber3;

    // Use this for initialization
    void Start()
    {
        MainMenu = GameObject.FindGameObjectWithTag("MainMenuLogic");
        gamePaused = false;
        finishPhase = false;
        phaseChange = false;
        showTimer = 0.0f;
        trashBin1.transform.position = new Vector3(3.8f, 0f, 0f);
        Alien.SetActive(false);
        Game.SetActive(true);
        AlienHand.SetActive(false);
        ExampleObject1.SetActive(false);
        ExampleObject2.SetActive(false);
        ExampleObject3.SetActive(false);
        ExampleBin1.SetActive(false);
        playHelpSound = true;
        playPromptSound = true;
        GetComponent<AudioSource>().PlayOneShot(helpTrashSound);
    }

    void Update()
    {
        if (!gamePaused)
        {
            if(!(camera.GetComponent<GrabTrash>().movedObjects) && !finishPhase)
            {
                showTimer += Time.deltaTime;
            }
            if (showTimer >= timeToShowLogic)
            {
                Game.SetActive(false);
                switch (camera.GetComponent<GrabTrash>().phase)
                {
                    case 1:
                    case 5:
                    case 7:
                        StartCoroutine(ShowTrashPrompt());
                        break;
                    case 2:
                    case 4:
                        StartCoroutine(ShowRecyclePrompt());
                        break;
                    case 3:
                    case 6:
                        StartCoroutine(ShowCompostPrompt());
                        break;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (camera.GetComponent<GrabTrash>().phase)
        {
            case 1:
            case 5:
            case 7:
                CheckSingleTouch(other);
                SingleCheckPhase();
                break;
            case 2:
            case 4:
                CheckDoubleMultitouch(other);
                DoubleCheckPhase();
                break;
            case 3:
            case 6:
                CheckTripleMultitouch(other);
                TripleCheckPhase();
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Circle 1")
        {
            circle1 = false;
        }
        else if (other.tag == "Circle 2")
        {
            circle2 = false;
        }
        else if (other.tag == "Circle 3")
        {
            circle3 = false;
        }
    }

    void CheckSingleTouch(Collider other)
    {
        if (other.tag == "Circle 1")
        {
            circle1 = true;
        }
    }

    void CheckDoubleMultitouch(Collider other)
    {
        if (other.tag == "Circle 1")
        {
            circle1 = true;
        }
        else if (other.tag == "Circle 2")
        {
            circle2 = true;
        }
    }

    void CheckTripleMultitouch(Collider other)
    {
        if (other.tag == "Circle 1")
        {
            circle1 = true;
        }
        else if (other.tag == "Circle 2")
        {
            circle2 = true;
        }
        else if (other.tag == "Circle 3")
        {
            circle3 = true;
        }
    }

    void SingleCheckPhase()
    {
        if (!finishPhase && circle1 && camera.GetComponent<GrabTrash>().phase == 1)
        {
            ++camera.GetComponent<GrabTrash>().phase;
            StartCoroutine(Phase1Reset());
        }
        else if (!finishPhase && circle1 && camera.GetComponent<GrabTrash>().phase == 5)
        {
            ++camera.GetComponent<GrabTrash>().phase;
            StartCoroutine(Phase2Reset());
        }
        else if (!finishPhase && circle1 && camera.GetComponent<GrabTrash>().phase == 7)
        {
            StartCoroutine(FinishPhase());
        }
    }

    void DoubleCheckPhase()
    {
        if ((circle1 || circle2) && camera.GetComponent<GrabTrash>().phase == 2)
        {
            ++camera.GetComponent<GrabTrash>().phase;
            StartCoroutine(Phase2Reset());
        }
        else if ((circle1 || circle2) && camera.GetComponent<GrabTrash>().phase == 4)
        {
            ++camera.GetComponent<GrabTrash>().phase;
            StartCoroutine(Phase0Reset());
        }
    }

    void TripleCheckPhase()
    {
        if ((circle1 || circle2 || circle3) && camera.GetComponent<GrabTrash>().phase == 3)
        {
            ++camera.GetComponent<GrabTrash>().phase;
            StartCoroutine(Phase1Reset());
        }
        else if ((circle1 || circle2 || circle3) && camera.GetComponent<GrabTrash>().phase == 6)
        {
            ++camera.GetComponent<GrabTrash>().phase;
            StartCoroutine(Phase0Reset());
        }
    }

    IEnumerator ShowTrashPrompt()
    {
        playPromptSound = true;
        if (playPromptSound)
        {
            GetComponent<AudioSource>().PlayOneShot(dragTrashSound);
            playPromptSound = false;
        }
        gamePaused = true;
        Game.SetActive(false);
        AlienHand.SetActive(true);

        AlienHand.GetComponent<SpriteRenderer>().sprite = AlienHand1;
        ExampleObject1.SetActive(true);
        ExampleObject1.GetComponent<SpriteRenderer>().sprite = trashSprite;
        ExampleBin1.SetActive(true);
        ExampleBin1.GetComponent<SpriteRenderer>().sprite = trashBin;
        ExampleBin1.transform.position = new Vector3(5.58f, 0, 82f);

        yield return new WaitForSeconds(AlienHand.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1.5f);

        ExampleBin1.SetActive(false);
        ExampleObject1.SetActive(false);
        AlienHand.SetActive(false);
        Game.SetActive(true);
        gamePaused = false;
        showTimer = 0.0f;
    }

    IEnumerator ShowRecyclePrompt()
    {
        playPromptSound = true;
        if (playPromptSound)
        {
            GetComponent<AudioSource>().PlayOneShot(dragRecycleSound);
            playPromptSound = false;
        }
        gamePaused = true;
        Game.SetActive(false);
        AlienHand.SetActive(true);

        AlienHand.GetComponent<SpriteRenderer>().sprite = AlienHand2;
        AlienHand.GetComponent<Animator>().SetTrigger(Animator.StringToHash("Move2"));
        ExampleObject1.SetActive(true);
        ExampleObject2.SetActive(true);
        ExampleObject1.GetComponent<SpriteRenderer>().sprite = recycleSprite;
        ExampleObject1.GetComponent<Animator>().SetTrigger(Animator.StringToHash("Move2"));
        ExampleObject2.GetComponent<SpriteRenderer>().sprite = recycleSprite;
        ExampleBin1.SetActive(true);
        ExampleBin1.GetComponent<SpriteRenderer>().sprite = recycleBin;
        ExampleBin1.transform.position = new Vector3(5.58f, 0, 82f);

        yield return new WaitForSeconds(AlienHand.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1.5f);

        ExampleBin1.SetActive(false);
        ExampleObject2.SetActive(false);
        ExampleObject1.SetActive(false);
        AlienHand.SetActive(false);
        Game.SetActive(true);
        gamePaused = false;
        showTimer = 0.0f;
    }

    IEnumerator ShowCompostPrompt()
    {
        playPromptSound = true;
        if (playPromptSound)
        {
            GetComponent<AudioSource>().PlayOneShot(dragCompostSound);
            playPromptSound = false;
        }
        gamePaused = true;
        Game.SetActive(false);
        AlienHand.SetActive(true);

        AlienHand.GetComponent<SpriteRenderer>().sprite = AlienHand3;
        AlienHand.GetComponent<Animator>().SetTrigger(Animator.StringToHash("Move2"));
        AlienHand.GetComponent<Animator>().SetTrigger(Animator.StringToHash("Move3"));
        ExampleObject1.SetActive(true);
        ExampleObject2.SetActive(true);
        ExampleObject3.SetActive(true);
        ExampleObject1.GetComponent<SpriteRenderer>().sprite = compostSprite;
        ExampleObject1.GetComponent<Animator>().SetTrigger(Animator.StringToHash("Move2"));
        ExampleObject1.GetComponent<Animator>().SetTrigger(Animator.StringToHash("Move3"));
        ExampleObject2.GetComponent<SpriteRenderer>().sprite = compostSprite;
        ExampleObject2.GetComponent<Animator>().SetTrigger(Animator.StringToHash("Move3"));
        ExampleObject3.GetComponent<SpriteRenderer>().sprite = compostSprite;
        ExampleBin1.SetActive(true);
        ExampleBin1.GetComponent<SpriteRenderer>().sprite = compostBin;
        ExampleBin1.transform.position = new Vector3(5.58f, 0, 82f);

        yield return new WaitForSeconds(AlienHand.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1.5f);

        ExampleBin1.SetActive(false);
        ExampleObject3.SetActive(false);
        ExampleObject2.SetActive(false);
        ExampleObject1.SetActive(false);
        AlienHand.SetActive(false);
        Game.SetActive(true);
        gamePaused = false;
        showTimer = 0.0f;
    }

    IEnumerator Phase0Reset()
    {
        playHelpSound = true;
        gamePaused = true;
        phaseChange = true;
        GetComponent<AudioSource>().PlayOneShot(bellSound);

        Game.SetActive(false);
        Alien.SetActive(true);
        float seed = Random.Range(0.0f, 1.0f);
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
        yield return new WaitForSeconds(Alien.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1.0f);

        Alien.SetActive(false);
        Game.SetActive(true);
        showTimer = 0.0f;
        trashBin1.GetComponent<SpriteRenderer>().sprite = trashBin;
        trashBin1.gameObject.transform.position = new Vector3(3.8f, 0f, 0);

        helpHand.GetComponent<SpriteRenderer>().sprite = helpHand1;
        helpNumber.GetComponent<SpriteRenderer>().sprite = helpNumber1;

        objectsToToss[0].gameObject.transform.position = new Vector3(-4f, 0, 0);
        objectsToToss[0].SetActive(true);
        objectsToToss[1].SetActive(false);
        objectsToToss[2].SetActive(false);
        objectsToToss[0].GetComponent<SpanObjectToToss>().spanSprite = true;
        circle1 = false;
        circle2 = false;
        circle3 = false;
        gamePaused = false;
        if (playHelpSound)
        {
            GetComponent<AudioSource>().PlayOneShot(helpTrashSound);
            playHelpSound = false;
        }
    }

    IEnumerator Phase1Reset()
    {
        playHelpSound = true;
        gamePaused = true;
        phaseChange = true;
        GetComponent<AudioSource>().PlayOneShot(bellSound);

        Game.SetActive(false);
        Alien.SetActive(true);
        float seed = Random.Range(0.0f, 1.0f);
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
        yield return new WaitForSeconds(Alien.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1.0f);

        Alien.SetActive(false);
        Game.SetActive(true);
        showTimer = 0.0f;
        trashBin1.GetComponent<SpriteRenderer>().sprite = recycleBin;
        trashBin1.gameObject.transform.position = new Vector3(3.8f, 0f, 0);

        helpHand.GetComponent<SpriteRenderer>().sprite = helpHand2;
        helpNumber.GetComponent<SpriteRenderer>().sprite = helpNumber2;

        objectsToToss[0].gameObject.transform.position = new Vector3(-4f, 1f, 0);
        objectsToToss[1].gameObject.transform.position = new Vector3(-4f, -1f, 0);
        objectsToToss[0].SetActive(true);
        objectsToToss[1].SetActive(true);
        objectsToToss[2].SetActive(false);
        objectsToToss[0].GetComponent<SpanObjectToToss>().spanSprite = true;
        objectsToToss[1].GetComponent<SpanObjectToToss>().spanSprite = true;
        circle1 = false;
        circle2 = false;
        circle3 = false;
        gamePaused = false;
        if (playHelpSound)
        {
            GetComponent<AudioSource>().PlayOneShot(helpRecycleSound);
            playHelpSound = false;
        }
    }

    IEnumerator Phase2Reset()
    {
        playHelpSound = true;
        gamePaused = true;
        phaseChange = true;
        GetComponent<AudioSource>().PlayOneShot(bellSound);

        Game.SetActive(false);
        Alien.SetActive(true);
        float seed = Random.Range(0.0f, 1.0f);
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
        yield return new WaitForSeconds(Alien.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1.0f);

        Alien.SetActive(false);
        Game.SetActive(true);
        showTimer = 0.0f;
        trashBin1.GetComponent<SpriteRenderer>().sprite = compostBin;
        trashBin1.gameObject.transform.position = new Vector3(3.8f, 0f, 0);

        helpHand.GetComponent<SpriteRenderer>().sprite = helpHand3;
        helpNumber.GetComponent<SpriteRenderer>().sprite = helpNumber3;

        objectsToToss[0].gameObject.transform.position = new Vector3(-4f, 2f, 0);
        objectsToToss[1].gameObject.transform.position = new Vector3(-4f, 0f, 0);
        objectsToToss[2].gameObject.transform.position = new Vector3(-4f, -2f, 0);
        objectsToToss[0].SetActive(true);
        objectsToToss[1].SetActive(true);
        objectsToToss[2].SetActive(true);
        objectsToToss[0].GetComponent<SpanObjectToToss>().spanSprite = true;
        objectsToToss[1].GetComponent<SpanObjectToToss>().spanSprite = true;
        objectsToToss[2].GetComponent<SpanObjectToToss>().spanSprite = true;
        circle1 = false;
        circle2 = false;
        circle3 = false;
        gamePaused = false;
        if (playHelpSound)
        {
            GetComponent<AudioSource>().PlayOneShot(helpCompostSound);
            playHelpSound = false;
        }
    }

    IEnumerator FinishPhase()
    {
        trashBin1.SetActive(false);
        helpHand.SetActive(false);
        helpNumber.SetActive(false);
        Alien.SetActive(true);
        finishPhase = true;
        foreach (GameObject currentObject in objectsToToss)
        {
            currentObject.SetActive(false);
        }

        GetComponent<AudioSource>().PlayOneShot(finishSound);
        yield return new WaitForSeconds(finishSound.length);

        if (MainMenu.GetComponent<MainMenuData>().levelsUnlocked == 2)
        {
            ++(MainMenu.GetComponent<MainMenuData>().levelsUnlocked);
        }

        DontDestroyOnLoad(MainMenu);
        SceneManager.LoadScene("Main Menu");
    }
}
