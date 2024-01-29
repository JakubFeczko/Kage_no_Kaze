using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the main boss and end-of-level events for the third level.
/// </summary>
public class EnemyManagerLevel3 : MonoBehaviour
{
    /// <summary>
    /// The main boss AI agent.
    /// </summary>
    public AIAgent mainBoss;
    public GameObject windowCase;

    /// <summary>
    /// The menu to be displayed when the level is finished.
    /// </summary>
    public GameObject finshMenu;
    private bool bossWasKilled = false;

    /// <summary>
    /// Update is called once per frame to check the boss's state and trigger end-of-level events.
    /// </summary>
    private void Update()
    {
        if (!bossWasKilled)
        {
            if(!CheckEnemyAlive())
            {
                windowCase.SetActive(false);
                bossWasKilled = true;
                finshMenu.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Checks if the main boss is still alive.
    /// </summary>
    /// <returns>True if the boss is dead, otherwise false.</returns>
    public bool CheckEnemyAlive()
    {
        if(mainBoss.stateMachine.currentState == AIStateId.Death) { return false;}
        else{ return true; }
    }
}
