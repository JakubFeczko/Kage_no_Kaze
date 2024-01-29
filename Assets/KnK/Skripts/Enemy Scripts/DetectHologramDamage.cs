using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects damage to an enemy hologram when it collides with an object tagged as a weapon.
/// </summary>
public class DetectHologramDamage : MonoBehaviour
{
    /// <summary>
    /// Reference to the EnemyHologram script that handles the enemy's health and state.
    /// </summary>
    public EnemyHologram enemyHealth;

    /// <summary>
    /// Called when this object collides with another object.
    /// </summary>
    /// <param name="collision">Information about the collision, including the object collided with.</param>
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has a tag labeled "weapon"
        if (collision.gameObject.tag == "weapon")
        {
            // If collided with a weapon, change the state of the enemy hologram
            enemyHealth.ChangeState();
        }
    }
}
