using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGame3Logic : MonoBehaviour
{
    public AudioClip finishSound;
    public GameObject alien;

    public AudioClip tryNewDiceRollSound;
    public AudioClip tryNewNumberSound;
    public AudioClip rollingSound1;
    public AudioClip rollingSound2;

    public AudioClip greatJobSound;
    public AudioClip youreDoingGreatSound;
    public AudioClip youDidItSound;

    public int rounds;
    [HideInInspector]
    public int currentRound;

    public int diceRollNumber;
    private bool diceRoll;

    public GameObject dice;
    public GameObject number;

    public Sprite dice1Sprite;
    public Sprite dice2Sprite;
    public Sprite dice3Sprite;
    public Sprite dice4Sprite;
    public Sprite dice5Sprite;
    public Sprite dice6Sprite;
    public Sprite number1Sprite;
    public Sprite number2Sprite;
    public Sprite number3Sprite;
    public Sprite number4Sprite;
    public Sprite number5Sprite;
    public Sprite number6Sprite;
    public Sprite trashBinSprite;
    public Sprite recycleBinSprite;
    public Sprite compostBinSprite;
    public Sprite trashSprite;
    public Sprite recycleSprite;
    public Sprite compostSprite;

    private bool spawnTrash;
    public GameObject[] trashes;

    private bool setBoard;
    public GameObject[] trashbins;
    private int activeBins;

    private bool checkBoard;

    public GameObject rollingUI;

    private bool playCheer;

    public AudioClip bellSound;

    // Use this for initialization
    void Start()
    {
        alien.SetActive(false);
        currentRound = 1;
        diceRoll = true;
        spawnTrash = false;
        setBoard = false;
        checkBoard = false;
        playCheer = true;
        for (int i = 0; i < 6; ++i)
        {
            trashes[i].SetActive(false);
        }
        for (int i = 0; i < 3; ++i)
        {
            trashbins[i].SetActive(false);
        }
        activeBins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentRound == rounds && playCheer)
        {
            StartCoroutine(FinishPhase());
            playCheer = false;
        }
        if (diceRoll)
        {
            StartCoroutine(NumberGenerator());
            diceRoll = false;
        }
        if (spawnTrash)
        {
            spawnTrashes();
        }
        if (setBoard)
        {
            StartCoroutine(SpawnBoard());
            setBoard = false;
        }
        if (checkBoard)
        {
            checkRound();
        }
    }

    IEnumerator NumberGenerator()
    {
        number.SetActive(false);
        dice.GetComponent<SpriteRenderer>().sprite = dice1Sprite;

        float seed = UnityEngine.Random.Range(0.0f, 1.0f);
        if (seed <= 0.5f)
        {
            GetComponent<AudioSource>().PlayOneShot(tryNewDiceRollSound);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(tryNewNumberSound);
        }

        seed = UnityEngine.Random.Range(0.0f, 1.0f);
        if (seed <= 0.5f)
        {
            GetComponent<AudioSource>().PlayOneShot(rollingSound1);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(rollingSound2);
        }

        yield return new WaitForSeconds(2.35f);
        number.SetActive(true);
        switch (currentRound)
        {
            case 1:
            case 2:
                diceRollNumber = Random.Range(1, 4);
                break;
            case 3:
            case 4:
                diceRollNumber = Random.Range(2, 5);
                break;
            case 5:
            case 6:
                diceRollNumber = Random.Range(4, 7);
                break;
            default:
                diceRollNumber = Random.Range(1, 7);
                break;
        }
        switch (diceRollNumber)
        {
            case 1:
                dice.GetComponent<SpriteRenderer>().sprite = dice1Sprite;
                number.GetComponent<SpriteRenderer>().sprite = number1Sprite;
                break;
            case 2:
                dice.GetComponent<SpriteRenderer>().sprite = dice2Sprite;
                number.GetComponent<SpriteRenderer>().sprite = number2Sprite;
                break;
            case 3:
                dice.GetComponent<SpriteRenderer>().sprite = dice3Sprite;
                number.GetComponent<SpriteRenderer>().sprite = number3Sprite;
                break;
            case 4:
                dice.GetComponent<SpriteRenderer>().sprite = dice4Sprite;
                number.GetComponent<SpriteRenderer>().sprite = number4Sprite;
                break;
            case 5:
                dice.GetComponent<SpriteRenderer>().sprite = dice5Sprite;
                number.GetComponent<SpriteRenderer>().sprite = number5Sprite;
                break;
            case 6:
                dice.GetComponent<SpriteRenderer>().sprite = dice6Sprite;
                number.GetComponent<SpriteRenderer>().sprite = number6Sprite;
                break;
        }
        spawnTrash = true;
    }

    void spawnTrashes()
    {
        for (int i = 0; i < 6; ++i)
        {
            if (i < diceRollNumber)
            {
                trashes[i].SetActive(true);
                trashes[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                trashes[i].GetComponent<BoxCollider>().enabled = true;
                trashes[i].transform.position = new Vector3(trashes[i].transform.position.x, trashes[i].transform.position.y, 0f);
            }
            else
            {
                trashes[i].SetActive(false);
            }
        }
        spawnTrash = false;
        setBoard = true;
    }

    IEnumerator SpawnBoard()
    {
        switch (diceRollNumber)
        {
            case 1:
                spawnBoardDiceRoll1();
                break;
            case 2:
                spawnBoardDiceRoll2();
                break;
            case 3:
                spawnBoardDiceRoll3();
                break;
            case 4:
                spawnBoardDiceRoll4();
                break;
            case 5:
                spawnBoardDiceRoll5();
                break;
            case 6:
                spawnBoardDiceRoll6();
                break;
        }
        spawnTrashBins();
        yield return new WaitForSeconds(1.5f);
        rollingUI.SetActive(false);
        checkBoard = true;
    }

    void checkRound()
    {
        bool roundComplete = true;
        for(int i = 0; i < trashes.Length; ++i)
        {
            if (trashes[i].activeInHierarchy)
            {
                roundComplete = false;
                break;
            }
        }
        if (roundComplete)
        {
            GetComponent<AudioSource>().PlayOneShot(bellSound);
            checkBoard = false;
            StartCoroutine(readyForNextRound());
        }
    }

    IEnumerator readyForNextRound()
    {
        if (++currentRound != rounds)
        {
            float seed = UnityEngine.Random.Range(0.0f, 1.0f);
            for (int i = 0; i < 3; ++i)
            {
                trashbins[i].SetActive(false);
            }
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
            checkBoard = false;

            trashes[0].transform.position = new Vector3(-4.1f, -1.16f, 0f);
            trashes[1].transform.position = new Vector3(-4.1f, -3.38f, 0f);
            trashes[2].transform.position = new Vector3(-1.48f, -1.16f, 0f);
            trashes[3].transform.position = new Vector3(-1.48f, -3.38f, 0f);
            trashes[4].transform.position = new Vector3(1.14f, -1.16f, 0f);
            trashes[5].transform.position = new Vector3(1.14f, -3.38f, 0f);
            rollingUI.SetActive(true);
            diceRoll = true;
            }
    }

    void spawnBoardDiceRoll1()
    {
        TrashSpawns(1, trashSprite);
    }

    void spawnBoardDiceRoll2()
    {
        int seed = Random.Range(1, 3);

        switch (seed)
        {
            case 1:
                TrashSpawns(2, trashSprite);
                break;
            case 2:
                TrashSpawns(2, recycleSprite);
                break;
        }
    }

    void spawnBoardDiceRoll3()
    {
        int seed = Random.Range(1, 7);

        switch (seed)
        {
            case 1:
                TrashSpawns(3, trashSprite);
                break;
            case 2:
            case 3:
                TrashSpawns(1, trashSprite);
                trashes[1].SetActive(true);
                trashes[2].SetActive(true);
                trashes[1].GetComponent<SpriteRenderer>().sprite = recycleSprite;
                trashes[1].tag = "Recycle";
                trashes[2].GetComponent<SpriteRenderer>().sprite = recycleSprite;
                trashes[2].tag = "Recycle";
                break;
            case 4:
            case 5:
            case 6:
                TrashSpawns(3, compostSprite);
                break;
        }
    }

    void spawnBoardDiceRoll4()
    {
        int seed = Random.Range(1, 8);

        switch (seed)
        {
            case 1:
                TrashSpawns(4, trashSprite);
                break;
            case 2:
            case 3:
            case 4:
                int seed2 = Random.Range(1, 3);
                switch (seed2)
                {
                    case 1:
                        TrashSpawns(2, trashSprite);
                        trashes[2].SetActive(true);
                        trashes[3].SetActive(true);
                        trashes[2].GetComponent<SpriteRenderer>().sprite = recycleSprite;
                        trashes[2].tag = "Recycle";
                        trashes[3].GetComponent<SpriteRenderer>().sprite = recycleSprite;
                        trashes[3].tag = "Recycle";
                        break;
                    case 2:
                        TrashSpawns(4, recycleSprite);
                        break;
                }
                break;
            case 5:
            case 6:
            case 7:
                TrashSpawns(3, compostSprite);
                trashes[3].SetActive(true);
                trashes[3].GetComponent<SpriteRenderer>().sprite = trashSprite;
                trashes[3].tag = "Trash";
                break;
        }
    }

    void spawnBoardDiceRoll5()
    {
        int seed = Random.Range(1, 12);

        switch (seed)
        {
            case 1:
                TrashSpawns(5, trashSprite);
                break;
            case 2:
            case 3:
            case 4:
            case 5:
                int seed2 = Random.Range(1, 3);
                switch (seed2)
                {
                    case 1:
                        TrashSpawns(4, recycleSprite);
                        trashes[4].SetActive(true);
                        trashes[4].GetComponent<SpriteRenderer>().sprite = trashSprite;
                        trashes[4].tag = "Trash";
                        break;
                    case 2:
                        int seed3 = Random.Range(1, 3);
                        switch (seed3)
                        {
                            case 1:
                                TrashSpawns(3, compostSprite);
                                trashes[3].SetActive(true);
                                trashes[4].SetActive(true);
                                trashes[3].GetComponent<SpriteRenderer>().sprite = recycleSprite;
                                trashes[3].tag = "Recycle";
                                trashes[4].GetComponent<SpriteRenderer>().sprite = recycleSprite;
                                trashes[4].tag = "Recycle";
                                break;
                            case 2:
                                TrashSpawns(3, trashSprite);
                                trashes[3].SetActive(true);
                                trashes[4].SetActive(true);
                                trashes[3].GetComponent<SpriteRenderer>().sprite = recycleSprite;
                                trashes[3].tag = "Recycle";
                                trashes[4].GetComponent<SpriteRenderer>().sprite = recycleSprite;
                                trashes[4].tag = "Recycle";
                                break;
                        }
                        break;
                }
                break;
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
                TrashSpawns(3, compostSprite);
                trashes[3].SetActive(true);
                trashes[4].SetActive(true);
                trashes[3].GetComponent<SpriteRenderer>().sprite = trashSprite;
                trashes[3].tag = "Trash";
                trashes[4].GetComponent<SpriteRenderer>().sprite = trashSprite;
                trashes[4].tag = "Trash";
                break;
        }
    }

    void spawnBoardDiceRoll6()
    {
        int seed = Random.Range(1, 9);

        switch (seed)
        {
            case 1:
                TrashSpawns(6, trashSprite);
                break;
            case 2:
            case 3:
            case 4:
                int seed2 = Random.Range(1, 3);
                switch (seed2)
                {
                    case 1:
                        TrashSpawns(6, recycleSprite);
                        break;
                    case 2:
                        int seed3 = Random.Range(1, 4);
                        switch (seed3)
                        {
                            case 1:
                                TrashSpawns(4, recycleSprite);
                                trashes[4].SetActive(true);
                                trashes[4].GetComponent<SpriteRenderer>().sprite = trashSprite;
                                trashes[4].tag = "Trash";
                                trashes[5].SetActive(true);
                                trashes[5].GetComponent<SpriteRenderer>().sprite = trashSprite;
                                trashes[5].tag = "Trash";
                                break;
                            case 2:
                                TrashSpawns(4, trashSprite);
                                trashes[4].SetActive(true);
                                trashes[5].SetActive(true);
                                trashes[4].GetComponent<SpriteRenderer>().sprite = recycleSprite;
                                trashes[4].tag = "Recycle";
                                trashes[5].GetComponent<SpriteRenderer>().sprite = recycleSprite;
                                trashes[5].tag = "Recycle";
                                break;
                            case 3:
                                TrashSpawns(3, compostSprite);
                                trashes[3].SetActive(true);
                                trashes[3].GetComponent<SpriteRenderer>().sprite = recycleSprite;
                                trashes[3].tag = "Recycle";
                                trashes[4].SetActive(true);
                                trashes[4].GetComponent<SpriteRenderer>().sprite = recycleSprite;
                                trashes[4].tag = "Recycle";
                                trashes[5].SetActive(true);
                                trashes[5].GetComponent<SpriteRenderer>().sprite = trashSprite;
                                trashes[5].tag = "Trash";
                                break;
                        }
                        break;
                }
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                int seed4 = Random.Range(1, 4);
                switch (seed4)
                {
                    case 1:
                    case 2:
                        TrashSpawns(6, compostSprite);
                        break;
                    case 3:
                        TrashSpawns(3, trashSprite);
                        trashes[3].SetActive(true);
                        trashes[4].SetActive(true);
                        trashes[5].SetActive(true);
                        trashes[3].GetComponent<SpriteRenderer>().sprite = compostSprite;
                        trashes[3].tag = "Compost";
                        trashes[4].GetComponent<SpriteRenderer>().sprite = compostSprite;
                        trashes[4].tag = "Compost";
                        trashes[5].GetComponent<SpriteRenderer>().sprite = compostSprite;
                        trashes[5].tag = "Compost";
                        break;
                }
                break;
        }
    }

    void TrashSpawns(int numberOfTrashes, Sprite typeSprite)
    {
        for (int i = 0; i < 6; ++i)
        {
            if (i < numberOfTrashes)
            {
                trashes[i].SetActive(true);
                trashes[i].GetComponent<SpriteRenderer>().sprite = typeSprite;
                if(typeSprite == trashSprite)
                {
                    trashes[i].tag = "Trash";
                }
                else if (typeSprite == recycleSprite)
                {
                    trashes[i].tag = "Recycle";
                }
                else if (typeSprite == compostSprite)
                {
                    trashes[i].tag = "Compost";
                }
            }
            else
            {
                trashes[i].SetActive(false);
            }
        }
    }

    void spawnTrashBins()
    {
        activeBins = 0;
        bool trashFlag = false;
        bool recycleFlag = false;
        bool compostFlag = false;
        for (int i = 0; i < diceRollNumber; ++i)
        {
            if(trashes[i].GetComponent<SpriteRenderer>().sprite == trashSprite)
            {
                trashFlag = true;
            }
            else if (trashes[i].GetComponent<SpriteRenderer>().sprite == recycleSprite)
            {
                recycleFlag = true;
            }
            else if (trashes[i].GetComponent<SpriteRenderer>().sprite == compostSprite)
            {
                compostFlag = true;
            }
        }
        if (trashFlag)
        {
            trashbins[0].SetActive(true);
            trashbins[0].GetComponent<SpriteRenderer>().sprite = trashBinSprite;
            ++activeBins;
        }

        if (recycleFlag)
        {
            trashbins[1].SetActive(true);
            trashbins[1].GetComponent<SpriteRenderer>().sprite = recycleBinSprite;
            ++activeBins;
        }

        if (compostFlag)
        {
            trashbins[2].SetActive(true);
            trashbins[2].GetComponent<SpriteRenderer>().sprite = compostBinSprite;
            ++activeBins;
        }

        switch (activeBins)
        {
            case 1:
                trashbins[0].SetActive(false);
                trashbins[1].SetActive(true);
                trashbins[2].SetActive(false);
                if (trashFlag)
                {
                    trashbins[1].GetComponent<SpriteRenderer>().sprite = trashBinSprite;
                }
                else if (recycleFlag)
                {
                    trashbins[1].GetComponent<SpriteRenderer>().sprite = recycleBinSprite;
                }
                else if (compostFlag)
                {
                    trashbins[1].GetComponent<SpriteRenderer>().sprite = compostBinSprite;
                }
                break;
            case 2:
                trashbins[0].SetActive(true);
                trashbins[1].SetActive(true);
                trashbins[2].SetActive(false);
                trashbins[0].transform.position = new Vector3(6.36f, 2f, 0f);
                trashbins[1].transform.position = new Vector3(6.36f, -2f, 0f);
                if (trashFlag && recycleFlag)
                {
                    trashbins[0].GetComponent<SpriteRenderer>().sprite = trashBinSprite;
                    trashbins[1].GetComponent<SpriteRenderer>().sprite = recycleBinSprite;
                } else if (recycleFlag && compostFlag)
                {
                    trashbins[0].GetComponent<SpriteRenderer>().sprite = recycleBinSprite;
                    trashbins[1].GetComponent<SpriteRenderer>().sprite = compostBinSprite;
                }
                else if (trashFlag && compostFlag)
                {
                    trashbins[0].GetComponent<SpriteRenderer>().sprite = trashBinSprite;
                    trashbins[1].GetComponent<SpriteRenderer>().sprite = compostBinSprite;
                }
                break;
        }
    }

    IEnumerator FinishPhase()
    {
        rollingUI.SetActive(false);
        for (int i = 0; i < 3; ++i)
        {
            trashbins[i].SetActive(false);
        }
        alien.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(finishSound);
        yield return new WaitForSeconds(finishSound.length);
        SceneManager.LoadScene(20);
    }
}
