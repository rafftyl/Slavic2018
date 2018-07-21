using UnityEngine;
using System.Collections.Generic;

public class Rodeo : Dance
{
    const float WOO_VALUE = 0.3f;
    const float WOO_RANGE = 0.5f;

    public override string Name => "Rodeo";

    public Rodeo() : base(new HashSet<int> { 0, 1, 2, 3 }, 1)
    {
    }

    protected override void OnCooldownFinished(GameObject character)
    {
        Collider[] collidersInRange = Physics.OverlapSphere(character.transform.position, WOO_RANGE);
        foreach (var collider in collidersInRange)
        {
            Girl girl = collider.gameObject.GetComponent<Girl>();
            if (girl != null)
            {
                girl.Woo(character.GetComponent<Dancer>().PlayerNumber, WOO_VALUE * Time.deltaTime);
            }
        }
    }

    public override float GetEffectRadius()
    {
        return WOO_RANGE;
    }
}