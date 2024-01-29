using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class AimHeroBody : MonoBehaviour
{
    public AimIK aimIK;
    public Transform target;

    private void LateUpdate()
    {
        aimIK.solver.IKPosition = target.position;
    }
}
