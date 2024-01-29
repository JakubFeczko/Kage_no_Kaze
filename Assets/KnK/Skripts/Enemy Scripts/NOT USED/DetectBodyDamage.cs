using HurricaneVR.Framework.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBodyDamage : HVRDamageHandlerBase
{

    public bool Damageable = true;

    public Rigidbody Rigidbody { get; private set; }

    public Enemy enemy;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public override void TakeDamage(float damage)
    {
        if (Damageable)
        {
            enemy.TakeDamage(damage);
        }
    }

    public override void HandleDamageProvider(HVRDamageProvider damageProvider, Vector3 hitPoint, Vector3 direction)
    {
        base.HandleDamageProvider(damageProvider, hitPoint, direction);

        if(Rigidbody)
        {
            Rigidbody.AddForceAtPosition(direction.normalized * damageProvider.Force, hitPoint, ForceMode.Impulse);
        }

    }
}
