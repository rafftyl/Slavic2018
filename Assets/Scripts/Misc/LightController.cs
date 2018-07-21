using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Light))]
public class LightController : MonoBehaviour, IRhythmListener
{
    public void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick)
    {
        throw new System.NotImplementedException();
    }
}