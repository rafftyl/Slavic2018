using UnityEngine;
using UnityEngine.Assertions;


public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private int playerNumber = 1;
    public int PlayerNumber { get => playerNumber; }
    private Fall fallDance = new Fall();
    private Dance[] selectableDances = { new Rodeo(), new HipSweep(), new Dab(), new CookCopter() };
    private int selectedDanceIndex = 0;
    private Vector3 movementDirection;
    public Vector3 MovementDirection { get => movementDirection; }

    void Awake ()
    {
        Assert.IsTrue(playerNumber > 0, "Too small player number");
        Assert.IsNotNull(GetComponent<Rigidbody>(), "Missing player Rigidbody");
        Assert.IsTrue(selectableDances.Length >= 1, "No selectable dances");
    }
	
	void Update ()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal_" + playerNumber);
        movementDirection.y = 0;
        movementDirection.z = Input.GetAxisRaw("Vertical_" + playerNumber);
        movementDirection = movementDirection.normalized;
        if (fallDance.TimeLeft > 0.0f)
        {
            fallDance.Perform(gameObject);
            if (fallDance.TimeLeft <= 0.0f)
            {
                selectedDanceIndex = 0;
                selectableDances[selectedDanceIndex].StartDancing();
            }
        }
        else
        {
            if (Input.GetButtonDown("NextDance_" + playerNumber))
            {
                ++selectedDanceIndex;
                if (selectedDanceIndex >= selectableDances.Length)
                {
                    selectedDanceIndex = 0;
                }
                selectableDances[selectedDanceIndex].StartDancing();
            }
            else if (Input.GetButtonDown("PreviousDance_" + playerNumber))
            {
                --selectedDanceIndex;
                if (selectedDanceIndex < 0)
                {
                    selectedDanceIndex = selectableDances.Length - 1;
                }
                selectableDances[selectedDanceIndex].StartDancing();
            }
            else
            {
                selectableDances[selectedDanceIndex].Perform(gameObject);
            }
        }
    }

    public void FallOver()
    {
        fallDance.StartDancing();
    }
}
