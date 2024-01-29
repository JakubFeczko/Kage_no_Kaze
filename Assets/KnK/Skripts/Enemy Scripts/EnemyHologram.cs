using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the state of an enemy hologram.
/// </summary>
public class EnemyHologram : MonoBehaviour
{
    /// <summary>
    /// Indicates whether the hologram is alive.
    /// </summary>
    public bool isAlive = true;

    /// <summary>
    /// Indicates whether the hologram's state has been checked.
    /// </summary>
    public bool wasCheck = false;

    /// <summary>
    /// The GameObject representing the hologram enemy.
    /// </summary>
    public GameObject hologramEnemy;

    /// <summary>
    /// Called every frame, checks the state of the hologram.
    /// </summary>
    private void Update()
    {
        if (!isAlive)
        {
            // Disable the hologram if it is no longer alive
            hologramEnemy.SetActive(false);
        }
    }

    /// <summary>
    /// Changes the state of the hologram to not alive.
    /// </summary>
    public void ChangeState()
    {
        isAlive = false;
    }
}
