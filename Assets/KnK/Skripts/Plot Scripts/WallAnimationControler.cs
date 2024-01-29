using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAnimationControler : MonoBehaviour
{
    private Animator wallAnimator;
    private AudioSource wallAudio;

    private bool isFadingOut = false;
    public float fadeOutSpeed = 0.5f;

    private void Awake()
    {
        wallAnimator = GetComponent<Animator>();
        wallAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isFadingOut)
        {
            if(wallAudio.volume > 0)
            {
                wallAudio.volume -= fadeOutSpeed * Time.deltaTime;
            }
            else
            {
                wallAudio.Stop();
                wallAudio.volume = 1f;
                isFadingOut=false;
            }
        }
    }

    public void StartWallAnimation()
    {
        if(wallAnimator != null)
        {
            bool isMoving = wallAnimator.GetBool("Move");
            wallAnimator.SetBool("Move", !isMoving);
        }
    }

    public void PlaySounds()
    {
        if(wallAudio != null) 
        {
            wallAudio.Play();
        }
    }

    public void StopSound()
    {
        if(wallAudio != null)
        {
            isFadingOut = true;
        }
    }
}
