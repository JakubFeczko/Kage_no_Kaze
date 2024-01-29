using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerLevelThree : MonoBehaviour
{
    [SerializeField]
    private NPCLevelThreeControler _npcLevelControler;

    [SerializeField]
    private AudioSource battleMusic;

    private bool _musicWasPlayed = false;
    private bool _playerWasEntered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (_npcLevelControler != null && other.gameObject.tag == "Player" && !_playerWasEntered)
        {
            _npcLevelControler.PlayTimeline();
            _playerWasEntered = true;
        }

        if(battleMusic != null && !_musicWasPlayed && other.gameObject.tag == "Player")
        {             
            battleMusic.Play();
            _musicWasPlayed = true;
        }
    }
}
