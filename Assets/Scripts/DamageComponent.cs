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

    private void Update()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(gameObject.transform.position, 0.5f);
        foreach (var collider in collidersInRange)
        {
            Girl girl = collider.gameObject.GetComponent<Girl>();
            if (girl != null)
            {
                girl.Retaliation(playerNumber);
                gameObject.GetComponent<Dancer>().FallOver();
            }
            else
            {
                Dancer dancer = collider.gameObject.GetComponent<Dancer>();
                if(dancer != null && dancer.PlayerNumber != playerNumber)
                {
                    dancer.FallOver();
                }
            }
        }
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0.0f)
        {
            Destroy(this);
        }
    }
}
