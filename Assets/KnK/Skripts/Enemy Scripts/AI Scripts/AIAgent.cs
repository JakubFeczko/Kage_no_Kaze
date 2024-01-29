using RootMotion.Dynamics;
using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VRUIP;

public class AIAgent : MonoBehaviour
{
    AIAgent agent;

    public AIStateMachine stateMachine;
    
    [Header("AI Core Components")]
    [Tooltip("State Machine handling AI states")]
    public NavMeshAgent navMeshAgent;

    [Tooltip("Configuration settings for AI agent")]
    public AIAgentConfig config;

    [Header("Initial Settings")]
    [Tooltip("Initial state of the enemy")]
    public AIStateId initialState;

    [Header("Player Interaction")]
    [Tooltip("Transform of the player for tracking and interaction")]
    public Transform playerTransform;

    [Header("Health System")]
    [Tooltip("Maximum health of the AI agent")]
    public int maxHealth = 100;

    [Tooltip("Current health of the AI agent")]
    public float currentHealth;

    [Tooltip("UI component to update health display")]
    public UpdateHealthBar healthBar;

    [Tooltip("Canvas for displaying health bar")]
    public Canvas healthBarCanvas;

    [Header("Animation and IK")]
    [Tooltip("PuppetMaster component for ragdoll physics")]
    public PuppetMaster puppet;

    [Tooltip("Aim IK component for aiming adjustments")]
    public AimIK aim;

    [Header("Patrol and Waypoints")]
    [Tooltip("Waypoints for AI patrolling")]
    public Transform[] waypoints;

    [Header("Combat and Weapons")]
    [Tooltip("Script to handle bullet firing")]
    public FireBulletOnActivate fireBulletOnActivate;

    [Tooltip("Prefab for the magazine object")]
    public GameObject magazinePrefab;

    [Tooltip("Spawn point for magazine when reloading")]
    public Transform spawnMagazinePoint;

    [Tooltip("Timestamp for the last time AI fired a bullet")]
    public float lastFireTime = 0.0f;

    [Header("Experience and Rewards")]
    [Tooltip("Experience points gained by the player")]
    public int exp = 230;

    [Tooltip("Reference to player's stats HUD")]
    public HUD playerStats;

    [Header("State Checks")]
    [Tooltip("Flag to determine if AI is dead")]
    public bool isDead = false;

    [Header("Distance to start shoot")]
    [Tooltip("Distance to start shoot at player")]
    [SerializeField]
    private float _distance2Shoot = 4.0f;

    [Space(30)]

    public Animator animator;

    public new AudioSource audio;


    /// <summary>
    /// Initializes AI components, registers AI states, and sets initial state.
    /// </summary>
    void Start()
    {
        // Initialization of AI components
        agent = GetComponent<AIAgent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audio = GetComponent<AudioSource>();
        stateMachine = new AIStateMachine(this);

        // Registering different AI states
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.RegisterState(new AIDeathState());
        stateMachine.RegisterState(new AIIdleState());
        stateMachine.RegisterState(new AIShootState());
        stateMachine.RegisterState(new AIAttackMeleeState());
        stateMachine.RegisterState(new AIIdleMeleeState());
        stateMachine.ChangeState(initialState);

        // Initial AI state
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Update is called once per frame to update the state machine.
    /// </summary>
    void Update()
    {
        stateMachine.Update();
    }

    /// <summary>
    /// Handles the AI taking damage and updates its state and health accordingly.
    /// </summary>
    /// <param name="damage">Amount of damage to apply to the AI.</param>
    public void TakeDamage(float damage)
    {
        if (stateMachine.currentState ==  AIStateId.Idle || stateMachine.currentState == AIStateId.ChasePlayer )
        {
            aim.enabled = true;
            AIShootState shootState = agent.stateMachine.GetState(AIStateId.Shoot) as AIShootState;
            agent.stateMachine.ChangeState(AIStateId.Shoot);
        }

        if (stateMachine.currentState == AIStateId.Idle)
        {
            AIAttackMeleeState attackState = agent.stateMachine.GetState(AIStateId.AttackMelee) as AIAttackMeleeState;
            agent.stateMachine.ChangeState(AIStateId.AttackMelee);
        }

        currentHealth -= damage;
        healthBar.UpdateHBar(currentHealth, maxHealth);

        if (currentHealth <= 0) { Die(); }
    }

    /// <summary>
    /// Handles the AI's death process, changing its state to death.
    /// </summary>
    private void Die()
    {
        Debug.Log("Set to die");
        AIDeathState deathState = agent.stateMachine.GetState(AIStateId.Death) as AIDeathState;
        agent.stateMachine.ChangeState(AIStateId.Death);
    }


    /// <summary>
    /// Moves the AI agent to a random waypoint.
    /// </summary>
    public void MoveToRandomWaypoint()
    {
        if (waypoints.Length == 0) return;

        int waypointIndex = Random.Range(0, waypoints.Length);
        Vector3 waypointPosition = waypoints[waypointIndex].position;

        agent.navMeshAgent.SetDestination(waypointPosition);
    }

    public void Rotate2Player(AIAgent agent)
    {
        Vector3 direction2Player = agent.playerTransform.position - agent.navMeshAgent.transform.position;
        direction2Player.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(direction2Player);
        float angle = Quaternion.Angle(agent.navMeshAgent.transform.rotation, lookRotation);

        if (angle > 5f)
        {
            agent.transform.rotation = Quaternion.Slerp(agent.navMeshAgent.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    public void CheckPlayerDistance(AIAgent agent)
    {
        Vector3 directionToPlayer = agent.playerTransform.position - agent.navMeshAgent.transform.position;
        float distance2Player = directionToPlayer.magnitude;
        if (distance2Player < agent._distance2Shoot)
        {
            agent.stateMachine.ChangeState(AIStateId.Shoot);
        }
    }
}
