using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class PlaySteps : MonoBehaviour
{
    public PlayableDirector director;
    public List<Step> steps;
    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }
    [System.Serializable]
    public class Step
    {
        public string name;
        public float time;
        public bool hasPlayed;
    }

    public void PlayStepIndex(int index)
    {
        Step step = steps[index];

        Debug.Log("Nazwa step: " + step.name);
        if(!step.hasPlayed)
        {
            step.hasPlayed = true;
            director.Stop();
            director.time = step.time;
            director.Play();
        }
    } 
}
