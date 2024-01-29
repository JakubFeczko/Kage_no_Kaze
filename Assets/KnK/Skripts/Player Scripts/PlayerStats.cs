using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Player;
using VRUIP;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the player's stats, including health, stamina, experience points (XP), and XP points.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    public PlayerStatsConfig playerStatsConfig;

    public SetUpScene setUpScene;

    [Header("Stamina Settings")]
    public int staminaDecreaseRate = 30; 
    public int staminaRecoveryRate = 5; 

    private int _currentHealth;
    private int _currentStamina;
    private int _currentXp;
    private int _currentXpPoint;
    private int _currentMaxHealth;
    private int _currentMaxStamina;

    private bool _checkSaveStats = false;

    [Header("Health From Json")]
    [SerializeField]
    private int _healthFromJson;

    [Header("Stamina From Json")]
    [SerializeField]
    private int _staminaFromJson;

    [Header("Xp From Json")]
    [SerializeField]
    private int _xpFromJson;

    [Header("XpPoint From Json")]
    [SerializeField]
    private int _xpPointFromJson;

    [Header("MaxHealth From Json")]
    [SerializeField]
    private int _maxHealthFromJson;

    [Header("MaxStamina From Json")]
    [SerializeField]
    private int _maxStaminaFromJson;

    private float _lastStaminaUpdateTime = 0f;
    private float _staminaUpdateInterval = 1f;

    public HUD playerStats;

    public HVRPlayerController playerController;

    /// <summary>
    /// // Initialize scene setup and player stats configuration.
    /// </summary>
    private void Awake()
    {
        setUpScene = FindObjectOfType<SetUpScene>();
        if( setUpScene != null)
        {
            this.playerStatsConfig = setUpScene.GetPlayerStatsConfig();
        }
        
    }

    /// <summary>
    /// Get the player controller component.
    /// </summary>
    private void Start()
    {
        playerController = GetComponent<HVRPlayerController>();
    }

    /// <summary>
    /// Method to check and save player stats
    /// </summary>
    private void CheckSaveStat()
    {

        if (playerStats != null && playerStatsConfig != null)
        {
            _healthFromJson = playerStatsConfig.Health;
            _staminaFromJson = playerStatsConfig.Stamina;
            _xpFromJson = playerStatsConfig.Xp;
            _xpPointFromJson = playerStatsConfig.XpPoint;
            _maxHealthFromJson = playerStatsConfig.MaxHealth;
            _maxStaminaFromJson = playerStatsConfig.MaxStamina;

            int currentHealth = playerStats._health;
            int currentStamina = playerStats._stamina;
            int currentXp = playerStats._xp;
            int currentXpPoint = playerStats._xpPoint;

            if (currentHealth > playerStatsConfig.Health) { playerStats.UpdateHealth(currentHealth - playerStatsConfig.Health); }
            else { playerStats.UpdateHealth(-(currentHealth - playerStatsConfig.Health)); }

            if (currentStamina < playerStatsConfig.Stamina) { playerStats.UpdateStamina(-(currentStamina - playerStatsConfig.Stamina)); }
            else { playerStats.UpdateStamina(currentStamina - playerStatsConfig.Stamina); }

            if (currentXp < playerStatsConfig.Xp) { playerStats.UpdateXp(-(currentXp - playerStatsConfig.Xp)); }
            else { playerStats.UpdateXp(currentXp - playerStatsConfig.Xp); }

            if (currentXpPoint < playerStatsConfig.XpPoint) { playerStats.UpdateXpPoint(-(currentXpPoint - playerStatsConfig.XpPoint)); }
            else { playerStats.UpdateXpPoint(currentXpPoint - playerStatsConfig.XpPoint); }

            playerStats.UpdateMaxHealth(playerStatsConfig.MaxHealth);
            playerStats.UpdateMaxStamina(playerStatsConfig.MaxStamina);
        }
    }

    /// <summary>
    /// Check and update player stats every frame.
    /// </summary>
    private void Update()
    {
        if(_checkSaveStats == false)
        {
            CheckSaveStat();
            _checkSaveStats = true;
        }
        IsSprinting();
        CheckXpPoint();
        UpdateCurrentStats();
    }

    /// <summary>
    /// Set data for the next scene.
    /// </summary>
    public void SetData4NextScene()
    {
        PlayerStatsConfig playerStats = new PlayerStatsConfig
        {
            Health = _currentMaxHealth,
            Stamina = _currentMaxStamina,
            Xp = _currentXp,
            XpPoint = _currentXpPoint,
            MaxHealth = _currentMaxHealth,
            MaxStamina = _currentMaxStamina
        };
        setUpScene.SaveStatToNextScene(playerStats);
    }

    /// <summary>
    /// Method to check and update XP points.
    /// </summary>
    private void CheckXpPoint()
    {
        if(playerStats._xp > playerStats._maxXp)
        {
            playerStats.UpdateXp(-playerStats._xp);
            playerStats.UpdateXpPoint(1);
        }
    }

    /// <summary>
    /// Update current stats of the player.
    /// </summary>
    public void UpdateCurrentStats()
    {
        _currentHealth = playerStats._health;
        _currentStamina = playerStats._stamina;
        _currentXp = playerStats._xp;
        _currentXpPoint = playerStats._xpPoint;
        _currentMaxHealth = playerStats._maxHealth;
        _currentMaxStamina = playerStats._maxStamina;
    }

    /// <summary>
    /// Method to update maximum health.
    /// </summary>
    public void UpdateMaxHealth()
    {
        if(playerStats._xpPoint > 0)
        {
            playerStats.UpdateMaxHealth(_currentMaxHealth + 10);
            playerStats.UpdateXpPoint(-1);
        }
    }

    /// <summary>
    /// Method to update maximum stamina.
    /// </summary>
    public void UpdateMaxStamina()
    {
        if (playerStats._xpPoint > 0)
        {
            playerStats.UpdateMaxStamina(_currentMaxStamina + 5);
            playerStats.UpdateXpPoint(-1);
        }
    }

    /// <summary>
    /// Get the current XP point of the player.
    /// </summary>
    /// <returns>Current xp points</returns>
    public int GetCurrentXpPoint()
    {
        return _currentXpPoint;
    }

    /// <summary>
    /// Check if the player is sprinting and update stamina accordingly.
    /// </summary>
    public void IsSprinting()
    {
        float currentTime = Time.time;

        if (playerController.CanSprint && playerController._actualVelocity > 3.0f)
        {
            if (currentTime - _lastStaminaUpdateTime > _staminaUpdateInterval)
            {
                playerStats.UpdateStamina(-1);
                _lastStaminaUpdateTime = currentTime;
            }

            if (playerStats._stamina == 0)
            {
                playerController.Sprinting = false;
                playerController.CanSprint = false;
            }
        }
        else
        {
            if (playerStats._stamina < _currentMaxStamina && playerController._actualVelocity < 3.0f)
            {
                if (currentTime - _lastStaminaUpdateTime > _staminaUpdateInterval)
                {
                    playerStats.UpdateStamina(1);
                    _lastStaminaUpdateTime = currentTime;
                }
            }
        }

        if (playerStats._stamina > 0) playerController.CanSprint = true;
    }
}