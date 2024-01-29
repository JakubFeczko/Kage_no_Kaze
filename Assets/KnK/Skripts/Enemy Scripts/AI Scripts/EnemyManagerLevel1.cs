using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Manages enemy behavior and interactions for the first level.
/// </summary>
public class EnemyManagerLevel1 : MonoBehaviour
{
    /// <summary>
    /// Array of AI agents representing the enemies.
    /// </summary>
    public AIAgent[] aiEnemy;

    /// <summary>
    /// PlayableDirector for controlling timelines.
    /// </summary>
    public PlayableDirector director;
    private bool fightStarted = false;

    /// <summary>
    /// AudioSource component for playing sounds.
    /// </summary>
    public new AudioSource audio;
    public float fadeOutDuration = 1f;

    [Header("Samurai Enemy")]
    public AIAgent samuraiEnemy;

    /// <summary>
    /// GameObject that is activated when the fight is finished.
    /// </summary>
    public GameObject fightFinish;

    private bool wasEnded = false;


    /// <summary>
    /// Initialize components on start.
    /// </summary>
    private void Start()
    {
        director = GetComponent<PlayableDirector>();
        audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Update is called once per frame to check enemy states and trigger events.
    /// </summary>
    private void Update()
    {
        if(aiEnemy.Length != 0)
        {
            shouldShoot();
        }
        if (CheckSomeoneAlive() && !wasEnded)
        {
            fightFinish.SetActive(true);
            wasEnded = true;
        }
    }

    /// <summary>
    /// Checks if any enemy should start shooting.
    /// </summary>
    private void shouldShoot()
    {
        foreach (var ai in aiEnemy)
        {
            if (ai != null && ai.stateMachine != null)
            {
                if (ai.stateMachine.currentState == AIStateId.Shoot && fightStarted == false)
                {
                    fightStarted = true;
                    ChangeEnemyState(aiEnemy);
                    if (director != null && audio != null)
                    {
                        director.Play();
                    }

                }
            }
        }
    }

    /// <summary>
    /// Changes the state of all enemies to shoot state.
    /// </summary>
    /// <param name="aIAgents"></param>
    private void ChangeEnemyState(AIAgent[] aIAgents)
    {
        foreach(var ai in aiEnemy)
        {
            ai.stateMachine.ChangeState(AIStateId.Shoot);
        }
    }

    /// <summary>
    /// Checks if any of the enemies are still alive.
    /// </summary>
    /// <returns>True if all enemies are dead, otherwise false.</returns>
    private bool CheckSomeoneAlive()
    {
        foreach (var ai in aiEnemy)
        {
            if ( ai.enabled && ai.stateMachine.currentState != AIStateId.Death ) { return false; };
        }
        return true;
    }
}
