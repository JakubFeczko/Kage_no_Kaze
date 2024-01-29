using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject options;

    [Header("Main Manu Buttons")]
    public Button startBtn;
    public Button continueBtn;
    public Button optionsBtn;
    public Button quitBtn;

    public List<Button> returnBtns;
    void Start()
    {

        EnableMainMenu();

        startBtn.onClick.AddListener(StartGame);
        continueBtn.onClick.AddListener(ContinueGame);
        optionsBtn.onClick.AddListener(EnableOptions);
        quitBtn.onClick.AddListener(QuitGame);


        foreach (var item in returnBtns)
        {
            item.onClick.AddListener(EnableMainMenu);
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    public void ContinueGame()
    {

    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }

    public void EnableOptions()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }
}
