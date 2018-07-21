using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RhythmData", menuName = "Rhythm/RhythmData")]
public class RhythmData : ScriptableObject
{
    [SerializeField]
    float beatsPerMinute = 150;

    [SerializeField]
    float intensity = 0.5f;

    [SerializeField]
    bool[] measureAccentDistribution;

    public float BeatsPerMinute { get => beatsPerMinute; set => beatsPerMinute = value; }
    public float Intensity { get => intensity; set => intensity = value; }
    public int TimeSignature { get => MeasureAccentDistribution.Length; }
    public bool[] MeasureAccentDistribution { get => measureAccentDistribution; }
  
    public bool IsAccentOnBeat(int beat)
    {
        Assert.IsTrue(beat < MeasureAccentDistribution.Length);
        return MeasureAccentDistribution[beat];
    }
}