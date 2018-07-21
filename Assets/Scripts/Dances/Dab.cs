using UnityEngine;
using System.Collections.Generic;

public class Dab : Dance
{
    public const float THRUST_COOLDOWN = 0.5f;
    public const float FORCE_IMPULSE = 75.0f;

    private float cooldown = 0.0f;

    public override string Name => "Dab";

    public Dab() : base(new HashSet<int> { 1, 3 }, 2)
    {

    }

    public override void StartDancing(GameObject character)
    {
        base.StartDancing(character);
        cooldown = THRUST_COOLDOWN;
    }    

    protected override void OnCooldownFinished(GameObject character)
    {
        if (character.GetComponent<Dancer>().MovementDirection.magnitude > 0.0f)
        {
            character.GetComponent<Rigidbody>().AddForce(character.GetComponent<Dancer>().MovementDirection * FORCE_IMPULSE);
            cooldown = THRUST_COOLDOWN;
        }
    }

    public override float GetEffectRadius()
    {
        return 0.0f;
    }
}