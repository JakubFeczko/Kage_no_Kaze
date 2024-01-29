using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI state for handling melee attacks.
/// </summary>
public class AIAttackMeleeState : AIState
{
    /// <summary>
    /// Array of attack types that the melee AI can perform.
    /// </summary>
    public static string[] attacks = { "Attack1", "Attack2", "Blocking", "Attack3", "AttackSeq"};

    private string _lastAttack = "";

    private float _lastBlockTime;
    private bool _isBlocking = false;
    private float _timeAfterBlockingBeforeNextAction = 2f;
    private float _minAttackDuration = 2.0f;
    private float _lastAttackEndTime = 0f;

    List<string> availableAttacks = new List<string>(attacks);

    /// <summary>
    /// Called when entering the melee attack state.
    /// </summary>
    /// <param name="agent">The AI agent that is performing the state.</param>
    public void Enter(AIAgent agent)
    {   
        agent.audio.Play();
        agent.aim.enabled = true;
        agent.Rotate2Player(agent);
    }

    /// <summary>
    /// Called when the AI agent exits the melee attack state.
    /// </summary>
    /// <param name="agent">The AI agent that is exiting the melee attack state.</param>
    public void Exit(AIAgent agent)
    {
        
    }

    /// <summary>
    /// Returns the ID of the melee attack state.
    /// </summary>
    /// <returns>The ID of the melee attack state.</returns>
    public AIStateId GetId()
    {
        return AIStateId.AttackMelee;
    }

    /// <summary>
    /// Updates the AI agent during the melee attack state.
    /// </summary>
    /// <param name="agent">The AI agent to update.</param>
    public void Update(AIAgent agent)
    {
        // Check if the AI agent is farther than 3 units from the player
        if (CheckDistance2Player(agent) > 3.0f)
        {
            // If too far, set the AI to chase the player
            agent.animator.SetTrigger("Chase");
            agent.navMeshAgent.SetDestination(agent.playerTransform.position);
        }

        // Rotate the AI to face the player
        agent.Rotate2Player(agent);

        // If the AI agent is within 3 units of the player
        if (CheckDistance2Player(agent) <= 3.0f)
        {
            // Check if the AI is currently blocking
            if (_isBlocking)
            {
                // Stop blocking and reset the last attack end time
                agent.animator.SetBool("Blocking", false);
                _isBlocking = false;
                _lastAttackEndTime = Time.time;
            }
            else if (Time.time - _lastBlockTime >= _timeAfterBlockingBeforeNextAction && Time.time - _lastAttackEndTime >= _minAttackDuration)
            {
                // If enough time has passed since the last block or attack, choose a new attack
                string attack = GetRandomAttack();

                // If the chosen attack is blocking
                if (attack == "Blocking")
                {
                    // Start blocking and reset the block and attack times
                    agent.animator.SetBool("Blocking", true);
                    _lastBlockTime = Time.time;
                    _isBlocking = true;
                    _lastAttackEndTime = Time.time;
                }
                else
                {
                    // If the chosen attack is not a block, perform the attack and reset the last attack time
                    agent.animator.SetTrigger(attack);
                    _lastAttackEndTime = Time.time;
                }
            }
        }

    }

    /// <summary>
    /// Checks the distance between the AI agent and the player.
    /// </summary>
    /// <param name="agent">The AI agent.</param>
    /// <returns>The distance to the player.</returns>
    private float CheckDistance2Player(AIAgent agent)
    {
        float distance = Vector3.Distance(agent.transform.position, agent.playerTransform.position);
        return distance;
    }


    /// <summary>
    /// Selects a random attack from the list of available attacks.
    /// </summary>
    /// <returns>A string representing the selected attack.</returns>
    private string GetRandomAttack()
    {
        if (availableAttacks.Count == 0)
        {
            availableAttacks = new List<string>(attacks);
        }
        if (_lastAttack != "")
        {
            availableAttacks.Remove(_lastAttack);
        }

        if (availableAttacks.Count == 0)
        {
            // Reset the list if all attacks have been used
            availableAttacks = new List<string>(attacks);
        }

        int index = Random.Range(0, availableAttacks.Count);
        string selectedAttack = availableAttacks[index];
        _lastAttack = selectedAttack;
        return selectedAttack;
    }
}
