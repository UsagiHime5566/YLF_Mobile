using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.Threading.Tasks;

public class DolphinAnim : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    public AudioSource audioSource;
    public int delayAnim = 300;
    public bool isPlayOnStart = false;
    
    void Start()
    {
        if(isPlayOnStart)
        {
            PlayTimeline();
        }
    }

    public void SetPlayEnd()
    {
        timelineDirector.Play();
        timelineDirector.time = timelineDirector.duration;
    }

    public void PlayTimeline()
    {
        StartCoroutine(PlayTimelineCoroutine());
    }

    private IEnumerator PlayTimelineCoroutine()
    {
        yield return new WaitForSeconds(delayAnim / 1000f);
        timelineDirector.Play();
        audioSource.Play();
    }

    

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayTimeline();
        }
    }
}
