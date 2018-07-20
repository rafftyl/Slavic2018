using UnityEngine;
using System.Collections.Generic;

public class AttractionHeatmapper : MonoBehaviour
{
    List<IAttractionGenerator> attractionGenerators;

    void RegisterGenerator(IAttractionGenerator generator)
    {
        attractionGenerators.Add(generator);
    }

    void UnregisterGenerator(IAttractionGenerator generator)
    {
        attractionGenerators.Remove(generator);
    }
}