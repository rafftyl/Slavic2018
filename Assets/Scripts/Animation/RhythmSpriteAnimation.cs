using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class RhythmSpriteAnimation : RhythmAnimationController, IRhythmListener
{
    [SerializeField]
    RhythmSpriteSequence materialSequence;
    Sprite[] currentSpriteArray;
    SpriteRenderer renderer;
    float spriteDisplayDuration;
    float spriteDisplayTimer = 0;
    int spriteIndex = 0;
    bool isTickReceived = false;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(materialSequence);
    }

    public void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick)
    {
        int materialArrayIndex = beatNumber % materialSequence.SpritesPerBeat.Length;
        currentSpriteArray = materialSequence.SpritesPerBeat[materialArrayIndex].sprites;
        spriteDisplayTimer = 0;
        spriteDisplayDuration = timeToNextTick / materialSequence.SpritesPerBeat[materialArrayIndex].sprites.Length;
        spriteIndex = 0;
        renderer.sprite = currentSpriteArray[spriteIndex];
        isTickReceived = true;
    }

    void FixedUpdate()
    {
        if (isTickReceived)
        {
            spriteDisplayTimer += Time.fixedDeltaTime;
            if (spriteDisplayTimer > spriteDisplayDuration)
            {
                spriteDisplayTimer = 0;
                ++spriteIndex;
                if (spriteIndex == currentSpriteArray.Length)
                {
                    spriteIndex = currentSpriteArray.Length - 1;
                }
                renderer.sprite = currentSpriteArray[spriteIndex];
            }
        }
    }
}
