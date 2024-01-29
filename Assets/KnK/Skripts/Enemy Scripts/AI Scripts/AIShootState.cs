using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Represents the AI state for shooting behavior.
/// </summary>
public class AIShootState : AIState
{
    /// <summary>
    /// Called when the AI agent enters the shoot state.
    /// </summary>
    /// <param name="agent">The AI agent entering the shoot state.</param>
    public void Enter(AIAgent agent)
    {
        
    }

    /// <summary>
    /// Called when the AI agent exits the shoot state.
    /// </summary>
    /// <param name="agent">The AI agent exiting the shoot state.</param>
    public void Exit(AIAgent agent)
    {
        
    }

    /// <summary>
    /// Returns the ID of the shoot state.
    /// </summary>
    /// <returns>The ID of the shoot state.</returns>
    public AIStateId GetId()
    {
        return AIStateId.Shoot;
    }

    /// <summary>
    /// Update method for the shoot state.
    /// </summary>
    /// <param name="agent">The AI agent to update.</param>
    public void Update(AIAgent agent)
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = agent.playerTransform.position - agent.navMeshAgent.transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Rotate the agent to face the player
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        agent.transform.rotation = Quaternion.Slerp(agent.navMeshAgent.transform.rotation, lookRotation, Time.deltaTime * 5f);

        // If the player is too close, move to a random waypoint
        if (distanceToPlayer < 3f) { ExtendDistance(agent); }
        else
        {
            // Check if it's time to fire a bullet
            if (Time.time - agent.lastFireTime >= agent.config.fireRate && agent.currentHealth >= 0)
            {
                agent.fireBulletOnActivate.FireBullet();
                agent.lastFireTime = Time.time;
            }
            
        }
    }

    /// <summary>
    /// Extends the distance between the AI agent and the player by moving the agent in the opposite direction.
    /// </summary>
    /// <param name="agent">The AI agent that needs to extend the distance from the player.</param>
    private void ExtendDistance(AIAgent agent)
    {
        Vector3 fleeDirection = (agent.navMeshAgent.transform.position - agent.playerTransform.position).normalized;
        Vector3 newGoal = agent.navMeshAgent.transform.position + fleeDirection * 5f;

        NavMeshHit hit;
        NavMesh.SamplePosition(newGoal, out hit, 5f, NavMesh.AllAreas);
        agent.navMeshAgent.SetDestination(hit.position);
    }
}
