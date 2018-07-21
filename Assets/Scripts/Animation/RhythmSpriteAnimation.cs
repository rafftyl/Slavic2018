using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class RhythmSpriteAnimation : RhythmAnimationController, IRhythmListener
{
    [SerializeField]
    RhythmSpriteSequence spriteSequence;
    public RhythmSpriteSequence SpriteSequence { set => spriteSequence = value; }

    Sprite[] currentSpriteArray;
    SpriteRenderer renderer;
    float spriteDisplayDuration;
    float spriteDisplayTimer = 0;
    int spriteIndex = 0;
    bool isTickReceived = false;      

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteSequence);
    }

    public void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick)
    {
        int materialArrayIndex = beatNumber % spriteSequence.SpritesPerBeat.Length;
        currentSpriteArray = spriteSequence.SpritesPerBeat[materialArrayIndex].sprites;
        spriteDisplayTimer = 0;
        spriteDisplayDuration = timeToNextTick / spriteSequence.SpritesPerBeat[materialArrayIndex].sprites.Length;
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
