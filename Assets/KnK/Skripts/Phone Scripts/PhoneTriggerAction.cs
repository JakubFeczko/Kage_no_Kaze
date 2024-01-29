using HurricaneVR.Framework.ControllerInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the interactions and actions triggered by the phone in the game.
/// </summary>
public class PhoneTriggerAction : MonoBehaviour
{
    /// <summary>
    /// Array of AudioSource components associated with the phone.
    /// </summary>
    public new AudioSource[] audio; 
    private bool _wasPlayed = false; // Flag to check if the audio was played.

    /// <summary>
    /// Animator for the chest interaction.
    /// </summary>
    public Animator chectAnimator;

    /// <summary>
    /// Particle system for explosion effect.
    /// </summary>
    public ParticleSystem particleSystemExplosion;

    /// <summary>
    /// // MeshRenderer for the phone.
    /// </summary>
    private MeshRenderer _phoneMesh; 
    private bool _wasDestroyed = false;
    [SerializeField]
    private int levelNumber = 2;

    /// <summary>
    /// Reference to the player's health script.
    /// </summary>
    public PlayerHealth playerHealth;

    /// <summary>
    /// Initialize components at the start of the game.
    /// </summary>
    public void Start()
    {
        audio = GetComponentsInChildren<AudioSource>();
        _phoneMesh = GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// Update is called once per frame to handle the phone's interaction logic.
    /// </summary>
    private void Update()
    {
        // Check if the right controller's trigger button is just activated and the audio hasn't been played.
        if (HVRInputManager.Instance.RightController.TriggerButtonState.JustActivated && !_wasPlayed)
        {
            if(audio.Length > 1 && !audio[1].isPlaying)
            {
                audio[1].Play();
                _wasPlayed = true;
            }

        }

        // Perform actions after the audio has played and for specific level conditions.
        if (!audio[1].isPlaying && _wasPlayed && levelNumber == 2 && !_wasDestroyed)
        {
            particleSystemExplosion.Play();
            _phoneMesh.enabled = false;
            _wasDestroyed = true;
            playerHealth.TakeDamage(10);
        }

        // Additional actions if the chest animator is present and the phone has been interacted with.
        if (HVRInputManager.Instance.RightController.TriggerButtonState.JustActivated && _wasPlayed && !audio[1].isPlaying && chectAnimator != null && !_wasDestroyed)
        {
            chectAnimator.SetBool("Open", true);
            particleSystemExplosion.Play();
            audio[2].Play();
            _phoneMesh.enabled = false;
            _wasDestroyed = true;
            playerHealth.TakeDamage(10);
            
        }


    }
}
