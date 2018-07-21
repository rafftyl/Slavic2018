using UnityEngine;


public class Fall : Dance
{
    const float WOO_LOST_RANGE = 1.0f;
    const float WOO_LOST_VALUE = -0.4f;
    const float DURATION_TIME = 2.0f;
    private float timeLeft = 0.0f;
    public float TimeLeft { get => timeLeft; }

    public override void StartDancing(GameObject character)
    {
        timeLeft = DURATION_TIME;
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

    public override void Perform(GameObject character)
    {
        if(timeLeft > 0.0f)
        {
            timeLeft -= Time.deltaTime;
        }
    }

    public override float GetEffectRadius()
    {
        return 0.0f;
    }
}