using UnityEngine;
using System.Collections.Generic;

public class DanceController : AnimationModifier , IGameStateReceiver, IRhythmListener, IAttractionGenerator
{
    List<IDanceListener> danceListeners;

    public GameState GameState { set => throw new System.NotImplementedException(); }

    public Vector2 Position => throw new System.NotImplementedException();

    public float Attraction => throw new System.NotImplementedException();

    public float AttractionFalloff => throw new System.NotImplementedException();

    public void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick)
    {
        throw new System.NotImplementedException();
    }
}