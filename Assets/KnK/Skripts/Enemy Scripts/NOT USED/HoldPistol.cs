using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPistol : MonoBehaviour
{
    public FullBodyBipedIK iK;

    public Transform leftHandTarget;

    private void LateUpdate()
    {
        iK.solver.leftHandEffector.position = leftHandTarget.position;
        iK.solver.leftHandEffector.rotation = leftHandTarget.rotation;
        iK.solver.leftHandEffector.positionWeight = 1f;
        iK.solver.leftHandEffector.rotationWeight = 1f;
    }
}
