using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Light))]
public class LightController : MonoBehaviour, IRhythmListener
{
    public void MetronomeTick(int value, float intensity, bool accent)
    {
        throw new System.NotImplementedException();
    }
}