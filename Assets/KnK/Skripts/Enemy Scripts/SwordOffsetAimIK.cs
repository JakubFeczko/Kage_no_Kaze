using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Adjusts the AimIK to target a specific position.
/// </summary>
public class SwordOffsetAimIK : MonoBehaviour
{
    /// <summary>
    /// Reference to the AimIK component, which is used for inverse kinematics calculations.
    /// </summary>
    public AimIK aimIK;

    /// <summary>
    /// The transform of the target that AimIK should point at.
    /// </summary>
    public Transform target;


    /// <summary>
    /// Called after all Update functions have been called. This is useful to override 
    /// animations that were calculated in Update.
    /// </summary>
    private void LateUpdate()
    {
        // Set the position for the AimIK solver to the target's position
        aimIK.solver.IKPosition = target.position ;
    }
}
