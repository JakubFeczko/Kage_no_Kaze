using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/// <summary>
/// Manages the behavior and animations of an elevator and its associated hidden door.
/// </summary>
public class ElevatorManager : MonoBehaviour
{
    /// <summary>
    /// Animator component controlling the elevator's animations.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Animator component controlling the hidden door's animations.
    /// </summary>
    public Animator hidenDoorAnimator;

    /// <summary>
    /// Initialize the animator component on start.
    /// </summary>
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Toggles the state of the elevator and hidden door.
    /// </summary>
    public void SetElevatorState()
    {
        if (animator != null && hidenDoorAnimator != null)
        {
            bool downFirst = animator.GetBool("Down First");
            bool isDown = animator.GetBool("Down");

            if (!downFirst)
            {
                animator.SetBool("Down First", true);
                animator.SetBool("Down", true);

            }
            else
            {
                if (isDown)
                {
                    animator.SetBool("Up", true);
                    animator.SetBool("Down", false);

                    hidenDoorAnimator.SetBool("Close", false);
                    hidenDoorAnimator.SetBool("Open", true);
                }
                else
                {
                    animator.SetBool("Down", true);
                    animator.SetBool("Up", false);

                    hidenDoorAnimator.SetBool("Open", false);
                    hidenDoorAnimator.SetBool("Close", true);
                }
            }
        }
        
    }
}
