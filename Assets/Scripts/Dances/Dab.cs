using UnityEngine;


public class Dab : Dance
{
    public const float THRUST_COOLDOWN = 0.5f;
    public const float FORCE_IMPULSE = 75.0f;

    private float cooldown = 0.0f;

    public override void StartDancing()
    {
        cooldown = THRUST_COOLDOWN;
    }

    public override void Perform(GameObject character)
    {
        if (cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
        }
        else if (character.GetComponent<CharacterManager>().MovementDirection.magnitude > 0.0f)
        {
            character.GetComponent<Rigidbody>().AddForce(character.GetComponent<CharacterManager>().MovementDirection * FORCE_IMPULSE);
            cooldown = THRUST_COOLDOWN;
        }
    }
}