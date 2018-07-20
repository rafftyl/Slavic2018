
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DanceController : AnimationModifier , IGameStateReceiver, IRhythmListener, IAttractionGenerator {

    public DanceController() {
    }

    public List<IDanceListener> DanceListeners;




}