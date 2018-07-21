﻿using UnityEngine;


public class Rodeo : Dance
{
    const float WOO_VALUE = 0.05f;
    const float WOO_RANGE = 10.0f;

    public override void StartDancing(GameObject character)
    {
    }

    public override void Perform(GameObject character)
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
}