using HurricaneVR.Framework.Core.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRUIP;

/// <summary>
/// Manages the player's health, including taking damage and handling death.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    /// The player's total health.
    /// </summary>
    [SerializeField]
    private int _health;

    /// <summary>
    /// Current health of the player.
    /// </summary>
    public int currentHealth;

    /// <summary>
    /// // Cooldown time before the player can take damage again.
    /// </summary>
    private float _damageCooldown = 0.4f;

    /// <summary>
    /// Timestamp of the last time the player took damage
    /// </summary>
    private float _lastDamageTime;

    /// <summary>
    /// Reference to the screen fade effect.
    /// </summary>
    public HVRScreenFade fade;

    /// <summary>
    /// Reference to the image component, used for showing damage effect.
    /// </summary>
    public Image image;

    /// <summary>
    /// Reference to the HUD script.
    /// </summary>
    public HUD hUD;
    public UnityEvent unityEvent;

    /// <summary>
    /// Update is called once per frame to update the player's health.
    /// </summary>
    private void Update()
    {
        _health = hUD._health;
        currentHealth = _health;
    }

    /// <summary>
    /// Called when the controller collider hits another object.
    /// </summary>
    /// <param name="hit">Information about the collision.</param>
    private void OnControllerColliderHit(ControllerColliderHit hit )
    {
        if (hit.gameObject.CompareTag("weapon") && Time.time - _lastDamageTime > _damageCooldown)
        {
            if(currentHealth > 0)
            {
                TakeDamage(10);
                _lastDamageTime = Time.time;
            }
            else if (currentHealth == 0)
            {
                Die();
            }
        }
    }

    /// <summary>
    /// Reduces the player's health by the specified damage amount.
    /// </summary>
    /// <param name="damage">The amount of damage to take.</param>
    public void TakeDamage(int damage)
    {
        _health -= damage;
        currentHealth = _health;
        hUD.UpdateHealth(-10); ;
    }

    /// <summary>
    /// Handles the player's death, triggering a fade effect and restarting the scene.
    /// </summary>
    private void Die()
    {
        image.color = Color.red;
        fade.Fade(1f, fade.FadeInSpeed);
        StartCoroutine(DelayedEventActivation());
    }

    /// <summary>
    /// A coroutine to delay the activation of an event, such as scene reloading.
    /// </summary>
    IEnumerator DelayedEventActivation()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
