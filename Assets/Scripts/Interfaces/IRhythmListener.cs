
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IRhythmListener {


    /// <summary>
    /// @param value 
    /// @param intensity 
    /// @param accent
    /// </summary>
    public void MetronomeTick(int value, float intensity, bool accent);

}