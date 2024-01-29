using RootMotion.Dynamics;
using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the AI state for handling the death of the AI agent.
/// </summary>
public class AIDeathState : AIState
{
    /// <summary>
    /// Called when the AI agent enters the death state.
    /// </summary>
    /// <param name="agent">The AI agent that is entering the death state.</param>
    public void Enter(AIAgent agent)
    {
        if (!agent.isDead)
        {
            agent.puppet.state = PuppetMaster.State.Dead;
            agent.healthBar.enabled = false;
            agent.healthBarCanvas.enabled = false;
            agent.aim.enabled = false;
            agent.playerStats.UpdateXp(agent.exp);

            if (agent.magazinePrefab != null && agent.spawnMagazinePoint != null && agent.isDead == false)
            {
                GameObject.Instantiate(agent.magazinePrefab, agent.spawnMagazinePoint.position, agent.spawnMagazinePoint.rotation);
            }

            agent.isDead = true;
        }
        
    }

    /// <summary>
    /// Called when the AI agent exits the death state.
    /// </summary>
    /// <param name="agent">The AI agent that is exiting the death state.</param>
    public void Exit(AIAgent agent)
    {

    }

    /// <summary>
    /// Returns the ID of the death state.
    /// </summary>
    /// <returns>The ID of the death state.</returns>
    public AIStateId GetId()
    {
        return AIStateId.Death;
    }

    /// <summary>
    /// Update method for the death state, currently unused.
    /// </summary>
    /// <param name="agent">The AI agent to update.</param>
    public void Update(AIAgent agent)
    {
        
    }
}
