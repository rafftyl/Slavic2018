using UnityEngine;
using System.Collections.Generic;

public abstract class Dance
{
    public abstract string Name { get; }

    HashSet<int> accents;
    public HashSet<int> Accents { get => accents; }  

    int cooldownDuration;
    protected int cooldownRemaining = 0;
    public int CooldownRemaining { get => cooldownRemaining; }

    public Dance(HashSet<int> accents, int cooldown)
    {
        this.accents = accents;
        cooldownDuration = cooldownRemaining = cooldown;
    }

    public virtual void StartDancing(GameObject character)
    {
        cooldownRemaining = cooldownDuration;
    }

    public void Perform(GameObject character)
    {
        --cooldownRemaining;
        if(CooldownRemaining <= 0)
        {
            cooldownRemaining = cooldownDuration;
            OnCooldownFinished(character);
        }
    }

    public void Fail(GameObject character)
    {
        cooldownRemaining = cooldownDuration;
    }

    protected abstract void OnCooldownFinished(GameObject character);
}