using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the idle state for a melee AI agent.
/// </summary>
public class AIIdleMeleeState : AIState
{
    /// <summary>
    /// Called when the AI agent enters the idle state.
    /// </summary>
    /// <param name="agent">The AI agent entering the state.</param>
    public void Enter(AIAgent agent)
    {
        
    }

    /// <summary>
    /// Called when the AI agent exits the idle state.
    /// </summary>
    /// <param name="agent">The AI agent exiting the state.</param>
    public void Exit(AIAgent agent)
    {
        
    }

    /// <summary>
    /// Returns the ID of this state, which is AIStateId.IdleMelee.
    /// </summary>
    /// <returns>The AIStateId corresponding to the idle melee state.</returns>
    public AIStateId GetId()
    {
        return AIStateId.IdleMelee;
    }

    /// <summary>
    /// Called every frame to update the state.
    /// </summary>
    /// <param name="agent">The AI agent that is being updated.</param>
    public void Update(AIAgent agent)
    {
        // Calculate the direction and distance to the player.
        Vector3 directionToPlayer = agent.playerTransform.position - agent.navMeshAgent.transform.position;
        float distance2Player = directionToPlayer.magnitude;

        // If the player is within 3 units, change the state to attack melee.
        if (distance2Player < 3.0f)
        {
            agent.stateMachine.ChangeState(AIStateId.AttackMelee);
        }
    }
}
