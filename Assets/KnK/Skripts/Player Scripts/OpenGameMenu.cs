using HurricaneVR.Framework.ControllerInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the opening and closing of the game menu in response to player input.
/// </summary>
public class OpenGameMenu : MonoBehaviour
{
    /// <summary>
    /// The GameObject representing the game menu.
    /// </summary>
    public GameObject gameMenu;

    /// <summary>
    /// Update is called once per frame to check for player input to toggle the game menu.
    /// </summary>
    void Update()
    {
        // Check if the secondary button on the left controller is just activated.
        if (HVRInputManager.Instance.LeftController.SecondaryButtonState.JustActivated)
        {
            if (gameMenu.activeSelf){gameMenu.SetActive(false);}
            else{gameMenu.SetActive(true);}
        }
    }
}
