using UnityEngine;
using System.Collections.Generic;

public class RhythmVisualizer : IRhythmListener, IDanceListener
{
    public DanceController DanceController;

    public void MetronomeTick(int value, float intensity, bool accent)
    {
        throw new System.NotImplementedException();
    }
}