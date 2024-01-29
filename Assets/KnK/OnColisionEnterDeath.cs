using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnColisionEnterDeath : MonoBehaviour
{
    public string targetTag;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag ==  targetTag)
        {
            
        }
    }
}
