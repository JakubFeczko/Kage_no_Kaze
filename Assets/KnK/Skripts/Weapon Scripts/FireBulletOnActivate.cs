using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for firing a bullet from a spawn point.
/// </summary>
public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed;
    public AudioSource gunShoot;

    /// <summary>
    /// Method to fire the bullet.
    /// </summary>
    public void FireBullet()
    {
        GameObject spawndBullet = Instantiate(bullet);
        spawndBullet.transform.position = spawnPoint.position;
        gunShoot.Play();
        spawndBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawndBullet, 5);                                                                                                                                                              
    }
}
