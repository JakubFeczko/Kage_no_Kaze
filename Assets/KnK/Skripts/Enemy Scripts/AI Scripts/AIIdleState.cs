using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the AI state for when the agent is in idle mode.
/// </summary>
public class AIIdleState : AIState
{
    /// <summary>
    /// Called when the AI agent enters the idle state.
    /// </summary>
    /// <param name="agent">The AI agent entering the idle state.</param>
    public void Enter(AIAgent agent)
    {
        
    }

    /// <summary>
    /// Called when the AI agent exits the idle state.
    /// </summary>
    /// <param name="agent">The AI agent exiting the idle state.</param>
    public void Exit(AIAgent agent)
    {
        
    }

    /// <summary>
    /// Returns the ID of the idle state.
    /// </summary>
    /// <returns>The ID of the idle state.</returns>
    public AIStateId GetId()
    {
        return AIStateId.Idle;
    }

    /// <summary>
    /// Update method for the idle state.
    /// </summary>
    /// <param name="agent">The AI agent to update.</param>
    public void Update(AIAgent agent)
    {
        agent.Rotate2Player(agent);
        agent.CheckPlayerDistance(agent);
    }

    
}
