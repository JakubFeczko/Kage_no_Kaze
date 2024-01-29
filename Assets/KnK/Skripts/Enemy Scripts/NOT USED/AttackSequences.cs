using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSequences : MonoBehaviour
{
    public Animator animator;
    public float changeInterval = 2f;

    private float _timer;
    private int _lastSequence = 0;

    private bool _die;
    // Start is called before the first frame update
    void Start()
    {
        if( animator == null)
        {
            animator = GetComponent<Animator>();
        }

        _die = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_die == false)
        {
            _timer += Time.deltaTime;
            if (_timer > changeInterval)
            {
                int randomSequence = Random.Range(1, 4);

                while (randomSequence == _lastSequence)
                {
                    randomSequence = Random.Range(1, 4);
                }

                //resetowanie parametrów
                for (int i = 1; i <= 4; i++)
                {
                    animator.SetBool("Sequence " + i, false);
                }

                //aktywowanie losowych parametrów
                animator.SetBool("Sequence " + randomSequence, true);

                _lastSequence = randomSequence;
                _timer = 0;
            }
        }
        
    }

    public void SetDie()
    {
        _die = true;
        for (int i = 1; i <= 4; i++)
        {
            animator.SetBool("Sequence " + i, false);
        }

        animator.SetBool("Die", true);
    }
}
