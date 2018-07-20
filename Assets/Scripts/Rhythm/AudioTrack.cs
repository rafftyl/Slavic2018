using UnityEngine;
using System.Collections.Generic;

public class AudioTrack : ScriptableObject
{
    [SerializeField]
    RhythmData rhythmData;

    [SerializeField]
    AudioClip audioClip;
}