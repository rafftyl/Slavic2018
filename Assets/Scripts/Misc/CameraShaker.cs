using UnityEngine;
using System.Collections.Generic;

public class CameraShaker : MonoBehaviour, IRhythmListener, IGameStartListener
{
    [SerializeField]
    float relativeShakeDuration = 0.3f;
    [SerializeField]
    AnimationCurve horizontalShakeCurve;
    [SerializeField]
    AnimationCurve verticalShakeCurve;

    bool pulse = false;
    float duration = 0;
    float shakeIntensity = 0;
    float time = 0;
    Vector3 initPos;

    [SerializeField]
    Vector2 shakeMagnitudes;

    public void GameStarted()
    {
        initPos = transform.position;
    }

    public void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick)
    {
        if (accent)
        {
            pulse = true;
            duration = relativeShakeDuration * timeToNextTick;
            shakeIntensity = intensity;
            time = 0;
        }
    }

    void Update()
    {
        if (pulse)
        {
            time += Time.deltaTime;
            if (time < duration)
            {
                float relativeTime = time / duration;
                Vector3 right = transform.right;
                Vector3 up = transform.up;
                float rightShake = horizontalShakeCurve.Evaluate(relativeTime) * shakeMagnitudes.x;
                float upShake = verticalShakeCurve.Evaluate(relativeTime) * shakeMagnitudes.y;
                transform.position = initPos + shakeIntensity * (right * rightShake + up * upShake);               
            }
            else
            {
                transform.position = initPos;
            }
        }
    }
}