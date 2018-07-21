using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MeasureCounter : MonoBehaviour, IRhythmListener
{
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    public void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick)
    {
        text.text = beatNumber.ToString() + "/" + measure.ToString() + " intensity: " + intensity + " accent: " + accent + " tick duration: " + timeToNextTick;
    }
}
