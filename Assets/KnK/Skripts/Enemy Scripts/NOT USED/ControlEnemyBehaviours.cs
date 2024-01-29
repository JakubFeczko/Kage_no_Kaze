using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ControlEnemyBehaviours : MonoBehaviour
{
    public Animator animator;
    public AimIK aimIK;
    public float smoothBlend = 0.1f;
    public float transitionDuration = 1.0f;
    private float _currentTransitionTime = 0f;
    private bool _isTransitioning = false;
    private bool _isAnimating = false;
    public Transform target;

    private bool _setUpEnemyMode = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
        aimIK = GetComponent<AimIK>();
    }

    private void Update()
    {
        if (_setUpEnemyMode && !_isTransitioning)
        {
            _currentTransitionTime += Time.deltaTime;

            float blendValue = Mathf.Clamp(_currentTransitionTime / transitionDuration, 0f, 1f);
            animator.SetFloat("Blend", blendValue);

            if (_currentTransitionTime >= transitionDuration)
            {
                _isTransitioning = true;
            }
        }

        if (_isTransitioning && !_isAnimating)
        {
            SetUpAgressive();
            _isAnimating = true;
        }
    }

    //private void LateUpdate()
    //{
    //    aimIK.solver.axis = aimIK.solver.transform.InverseTransformDirection(target.right);
    //}

    public void SetUpAgressive()
    {
        if (animator != null)
        {
            animator.SetLayerWeight(1, 1);
            aimIK.enabled = true;
        }
    }

    private void SetUpEnemyMode(bool enemyMode)
    {
        _setUpEnemyMode = enemyMode;
    }

    public void ActivateEnemyModeAfterDelay()
    {
        StartCoroutine(SetUpEnemyModeAfterDelayCoroutine());
    }

    // Koreutyna czekaj¹ca 2 sekundy przed ustawieniem SetUpEnemyMode na true
    private IEnumerator SetUpEnemyModeAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(2f); // Oczekiwanie przez 2 sekundy
        SetUpEnemyMode(true);
    }
}
