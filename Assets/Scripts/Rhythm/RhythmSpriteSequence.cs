using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct SpriteArray
{
    [SerializeField]
    public Sprite[] sprites;
}

[CreateAssetMenu(fileName = "SpriteSequence", menuName = "Rhythm/RhythmSpriteSequence")]
public class RhythmSpriteSequence : ScriptableObject
{
    [SerializeField]
    SpriteArray[] spritesPerBeat;
    public SpriteArray[] SpritesPerBeat { get => spritesPerBeat; }
}