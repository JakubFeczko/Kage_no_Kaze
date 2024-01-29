using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the health bar UI, updating its display based on the current health of the AI or player.
/// </summary>
public class UpdateHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healtbarSprite;

    /// <summary>
    /// Updates the health bar's fill amount based on the current and maximum health.
    /// </summary>
    /// <param name="currentHealth">The current health value.</param>
    /// <param name="maxHealth">The maximum health value.</param>
    public void UpdateHBar(float currentHealth, float maxHealth)
    {
        float fillValue = currentHealth / maxHealth;
        _healtbarSprite.fillAmount = fillValue;
    }
}
