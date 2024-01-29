using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.HDROutputUtils;

public class SceneTransitionManager : MonoBehaviour
{
    public HVRCanvasFade screenFade;
    public static SceneTransitionManager singleton;
    public float fadeDuration = 1f;
    public GameObject player;
    public List<HVRSocket> sockets;

    public List<GameObject> storedItems = new List<GameObject>();

    private void Awake()
    {
        //if (singleton && singleton != this)
            //Destroy(singleton);

        //singleton = this;
    }

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        screenFade.Fade(0f, screenFade.FadeOutSpeed); // Rozpocznij efekt zanikania
        yield return new WaitForSeconds(fadeDuration);

        AsyncOperation operation= SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;


        float timer = 0;
        while (timer <= fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        GameObject spawnPoint = GameObject.Find("PlayerSpawnPoint");
        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
            player.transform.rotation = spawnPoint.transform.rotation;
        }
        operation.allowSceneActivation = true;

        yield return new WaitUntil(() => operation.isDone); // Czekaj, a¿ scena zostanie ca³kowicie za³adowana
    }

    public void GoToSceneAsync(int sceneIndex)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        screenFade.Fade(0f, screenFade.FadeOutSpeed); // Rozpocznij efekt zanikania
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0;
        while (timer <= fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        GameObject spawnPoint = GameObject.Find("PlayerSpawnPoint");
        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
            player.transform.rotation = spawnPoint.transform.rotation;
        }

        operation.allowSceneActivation = true;

        yield return new WaitUntil(() => operation.isDone); // Czekaj, a¿ scena zostanie ca³kowicie za³adowana

    }
}
