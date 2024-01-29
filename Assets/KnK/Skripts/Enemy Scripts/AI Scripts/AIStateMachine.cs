using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the states of an AI agent, allowing transitions between different behaviors.
/// </summary>
public class AIStateMachine
{
    public AIState[] states;
    public AIAgent agent;
    public AIStateId currentState;

    /// <summary>
    /// Constructor for the AIStateMachine.
    /// </summary>
    /// <param name="agent">The AI agent this state machine will manage.</param>
    public AIStateMachine(AIAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(AIStateId)).Length;
        states = new AIState[numStates];
    }


    /// <summary>
    /// Registers a state with the state machine.
    /// </summary>
    /// <param name="state">The state to be registered.</param>
    public void RegisterState(AIState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }


    /// <summary>
    /// Retrieves a state from the state machine.
    /// </summary>
    /// <param name="stateId">The ID of the state to retrieve.</param>
    /// <returns>The AIState corresponding to the given ID.</returns>
    public AIState GetState(AIStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }

    /// <summary>
    /// Updates the current state of the AI agent.
    /// </summary>
    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }

    /// <summary>
    /// Changes the current state of the AI agent to a new state.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    public void ChangeState(AIStateId newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
