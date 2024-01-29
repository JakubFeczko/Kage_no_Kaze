using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDoorState()
    {
        bool firstOpen = animator.GetBool("Open First");
        bool isOpen = animator.GetBool("Open");

        if (!firstOpen)
        {
            animator.SetBool("Open First", true);
            animator.SetBool("Open", true);
        }
        else
        {
            if (isOpen)
            {
                animator.SetBool("Close", true);
                animator.SetBool("Open", false);
            }
            else
            {
                animator.SetBool("Open", true);
                animator.SetBool("Close", false);
            }
        }
    }
}
