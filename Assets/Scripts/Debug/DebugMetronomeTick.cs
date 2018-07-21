using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(AudioSource))]
public class DebugMetronomeTick : MonoBehaviour, IRhythmListener
{
    AudioSource source;
    [SerializeField]
    AudioClip metronomeTick;

    public void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick)
    {
        source.PlayOneShot(metronomeTick);
    }

    void Awake ()
    {
        Assert.IsNotNull(metronomeTick);
        source = GetComponent<AudioSource>();
        source.loop = false;
	}


}
