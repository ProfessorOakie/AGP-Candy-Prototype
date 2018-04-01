using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDefeatedEffects : MonoBehaviour {

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private ParticleSystem[] confetti;

    private void Start()
    {
        if(!source)
            source = GetComponent<AudioSource>();
        if(confetti.Length == 0)
            confetti = GetComponentsInChildren<ParticleSystem>();
    }

    public void PlayEffects()
    {
        StartCoroutine(TimedEffects());
    }

    IEnumerator TimedEffects()
    {  
        if(source)
            source.Play();

        yield return new WaitForSeconds(0.7f);

        foreach(var c in confetti)
        {
            c.Play(true);
        }
    }

}
