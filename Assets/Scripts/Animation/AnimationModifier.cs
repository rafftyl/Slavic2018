using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(RhythmAnimationController))]
public class AnimationModifier : MonoBehaviour
{
    RhythmAnimationController animController;

    [SerializeField]
    List<RhythmSpriteSequence> spriteSequences;
}