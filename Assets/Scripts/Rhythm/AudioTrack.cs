using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;

[System.Serializable]
public class RhythmDataChangeMarker
{
    public int measure;
    public RhythmData rhythmData;
}

public class AudioTrack : ScriptableObject
{
    [SerializeField]
    List<RhythmDataChangeMarker> rhythmChangeData;

    [SerializeField]
    AudioClip audioClip;

    public RhythmData GetRhythmDataForMeasure(int measure)
    {
        var changeMarker = rhythmChangeData.FindLast((RhythmDataChangeMarker marker) => { return marker.measure <= measure; });
        if (changeMarker == null)
        {
            return null;
        }
        return changeMarker.rhythmData;
    }   
}