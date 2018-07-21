using UnityEngine;
using System.Collections.Generic;


public class HipSweep : Dance
{
    public const float THRUST_COOLDOWN = 1.0f;
    public const float FORCE_IMPULSE = 500.0f;

    private float cooldown = 0.0f;

    public override string Name => "HipSweep";

    public HipSweep() : base(new HashSet<int> { 0, 1, 2, 3 }, 1)
    {
    }

    public override void StartDancing(GameObject character)
    {
        cooldown = THRUST_COOLDOWN;
    }

    protected override void OnCooldownFinished(GameObject character)
    {
        if (character.GetComponent<Dancer>().MovementDirection.magnitude > 0.0f)
        {
            character.GetComponent<Rigidbody>().AddForce(character.GetComponent<Dancer>().MovementDirection * FORCE_IMPULSE);
            cooldown = THRUST_COOLDOWN;
            character.AddComponent<DamageComponent>().playerNumber = character.GetComponent<Dancer>().PlayerNumber;
        }
    }

    public override float GetEffectRadius()
    {
        return 0.25f;
    }
}