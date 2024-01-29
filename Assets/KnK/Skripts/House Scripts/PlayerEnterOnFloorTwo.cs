using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Manages the event when the player enters the second floor.
/// </summary>
public class PlayerEnterOnFloorTwo : MonoBehaviour
{
    /// <summary>
    /// AudioSource component for playing sounds.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// GameObject representing a wall that should be deactivated when the player enters.
    /// </summary>
    public GameObject stopWall;

    /// <summary>
    /// Flag to indicate whether the player has already entered the second floor.
    /// </summary>
    private bool _playerWasEntered = false;

    /// <summary>
    /// Detects when a collider enters the trigger zone.
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (audioSource != null && other.gameObject.tag == "Player" && !_playerWasEntered)
        {
            audioSource.Play();
            stopWall.SetActive(false);
            _playerWasEntered = true;
        }
    }
}
