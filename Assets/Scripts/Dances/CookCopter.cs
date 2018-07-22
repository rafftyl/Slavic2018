using UnityEngine;
using System.Collections.Generic;

public class CookCopter : Dance
{
    const float WOO_VALUE = 0.5f;
    const float WOO_RANGE = 0.7f;

    public override string Name => "CockCopter";

    public CookCopter() : base(new HashSet<int> { 0, 1, 2, 3 }, 2)
    {

    }

    public override void StartDancing(GameObject character)
    {
        base.StartDancing(character);
    }
    
    protected override void OnCooldownFinished(GameObject character)
    {
        Collider[] collidersInRange = Physics.OverlapSphere(character.transform.position, WOO_RANGE);
        foreach (var collider in collidersInRange)
        {
            Girl girl = collider.gameObject.GetComponent<Girl>();
            if (girl != null)
            {
                girl.Woo(character.GetComponent<Dancer>().PlayerNumber, WOO_VALUE);
            }
        }
    }

    public override float GetEffectRadius()
    {
        return WOO_RANGE;
    }
}