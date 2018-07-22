using UnityEngine;
using System.Collections.Generic;


public class Dab : Dance
{
    public const float FORCE_IMPULSE = 200.0f;
    private float cooldown = 0.0f;

    public override string Name => "Dab";

    public Dab() : base(new HashSet<int> { 1, 3 }, 2)
    {

    }

    protected override void OnCooldownFinished(GameObject character)
    {
        if (character.GetComponent<Dancer>().MovementDirection.magnitude > 0.0f)
        {
            character.GetComponent<Rigidbody>().AddForce(character.GetComponent<Dancer>().MovementDirection * FORCE_IMPULSE);
        }
    }

    public override float GetEffectRadius()
    {
        return 0.0f;
    }
}