using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    public const float LIFE_TIME = 0.5f;
    public int playerNumber = 0;
    private float timeLeft;
    
    private void Start()
    {
        timeLeft = LIFE_TIME;
    }

    void OnCollisionEnter(Collision collision)
    {
        Girl girl = collision.other.GetComponent<Girl>();
        if (girl != null)
        {
            girl.Retaliation(playerNumber);
            gameObject.GetComponent<Dancer>().FallOver();
        }
        else
        {
            Dancer dancer = collision.other.GetComponent<Dancer>();
            if (dancer != null && dancer.PlayerNumber != playerNumber)
            {
                dancer.FallOver();
            }
        }
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0.0f)
        {
            Destroy(this);
        }
    }
}
