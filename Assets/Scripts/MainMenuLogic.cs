using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLogic : MonoBehaviour
{
    private GameObject MainMenu;

    public AudioClip levelOneSound;
    public AudioClip levelTwoSound;
    public AudioClip levelThreeSound;

    public Sprite imageTwoNormal;
    public Sprite imageThreeNormal;

    public GameObject levelOneButton;
    public GameObject levelOneImage;
    public GameObject levelTwoButton;
    public GameObject levelTwoImage;
    public GameObject levelThreeButton;
    public GameObject levelThreeImage;

    void Start()
    {
        MainMenu = GameObject.FindGameObjectWithTag("MainMenuLogic");
    }

    void Update()
    {
        switch (MainMenu.GetComponent<MainMenuData>().levelsUnlocked)
        {
            case 1:
                levelOneButton.GetComponent<Button>().interactable = true;
                levelOneImage.GetComponent<Button>().interactable = true;
                levelTwoButton.GetComponent<Button>().interactable = false;
                levelTwoImage.GetComponent<Button>().interactable = false;
                levelThreeButton.GetComponent<Button>().interactable = false;
                levelThreeImage.GetComponent<Button>().interactable = false;

                if (MainMenu.GetComponent<MainMenuData>().playUnlockLevelOneSound)
                {
                    GetComponent<AudioSource>().PlayOneShot(levelOneSound);
                    MainMenu.GetComponent<MainMenuData>().playUnlockLevelOneSound = false;
                }
                break;
            case 2:
                levelTwoButton.GetComponent<Button>().interactable = true;
                levelTwoImage.GetComponent<Button>().interactable = true;
                levelThreeButton.GetComponent<Button>().interactable = true;
                levelThreeImage.GetComponent<Button>().interactable = true;
                levelThreeButton.GetComponent<Button>().interactable = false;
                levelThreeImage.GetComponent<Button>().interactable = false;

                levelTwoImage.GetComponent<Image>().sprite = imageTwoNormal;

                if (MainMenu.GetComponent<MainMenuData>().playUnlockLevelTwoSound)
                {
                    GetComponent<AudioSource>().PlayOneShot(levelTwoSound);
                    MainMenu.GetComponent<MainMenuData>().playUnlockLevelTwoSound = false;
                }
                break;
            case 3:
                levelTwoButton.GetComponent<Button>().interactable = true;
                levelTwoImage.GetComponent<Button>().interactable = true;
                levelThreeButton.GetComponent<Button>().interactable = true;
                levelThreeImage.GetComponent<Button>().interactable = true;
                levelThreeButton.GetComponent<Button>().interactable = true;
                levelThreeImage.GetComponent<Button>().interactable = true;

                levelTwoImage.GetComponent<Image>().sprite = imageTwoNormal;
                levelThreeImage.GetComponent<Image>().sprite = imageThreeNormal;

                if (MainMenu.GetComponent<MainMenuData>().playUnlockLevelThreeSound)
                {
                    GetComponent<AudioSource>().PlayOneShot(levelThreeSound);
                    MainMenu.GetComponent<MainMenuData>().playUnlockLevelThreeSound = false;
                }
                break;
        }
    }

    public void LoadLevel(int scene)
    {
        DontDestroyOnLoad(MainMenu);
        SceneManager.LoadScene(scene);
    }
}
