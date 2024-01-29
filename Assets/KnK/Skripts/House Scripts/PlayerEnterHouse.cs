using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Handles the event when the player enters the house
/// </summary>
public class PlayerEnterHouse : MonoBehaviour
{
    /// <summary>
    /// PlayableDirector for controlling timelines
    /// </summary>
    public PlayableDirector director;

    /// <summary>
    /// Flag to indicate whether the player has already entered the house.
    /// </summary>
    public bool playerWasEntered = false;

    /// <summary>
    /// Detects when a collider enters the trigger zone.
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone</param>
    private void OnTriggerEnter(Collider other)
    {
        if (director != null && other.gameObject.tag == "Player" && !playerWasEntered)
        {
            director.Play();
            playerWasEntered=true;
        }
    }
}
