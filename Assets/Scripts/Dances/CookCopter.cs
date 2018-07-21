using UnityEngine;


public class CookCopter : Dance
{
    const float SPIN_TIME = 2.0f;
    const float WOO_VALUE = 0.5f;
    const float WOO_RANGE = 20.0f;
    private float currentSpin = 0.0f;

    public override void StartDancing(GameObject character)
    {
        currentSpin = 0.0f;
    }

    public override void Perform(GameObject character)
    {
        if (currentSpin < SPIN_TIME)
        {
            currentSpin += Time.deltaTime;
        }
        else if (currentSpin >= SPIN_TIME)
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
}