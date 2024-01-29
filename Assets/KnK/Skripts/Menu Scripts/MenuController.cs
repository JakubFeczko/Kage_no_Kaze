using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRUIP.Demo;
using VRUIP;
using System;
using UnityEditor.Rendering;
using HurricaneVR.Framework.Core.Player;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Linq;
using HurricaneVR.Framework.ControllerInput;

public class MenuController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SwitchController themeSwitch;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject navigationPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject typePanel;
    [SerializeField] private GameObject welcomePanel;
    [SerializeField] private ButtonController menuButton;
    [SerializeField] private ButtonController trophiesButton;
    [SerializeField] private ButtonController quitButton;
    [SerializeField] private ButtonController mainMenuButton;
    [SerializeField] private IconController settingsIcon;
    [SerializeField] private TextController currentDemoTitle;

    [Header("Sections")]
    [SerializeField] private MenuSection menuSections;
    [SerializeField] private MenuSection trophiesSections;
    [SerializeField] private MenuSection quitSections;

    [Header("Player Settings")]
    [SerializeField] private PlayerControlerOptions playerControlerOptions;

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
        trophiesButton.RegisterOnClick(() => StartMenu(DemoType.Trophies));
        quitButton.RegisterOnClick(() => StartMenu(DemoType.Quit));
        settingsIcon.RegisterOnClick(OnSettingsButtonClicked);
        mainMenuButton.RegisterOnClick(OnMainMenuButtonClicked);
        if (!playerControlerOptions.Player)
        {
            playerControlerOptions.Player = GameObject.FindObjectsOfType<HVRPlayerController>().FirstOrDefault(e => e.gameObject.activeInHierarchy);
        }
        if (playerControlerOptions.Player)
        {

            if (!playerControlerOptions.CameraRig)
            {
                playerControlerOptions.CameraRig = playerControlerOptions.Player.GetComponentInChildren<HVRCameraRig>();
            }

            if (!playerControlerOptions.Inputs)
            {
                playerControlerOptions.Inputs = playerControlerOptions.Player.GetComponentInChildren<HVRPlayerInputs>();
            }

        }

        if (playerControlerOptions.LeftHand) playerControlerOptions.leftparent = playerControlerOptions.LeftHand.transform.parent;
        if (playerControlerOptions.RightHand) playerControlerOptions.rightParent = playerControlerOptions.RightHand.transform.parent;

        playerControlerOptions.SmoothTurnToggle.isOn = playerControlerOptions.Player.RotationType == RotationType.Smooth;

        playerControlerOptions.TurnRateSlider.onValueChanged.AddListener(playerControlerOptions.OnTurnRateChanged);
        playerControlerOptions.SnapTurnSlider.onValueChanged.AddListener(playerControlerOptions.OnSnapTurnRateChanged);
        playerControlerOptions.SmoothTurnToggle.onValueChanged.AddListener(playerControlerOptions.OnSmoothTurnChanged);
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
        else if (currentDemoType == DemoType.Trophies)
        {
            trophiesSections.HideSection();
        }
        else if (currentDemoType == DemoType.Quit)
        {
            quitSections.HideSection();
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
            case DemoType.Trophies:
                trophiesSections.ShowSection();
                currentDemoTitle.Text = trophiesSections.name;
                currentDemoType = DemoType.Trophies;
                break;
            case DemoType.Quit:
                quitSections.ShowSection();
                currentDemoTitle.Text = quitSections.name;
                currentDemoType = DemoType.Quit;
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
        Trophies,
        Quit
    }

    public void CalibrateHeight()
    {
        if (playerControlerOptions.CameraRig)
            playerControlerOptions.CameraRig.Calibrate();
    }

    public void OnSitStandClicked()
    {
        var index = (int)playerControlerOptions.CameraRig.SitStanding;
        index++;
        if (index > 2)
        {
            index = 0;
        }

        playerControlerOptions.CameraRig.SetSitStandMode((HVRSitStand)index);
        playerControlerOptions.UpdateSitStandButton();
    }
}

[Serializable]
class MenuSection
{
    public string name;
    public A_Canvas[] canvases;
    public GameObject[] objects;

    public void HideSection(Action callback = null)
    {
        foreach (var canvas in canvases)
        {
            if (canvas.gameObject.activeInHierarchy) canvas.FadeOutCanvas(callback);
        }

        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
    }

    public void ShowSection(Action callback = null)
    {
        foreach (var canvas in canvases)
        {
            if (!canvas.IsSetup) canvas.Setup();
            canvas.FadeInCanvas(callback);
        }
        foreach (var obj in objects)
        {
            obj.SetActive(true);
        }
    }

    public void HideOnStart()
    {
        foreach (var canvas in canvases)
        {
            canvas.SetAlpha(0);
            canvas.gameObject.SetActive(false);
        }
    }
}

[Serializable]

class PlayerControlerOptions
{
    public HVRCameraRig CameraRig;
    public TextMeshProUGUI SitStandText;
    public Toggle SmoothTurnToggle;
    public Slider TurnRateSlider;
    public Slider SnapTurnSlider;
    public HVRPlayerController Player;
    public HVRPlayerInputs Inputs;
    public HVRJointHand LeftHand;
    public HVRJointHand RightHand;
    public Transform leftparent;
    public Transform rightParent;
    public TextMeshProUGUI TurnRateText;
    public TextMeshProUGUI SnapRateText;

    public void UpdateSitStandButton()
    {
        SitStandText.text = CameraRig.SitStanding.ToString();
    }

    public void OnTurnRateChanged(float rate)
    {
        Player.SmoothTurnSpeed = rate;
        TurnRateText.text = Player.SmoothTurnSpeed.ToString();
    }

    public void OnSnapTurnRateChanged(float rate)
    {
        Player.SnapAmount = rate;
        SnapRateText.text = Player.SnapAmount.ToString();
    }

    public void OnSmoothTurnChanged(bool smooth)
    {
        Player.RotationType = smooth ? RotationType.Smooth : RotationType.Snap;
    }
}