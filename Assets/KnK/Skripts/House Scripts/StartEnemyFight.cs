using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initiates enemy fight sequence when the player enters a specific area.
/// </summary>
public class StartEnemyFight : MonoBehaviour
{
    /// <summary>
    /// AudioSource component for playing background music or sound effects.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// An additional AudioSource component, possibly for different sound effects.
    /// </summary>
    public AudioSource audioSource2;

    /// <summary>
    /// Script managing enemy behavior for the second level.
    /// </summary>
    public EnemyScriptLevel2 enemyScriptLevel2;

    /// <summary>
    /// Array of GameObjects representing samurai enemies.
    /// </summary>
    public GameObject[] samuraiEnemy;

    public bool _fightStarted = false;

    /// <summary>
    /// Number of the wave to be activated.
    /// </summary>
    public int waveNumber;

    /// <summary>
    /// GameObject representing an NPC that should be deactivated when the fight starts.
    /// </summary>
    public GameObject npc;

    [Header("Debug Mode")]
    public bool testEnemy = false;

    public StartEnemyFight firstWave;

    /// <summary>
    /// Detects when a collider enters the trigger zone.
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (audioSource != null && other.gameObject.tag == "Player" && !_fightStarted)
        {
            if (waveNumber == 2 && firstWave != null)
            {
                if (firstWave._fightStarted == false) { return; }
            }
            audioSource.Play();
            enemyScriptLevel2.ActiveWave(waveNumber);
            audioSource2.Play();
            foreach (GameObject enemy in samuraiEnemy)
            {
                enemy.SetActive(true);
            }
            _fightStarted = true;
            npc.SetActive(false);
        }
    }

    /// <summary>
    /// Update is called once per frame for debugging purposes.
    /// </summary>
    private void Update()
    {
        if (testEnemy)
        {
            audioSource.Play();
            enemyScriptLevel2.ActiveWave(waveNumber);
            testEnemy = false;
        }
    }
}
