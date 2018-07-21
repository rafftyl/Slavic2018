using UnityEngine;
using System.Collections.Generic;

public class DanceController : AnimationModifier , IGameStateReceiver, IRhythmListener, IAttractionGenerator
{
    List<IDanceListener> danceListeners;

    public GameState GameState { set => throw new System.NotImplementedException(); }

    public Vector2 Position => throw new System.NotImplementedException();

    public float Attraction => throw new System.NotImplementedException();

    public float AttractionFalloff => throw new System.NotImplementedException();

    public void MetronomeTick(int value, float intensity, bool accent)
    {
        throw new System.NotImplementedException();
    }
}