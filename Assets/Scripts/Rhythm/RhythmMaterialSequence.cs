using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct MaterialArray
{
    [SerializeField]
    public Material[] materials;
}

[CreateAssetMenu(fileName = "MaterialSequence", menuName = "Rhythm/RhythmMaterialSequence")]
public class RhythmMaterialSequence : ScriptableObject
{
    [SerializeField]
    MaterialArray[] materialsPerBeat;
    public MaterialArray[] MaterialsPerBeat { get => materialsPerBeat; }
}