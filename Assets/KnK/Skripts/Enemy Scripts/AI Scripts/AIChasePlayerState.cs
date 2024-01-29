using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Represents the state where the AI agent chases the player.
/// </summary>
public class AIChasePlayerState : AIState
{

    /// <summary>
    /// Called when entering the chase player state.
    /// </summary>
    /// <param name="agent">The AI agent entering the state.</param>
    public void Enter(AIAgent agent)
    {
        agent.MoveToRandomWaypoint();
    }

    /// <summary>
    /// Called when exiting the chase player state.
    /// </summary>
    /// <param name="agent">The AI agent exiting the state.</param>
    public void Exit(AIAgent agent)
    {
        
    }

    /// <summary>
    /// Returns the ID of the chase player state.
    /// </summary>
    /// <returns>The ID of the chase player state.</returns>
    public AIStateId GetId()
    {
        return AIStateId.ChasePlayer;
    }

    /// <summary>
    /// Updates the AI agent during the chase player state.
    /// </summary>
    /// <param name="agent">The AI agent to update.</param>
    public void Update(AIAgent agent)
    {
        if (!agent.enabled) { return; }
        agent.Rotate2Player(agent);
        if (!agent.navMeshAgent.hasPath)
        {
            agent.stateMachine.ChangeState(AIStateId.Idle);
        }
    }
}
