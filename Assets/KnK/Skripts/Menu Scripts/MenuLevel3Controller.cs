using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VRUIP;

public class MenuLevel3Controller : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SwitchController themeSwitch;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject navigationPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject typePanel;
    [SerializeField] private GameObject welcomePanel;
    [SerializeField] private ButtonController menuButton;
    [SerializeField] private ButtonController mainMenuButton;
    [SerializeField] private IconController settingsIcon;
    [SerializeField] private TextController currentDemoTitle;
    [SerializeField] PlayerStats playerStats;

    [Header("Sections")]
    [SerializeField] private MenuSection menuSections;

    private Transform _cameraTransform;
    private Level3 currentDemoType;

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
        menuButton.RegisterOnClick(() => StartMenu(Level3.Menu));

        settingsIcon.RegisterOnClick(OnSettingsButtonClicked);
        mainMenuButton.RegisterOnClick(OnMainMenuButtonClicked);
    }

    private void OnMainMenuButtonClicked()
    {
        navigationPanel.SetActive(false);
        typePanel.SetActive(true);
        welcomePanel.SetActive(true);
        settingsPanel.SetActive(false);

        if (currentDemoType == Level3.Menu)
        {
            menuSections.HideSection();
        }

    }

    private void OnSettingsButtonClicked()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }

    /// <summary>
    /// Start a demo of a certain type.
    /// </summary>
    /// <param name="type"></param>
    public void StartMenu(Level3 type)
    {
        welcomePanel.SetActive(false);
        typePanel.SetActive(false);
        navigationPanel.SetActive(true);
        switch (type)
        {
            case Level3.Menu:
                menuSections.ShowSection();
                currentDemoTitle.Text = menuSections.name;
                currentDemoType = Level3.Menu;
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

    public enum Level3
    {
        Menu,
        Quit,
        ExchangeXp
    }
}
