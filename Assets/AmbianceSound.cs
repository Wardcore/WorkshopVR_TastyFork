using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceSound : MonoBehaviour {

    public AudioSource[] AudioLoop;
    public AnimationCurve plot = new AnimationCurve();
    //public AnimationCurve plot2 = new AnimationCurve();
    private int random;
    private int randomTime;

    private void Start()
    {
        random = Random.Range(0, AudioLoop.Length);
        AudioLoop[random].Play();
        StartCoroutine(PlayAnotherSound());
    }

    IEnumerator PlayAnotherSound()
    {
        AudioLoop[random].volume = 1f;
        randomTime = Random.Range(4, 9);
        yield return new WaitForSeconds(randomTime);
        Debug.Log("Audio : "+random);
        int newrandom = Random.Range(0, AudioLoop.Length);
        if(newrandom != random)
        {
            AudioLoop[newrandom].volume = 0f;
            AudioLoop[newrandom].Play();
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.1f);
                AudioLoop[random].volume -= 0.1f;
                AudioLoop[newrandom].volume += 0.1f;
                plot.AddKey(Time.realtimeSinceStartup, AudioLoop[random].volume);
                //plot2.AddKey(Time.realtimeSinceStartup, AudioLoop[newrandom].volume);
            }
            random = newrandom;
            //AudioLoop[random].Play();
        }
        else
        {
            newrandom = Random.Range(0, AudioLoop.Length);
        }
        StartCoroutine(PlayAnotherSound());
    }
}
