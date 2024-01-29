using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
///  Manages locomotion for an AI agent using Unity's NavMeshAgent for pathfinding and Animator for animation.
/// </summary>
public class AILocomotion : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

    /// <summary>
    /// Start is called before the first frame update. Initializes the agent and animator components.
    /// </summary>
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Update is called once per frame. Updates the animator parameters based on the agent's movement.
    /// </summary>
    void Update()
    {
        if (_agent.hasPath)
        {
            _animator.SetFloat("Speed", _agent.velocity.magnitude);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }
}
