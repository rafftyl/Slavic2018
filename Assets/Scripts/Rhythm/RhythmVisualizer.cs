using UnityEngine;
using System.Collections.Generic;

public class RhythmVisualizer : IRhythmListener, IDanceListener
{
    public DanceController DanceController;

    public void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick)
    {
        throw new System.NotImplementedException();
    }
}