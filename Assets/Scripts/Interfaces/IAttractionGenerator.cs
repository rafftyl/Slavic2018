using UnityEngine;

public interface IAttractionGenerator
{
    Vector2 Position { get; }
    float Attraction { get; }
    float AttractionFalloff { get; }
}