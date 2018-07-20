using UnityEngine;
using System.Collections.Generic;

public class AttractionController : CharacterController, IAttractionHeatmapReceiver
{
    public AttractionHeatmapper AttractionHeatmapper { set => throw new System.NotImplementedException(); }
}