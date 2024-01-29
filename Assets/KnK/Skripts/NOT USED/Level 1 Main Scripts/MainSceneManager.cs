using HurricaneVR.Framework.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public HVRCanvasFade screenFade;

    void Start()
    {

        // Rozpocznij efekt rozjaœniania ekranu
        if (screenFade != null)
        {
            screenFade.UpdateFade(1f);
            screenFade.Fade(0f, screenFade.FadeInSpeed); // Fade do pe³nej widocznoœci
        }

    }
}
