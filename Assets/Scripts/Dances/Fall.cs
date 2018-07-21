using UnityEngine;


public class Fall : Dance
{
    const float DURATION_TIME = 2.0f;
    private float timeLeft = 0.0f;
    public float TimeLeft { get => timeLeft; }

    public override void StartDancing()
    {
        timeLeft = DURATION_TIME;
    }

    public override void Perform(GameObject character)
    {
        if(timeLeft > 0.0f)
        {
            timeLeft -= Time.deltaTime;
        }
    }
}