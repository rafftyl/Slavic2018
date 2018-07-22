using UnityEngine;
using System.Collections.Generic;


public class HipSweep : Dance
{
    public const float FORCE_IMPULSE = 500.0f;
    public override string Name => "HipSweep";

    public HipSweep() : base(new HashSet<int> { 0, 2 }, 1)
    {
    }

    protected override void OnCooldownFinished(GameObject character)
    {
        if (character.GetComponent<Dancer>().MovementDirection.magnitude > 0.0f)
        {
            character.GetComponent<Rigidbody>().AddForce(character.GetComponent<Dancer>().MovementDirection * FORCE_IMPULSE);
            character.AddComponent<DamageComponent>().playerNumber = character.GetComponent<Dancer>().PlayerNumber;
        }
    }

    public override float GetEffectRadius()
    {
        return 0.0f;
    }
}