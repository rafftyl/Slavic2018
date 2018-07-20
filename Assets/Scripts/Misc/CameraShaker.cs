using UnityEngine;
using System.Collections.Generic;

public class CameraShaker : MonoBehaviour, IRhythmListener
{
    [SerializeField]
    List<CameraShake> cameraShakes;

    public void MetronomeTick(int value, float intensity, bool accent)
    {
        throw new System.NotImplementedException();
    }
}