using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Player;
using HurricaneVR.Framework.Shared;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UINextSceneManager : MonoBehaviour
{
    public HVRPlayerController Player;
    public HVRCameraRig CameraRig;
    public HVRPlayerInputs Inputs;

    public HVRJointHand LeftHand;
    public HVRJointHand RightHand;

    private Transform leftparent;
    private Transform rightParent;

    public Button nextLevel;
    public SceneTransitionManager sceneManager;
    void Start()
    {
        if (!Player)
        {
            Player = GameObject.FindObjectsOfType<HVRPlayerController>().FirstOrDefault(e => e.gameObject.activeInHierarchy);
        }

        if (Player)
        {
            if (!CameraRig)
            {
                CameraRig = Player.GetComponentInChildren<HVRCameraRig>();
            }

            if (!Inputs)
            {
                Inputs = Player.GetComponentInChildren<HVRPlayerInputs>();
            }

            if (!LeftHand) LeftHand = Player.Root.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Left)?.GetComponent<HVRJointHand>();
            if (!RightHand) RightHand = Player.Root.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Right)?.GetComponent<HVRJointHand>();
        }


        if (LeftHand) leftparent = LeftHand.transform.parent;
        if (RightHand) rightParent = RightHand.transform.parent;

        nextLevel.onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        sceneManager.GoToScene(1);
    }
}
