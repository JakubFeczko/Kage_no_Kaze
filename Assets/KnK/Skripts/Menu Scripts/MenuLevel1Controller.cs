using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRUIP;
using TMPro;

public class MenuLevel1Controller : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SwitchController themeSwitch;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject navigationPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject typePanel;
    [SerializeField] private GameObject welcomePanel;
    [SerializeField] private ButtonController menuButton;
    [SerializeField] private ButtonController quitButton;
    [SerializeField] private ButtonController exchangeXpButton;
    [SerializeField] private ButtonController mainMenuButton;
    [SerializeField] private IconController settingsIcon;
    [SerializeField] private TextController currentDemoTitle;
    [SerializeField] private TMP_Text xpPointText;
    [SerializeField] PlayerStats playerStats;

    [Header("Sections")]
    [SerializeField] private MenuSection menuSections;
    [SerializeField] private MenuSection quitSections;
    [SerializeField] private MenuSection exchangeXpSections;

    private Transform _cameraTransform;
    private DemoType currentDemoType;

    private void Awake()
    {
        Setup();
    }

    private void Start()
    {
        themeSwitch.RegisterOnValueChanged(ChangeTheme);
        if (!VRUIPManager.instance.IsVR) return;
        _cameraTransform = VRUIPManager.instance.mainCamera.transform;
        StartCoroutine(SetDemoPosition());
    }

    private void Setup()
    {
        menuButton.RegisterOnClick(() => StartMenu(DemoType.Menu));
        quitButton.RegisterOnClick(() => StartMenu(DemoType.Quit));
        exchangeXpButton.RegisterOnClick(() => StartMenu(DemoType.ExchangeXp));

        settingsIcon.RegisterOnClick(OnSettingsButtonClicked);
        mainMenuButton.RegisterOnClick(OnMainMenuButtonClicked);
    }

    private void OnMainMenuButtonClicked()
    {
        navigationPanel.SetActive(false);
        typePanel.SetActive(true);
        welcomePanel.SetActive(true);
        settingsPanel.SetActive(false);

        if (currentDemoType == DemoType.Menu)
        {
            menuSections.HideSection();
        }
        else if (currentDemoType == DemoType.Quit)
        {
            quitSections.HideSection();
        }
        else if( currentDemoType == DemoType.ExchangeXp)
        {
            exchangeXpSections.HideSection();
        }

    }

    public void UpdateXpPointText()
    {
        xpPointText.text = playerStats.GetCurrentXpPoint().ToString();
    }

    private void OnSettingsButtonClicked()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }

    /// <summary>
    /// Start a demo of a certain type.
    /// </summary>
    /// <param name="type"></param>
    public void StartMenu(DemoType type)
    {
        welcomePanel.SetActive(false);
        typePanel.SetActive(false);
        navigationPanel.SetActive(true);
        switch (type)
        {
            case DemoType.Menu:
                menuSections.ShowSection();
                currentDemoTitle.Text = menuSections.name;
                currentDemoType = DemoType.Menu;
                break;
            case DemoType.Quit:
                quitSections.ShowSection();
                currentDemoTitle.Text = quitSections.name;
                currentDemoType = DemoType.Quit;
                break;
            case DemoType.ExchangeXp:
                xpPointText.text = playerStats.GetCurrentXpPoint().ToString();
                exchangeXpSections.ShowSection();
                currentDemoTitle.Text = exchangeXpSections.name;
                currentDemoType= DemoType.ExchangeXp;
                break;
        }
    }



    private void ChangeTheme(bool isLightMode)
    {
        VRUIPManager.instance.colorMode = isLightMode ? VRUIPManager.ColorThemeMode.LightMode : VRUIPManager.ColorThemeMode.DarkMode;
        VRUIPManager.instance.SetTheme();
    }

    private IEnumerator SetDemoPosition()
    {
        while (_cameraTransform.position.y == 0)
        {
            yield return null;
        }
        container.SetY(_cameraTransform.position.y - .1f);
    }

    public enum DemoType
    {
        Menu,
        Quit,
        ExchangeXp
    }
}


