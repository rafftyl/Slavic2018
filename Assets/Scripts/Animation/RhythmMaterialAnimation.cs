using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Renderer))]
public class RhythmMaterialAnimation : RhythmAnimationController, IRhythmListener
{
    [SerializeField]
    RhythmMaterialSequence materialSequence;
    Material[] currentMaterialArray;
    Renderer renderer;
    float materialDisplayDuration;
    float materialDisplayTimer = 0;
    int materialIndex = 0;
    bool isTickReceived = false;

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        Assert.IsNotNull(materialSequence);
    }
    
    public void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick)
    {
        int materialArrayIndex = beatNumber % materialSequence.MaterialsPerBeat.Length;
        currentMaterialArray = materialSequence.MaterialsPerBeat[materialArrayIndex].materials;
        materialDisplayTimer = 0;
        materialDisplayDuration = timeToNextTick / materialSequence.MaterialsPerBeat[materialArrayIndex].materials.Length;
        materialIndex = 0;
        renderer.material = currentMaterialArray[materialIndex];
        isTickReceived = true;
    }

    void FixedUpdate()
    {
        if (isTickReceived)
        {
            materialDisplayTimer += Time.fixedDeltaTime;
            if (materialDisplayTimer > materialDisplayDuration)
            {
                materialDisplayTimer = 0;
                ++materialIndex;
                if(materialIndex == currentMaterialArray.Length)
                {
                    materialIndex = currentMaterialArray.Length - 1;
                }
                renderer.material = currentMaterialArray[materialIndex];
            }
        }
    }
}
