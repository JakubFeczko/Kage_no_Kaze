using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    public new AudioSource audio;
    private void OnCollisionEnter(Collision collision)
    {
        audio.Play();
    }
}
