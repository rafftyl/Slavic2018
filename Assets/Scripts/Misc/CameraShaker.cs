using UnityEngine;
using System.Collections.Generic;

public class CameraShaker : MonoBehaviour, IRhythmListener
{
    [SerializeField]
    List<CameraShake> cameraShakes;

    public void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick)
    {
        throw new System.NotImplementedException();
    }
}