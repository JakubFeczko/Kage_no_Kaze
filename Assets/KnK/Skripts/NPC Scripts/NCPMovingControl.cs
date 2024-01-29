using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the movement and turning behavior of a non-player character (NPC).
/// </summary>
public class NCPMovingControl : MonoBehaviour
{
    /// <summary>
    /// Distance the NPC walks in one direction.
    /// </summary>
    public float walkDistance = 20f;

    /// <summary>
    /// Walking speed of the NPC.
    /// </summary>
    public float walkSpeed = 3f;

    /// <summary>
    /// Turning speed of the NPC.
    /// </summary>
    public float turnSpeed = 2f;

    /// <summary>
    /// Start is called before the first frame update to initiate the walking routine.
    /// </summary>
    void Start()
    {
        StartCoroutine(WalkRoutine());
    }

    /// <summary>
    /// Coroutine to manage the continuous walking and turning routine of the NPC.
    /// </summary>
    /// <returns></returns>
    IEnumerator WalkRoutine()
    {
        while (true)
        {
            // Walk forward for a specified distance
            yield return StartCoroutine(WalkForward(walkDistance));

            // Turn around by 180 degrees
            yield return StartCoroutine(TurnAround());

            // Walk forward for twice the initial distance
            yield return StartCoroutine(WalkForward(2 * walkDistance));

            // Turn around again
            yield return StartCoroutine(TurnAround());
        }
    }

    /// <summary>
    /// Coroutine for walking forward a specified distance.
    /// </summary>
    /// <param name="distance">The distance to walk forward.</param>
    /// <returns></returns>
    IEnumerator WalkForward(float distance)
    {
        float remainingDistance = distance;
        while (remainingDistance > 0f)
        {
            // Oblicz krok do przodu
            float step = walkSpeed * Time.deltaTime;
            transform.Translate(Vector3.forward * step);
            remainingDistance -= step;
            yield return null;
        }
    }

    /// <summary>
    /// Coroutine for turning the NPC around by 180 degrees.
    /// </summary>
    /// <returns></returns>
    IEnumerator TurnAround()
    {
        float remainingAngle = 180f;
        while (remainingAngle > 0f)
        {
            // Oblicz k¹t obrotu
            float angle = turnSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * angle);
            remainingAngle -= angle;
            yield return null;
        }
    }
}
