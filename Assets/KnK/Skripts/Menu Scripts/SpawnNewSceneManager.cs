using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewSceneManager : MonoBehaviour
{
    public GameObject player;

    public GameObject spawnPoint;

    private void Awake()
    {
        player = GameObject.Find("Player");
        if(player != null)
        {
            Debug.Log("Player found");
            player.transform.position = spawnPoint.transform.position;
            player.transform.rotation = spawnPoint.transform.rotation;
        }
        else
        {
            Debug.Log("Player not found");
        }
    }
}
