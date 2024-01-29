using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles collisions for a melee part of an AI agent, typically used for detecting damage from weapons.
/// </summary>
public class AIMeleePartCollision : MonoBehaviour
{
    /// <summary>
    /// The amount of damage taken when this part collides with a weapon.
    /// </summary>
    [SerializeField]
    private int _damageTaken;

    /// <summary>
    /// Cooldown period to prevent rapid collision detections.
    /// </summary>
    [SerializeField]
    private float _collisionCooldown = 0.5f;

    /// <summary>
    /// The AI agent associated with this melee part.
    /// </summary>
    [SerializeField]
    private AIAgent _agent;

    /// <summary>
    /// Timestamp of the last collision.
    /// </summary>
    [SerializeField]
    private float _lastCollisionTime = 0;


    /// <summary>
    /// Called when this object collides with another object.
    /// </summary>
    /// <param name="collision">Information about the collision, including the object collided with.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "weapon" && Time.time - _lastCollisionTime > _collisionCooldown)
        {
            _agent.TakeDamage(_damageTaken);
            _lastCollisionTime = Time.time;
        }
    }
}
