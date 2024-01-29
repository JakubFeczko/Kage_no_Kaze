using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages enemy waves and their behavior for the second level.
/// </summary>
public class EnemyScriptLevel2 : MonoBehaviour
{
    /// <summary>
    /// Number of the current wave.
    /// </summary>
    public int waveNumber;

    /// <summary>
    /// Array of AI agents for the first wave of enemies.
    /// </summary>
    public AIAgent[] aiEnemyWaveOne;

    /// <summary>
    /// GameObject representing the first wave of enemies.
    /// </summary>
    public GameObject waveOne;

    /// <summary>
    /// GameObject representing the second wave of enemies.
    /// </summary>
    public GameObject waveTwo;

    /// <summary>
    /// GameObject representing the third wave of enemies.
    /// </summary>
    public GameObject waveThree;

    /// <summary>
    /// Array of AI agents for the second wave of enemies.
    /// </summary>
    public AIAgent[] aiEnemyWaveTwo;

    /// <summary>
    /// AudioSource component for playing sounds.
    /// </summary>
    public AudioSource audio;

    private bool _fightStartedOne = false;
    private bool _fightStartedTwo = false;

    /// <summary>
    /// GameObject that is activated when the fight is finished.
    /// </summary>
    public GameObject fightFinish;

    private bool _mapEneded = false;

    /// <summary>
    /// Initialize components on start.
    /// </summary>
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Update is called once per frame to check enemy states and trigger events.
    /// </summary>
    private void Update()
    {
        if(waveOne.activeSelf == true)
        {
            ShouldShoot(ref aiEnemyWaveOne, ref _fightStartedOne);
        }
        if(waveTwo.activeSelf == true && waveThree.activeSelf == true)
        {
            ShouldShoot(ref aiEnemyWaveTwo, ref _fightStartedTwo);
        }

        if ( _fightStartedOne && _fightStartedTwo && !_mapEneded)
        {
            fightFinish.SetActive(true);
            _mapEneded = true;
        }
    }

    /// <summary>
    /// Activates the specified wave of enemies.
    /// </summary>
    /// <param name="numberWave"></param>
    public void ActiveWave(int numberWave)
    {
        switch(numberWave)
        {
            case 1:
                waveOne.SetActive(true);
                break;
            case 2:
                waveTwo.SetActive(true);
                waveThree.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Checks if any enemy should start shooting based on the given wave.
    /// </summary>
    /// <param name="waveAgent">The array of AIAgents in the wave.</param>
    /// <param name="fightStart">Boolean indicating if the fight has started.</param>
    private void ShouldShoot(ref AIAgent[] waveAgent, ref bool fightStart)
    {
        foreach (var ai in waveAgent)
        {
            if (ai != null && ai.stateMachine != null)
            {
                if (ai.stateMachine.currentState == AIStateId.Shoot && fightStart == false)
                {
                    fightStart = true;
                    ChangeEnemyState(ref waveAgent);
                    if (audio != null)
                    {
                        audio.Play();
                    }

                }
            }
        }
    }

    /// <summary>
    /// Changes the state of all enemies in a wave to shoot state.
    /// </summary>
    /// <param name="waveAgent">Array of AIAgents to change state.</param>
    private void ChangeEnemyState(ref AIAgent[] waveAgent)
    {
        foreach (var ai in waveAgent)
        {
            ai.stateMachine.ChangeState(AIStateId.Shoot);
        }
    }

    /// <summary>
    /// Checks if any of the enemies in a wave are still alive.
    /// </summary>
    /// <param name="aIAgents">Array of AIAgents in the wave.</param>
    /// <returns>True if all enemies are dead, otherwise false.</returns>
    private bool CheckSomeoneAlive(ref AIAgent[] aIAgents )
    {
        if (aIAgents != null)
        {
            foreach (var ai in aIAgents)
            {
                if (ai != null && ai.stateMachine != null  && ai.stateMachine.currentState != AIStateId.Death) { return false; };
            }
            return true;
        }
        else return false;
        
    }

}
