using RootMotion.Dynamics;
using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;
    public UpdateHealthBar healthBar;
    public PuppetMaster puppet;
    public LookAtIK iK;
    public AimIK ami;
    //public AttackSequences attackSequences;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Take Damage");
        currentHealth -= damage;
        healthBar.UpdateHBar(currentHealth, maxHealth);
        Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy Die");
            puppet.state = PuppetMaster.State.Dead;
            iK.enabled = false;
            ami.enabled = false;
            //attackSequences.SetDie();
        }
    }
}
