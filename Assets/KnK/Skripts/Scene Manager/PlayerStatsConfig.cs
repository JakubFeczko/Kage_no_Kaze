using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerStatsConfig
{
    [SerializeField]
    private int _health;

    [SerializeField]
    private int _stamina;

    [SerializeField]
    private int _xp;

    [SerializeField]
    private int _xpPoint;

    [SerializeField]
    private int _maxHealth;

    [SerializeField]
    private int _maxStamina;

    public int Health 
    {
        get { return _health; }
        set { _health = value; } 
    }

    public int Stamina 
    {
        get { return _stamina; }
        set { _stamina = value; } 
    }

    public int Xp
    {
        get { return _xp; }
        set { _xp = value; }
    }

    public int XpPoint
    {
        get { return _xpPoint; }
        set { _xpPoint = value; }
    }

    public int MaxStamina
    {
        get { return _maxStamina; }
        set { _maxStamina = value; }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

}

[System.Serializable]
public class SceneData
{
    public int SceneIndex;
    public bool IsSceneCompleted;
}
