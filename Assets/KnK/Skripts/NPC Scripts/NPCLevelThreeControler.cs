using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Controls the NPC behavior and interactions in level three, including wall animations.
/// </summary>
public class NPCLevelThreeControler : MonoBehaviour
{
    /// <summary>
    /// Transform component of the player.
    /// </summary>
    public Transform playerTransform;

    /// <summary>
    /// Speed at which the NPC rotates to face the player.
    /// </summary>
    public float rotationSpeed = 5f;

    /// <summary>
    /// PlayableDirector for controlling timelines.
    /// </summary>
    [SerializeField]
    private PlayableDirector _playableDirector;

    private bool _isPlaying = false;

    /// <summary>
    /// Animator component for the left wall.
    /// </summary>
    [SerializeField]
    private Animator leftWall;

    /// <summary>
    /// Animator component for the right wall.
    /// </summary>
    [SerializeField]
    private Animator rightWall;

    /// <summary>
    /// Initialize the PlayableDirector component on awake.
    /// </summary>
    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }

    /// <summary>
    /// Update is called once per frame to handle NPC rotation and wall animations.
    /// </summary>
    void Update()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0;

        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle > 60f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

        if(_playableDirector.state != PlayState.Playing && _isPlaying)
        {
            rightWall.SetBool("Open", true);
            leftWall.SetBool("Open", true);
        }
    }

    /// <summary>
    /// Triggers the cinematic sequence controlled by the PlayableDirector.
    /// </summary>
    public void PlayTimeline()
    {
        _playableDirector.Play();
        _isPlaying = true;
    }
}
