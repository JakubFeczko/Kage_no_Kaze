using AdvancedSceneManager.Callbacks;
using AdvancedSceneManager.Defaults;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadingScreenOverride : LoadingScreen
{
    public Slider slider;

    public override IEnumerator OnClose()
    {
        if (slider)
        {
            slider.gameObject.SetActive(false);
        }
        yield return null;
    }

    public override IEnumerator OnOpen()=>null;

    public override void OnProgressChanged(float progress)
    {
        if (slider) slider.value = progress;
    }


}
