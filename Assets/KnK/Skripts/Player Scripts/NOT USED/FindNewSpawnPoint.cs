using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNewSpawnPoint : MonoBehaviour
{
    public GameObject player;

    public GameObject spawnPoint;

    public void FindSpawnPoint()
    {
        spawnPoint = GameObject.Find("Spawn Point");
        if (spawnPoint != null)
        {
            Debug.Log("Spawn Point found");
            player.transform.position = spawnPoint.transform.position;
            player.transform.rotation = spawnPoint.transform.rotation;
        }
        else
        {
            Debug.Log("Spawn Point not found");
        }
    }
}
