using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages the first part of the house scenario, controlling the storyline by tracking player actions.
/// </summary>
public class HouseFirstPartManager : MonoBehaviour
{
    public EnemyHologram[] hologramEnemy;
    public PlaySteps step;
    public PlayerEnterHouse player;

    private Dictionary<EnemyHologram, bool> _hologramEnemyDictonary;

    /// <summary>
    /// Initialize the dictionary to track each hologram enemy's state
    /// </summary>
    private void Start()
    {
        _hologramEnemyDictonary = new Dictionary<EnemyHologram, bool>();
        foreach(EnemyHologram enemyHologram in hologramEnemy) { _hologramEnemyDictonary.Add(enemyHologram, false); }
    }

    /// <summary>
    /// Update the state of each hologram enemy and proceed to the next part if conditions are met
    /// </summary>
    private void Update()
    {
        if(_hologramEnemyDictonary != null && player.playerWasEntered)
        {
            List<EnemyHologram> keys = new List<EnemyHologram>(_hologramEnemyDictonary.Keys);

            foreach (var key in keys)
            {
                EnemyHologram enemyHologram = key;
                if (enemyHologram != null && !enemyHologram.isAlive) { _hologramEnemyDictonary[key] = true; }
            }

            if (AreAllTrue(_hologramEnemyDictonary)){ GoNextPart();}
        }
    }

    /// <summary>
    /// Check if all values in the dictionary are true
    /// </summary>
    /// <param name="dictionary">The dictionary to check</param>
    /// <returns>True if all values are true, otherwise false</returns>
    bool AreAllTrue(Dictionary<EnemyHologram, bool> dictionary)
    {
        return dictionary.All(pair => pair.Value);
    }

    /// <summary>
    /// Triggers the next part of the game scenario
    /// </summary>
    public void GoNextPart()
    {
        step.PlayStepIndex(0);
    }
}
