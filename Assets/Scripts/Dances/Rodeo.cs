using UnityEngine;
using UnityEngine.Assertions;


public class Rodeo : Dance
{
    const float SPEED = 50.0f;

    public override void StartDancing()
    {
    }

    public override void Perform(GameObject character)
    {
        Assert.IsNotNull(character, "Null character");
        character.GetComponent<Rigidbody>().velocity = character.GetComponent<CharacterManager>().MovementDirection * SPEED * Time.deltaTime;
    }
}