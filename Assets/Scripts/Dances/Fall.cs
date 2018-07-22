using UnityEngine;
using System.Collections.Generic;


public class Fall : Dance
{
    const float WOO_LOST_RANGE = 1.0f;
    const float WOO_LOST_VALUE = -7.0f;

    public override string Name => "Fall";

    public Fall() : base(new HashSet<int> { 0, 2 }, 2)
    {

    }

    public override void StartDancing(GameObject character)
    {
        base.StartDancing(character);
        Collider[] collidersInRange = Physics.OverlapSphere(character.transform.position, WOO_LOST_RANGE);
        foreach (var collider in collidersInRange)
        {
            Girl girl = collider.gameObject.GetComponent<Girl>();
            if (girl != null)
            {
                girl.Woo(character.GetComponent<Dancer>().PlayerNumber, WOO_LOST_VALUE);
            }
        }
    }

    protected override void OnCooldownFinished(GameObject character)
    {
        character.GetComponent<Dancer>().IsStanding = true;
    }

    public override float GetEffectRadius()
    {
        return 0.0f;
    }
}