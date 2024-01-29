using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSecretWall : MonoBehaviour
{
    public GameObject led1;
    public GameObject led2;
    public WallAnimationControler wallAnimationControler;
    private Renderer renderer1, renderer2;
    private bool animationStart = false;

    private void Awake()
    {
        renderer1 = led1.GetComponent<Renderer>();
        renderer2 = led2.GetComponent<Renderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if(!animationStart && renderer1.material.color == Color.green &&  renderer2.material.color == Color.green)
        {
            wallAnimationControler.StartWallAnimation();
            animationStart = true;
        }
    }
}
