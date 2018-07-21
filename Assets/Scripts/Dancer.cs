using UnityEngine;
using UnityEngine.Assertions;


public class Dancer : MonoBehaviour
{
    [SerializeField]
    private int playerNumber = 1;
    public int PlayerNumber { get => playerNumber; }
    private Fall fallDance = new Fall();
    private Dance[] selectableDances = { new Rodeo(), new HipSweep(), new Dab(), new CookCopter() };
    private int selectedDanceIndex = 0;
    private Vector3 movementDirection;
    public Vector3 MovementDirection { get => movementDirection; }
    
    private int segments = 50;
    LineRenderer line;


    void Awake ()
    {
        Assert.IsTrue(playerNumber > 0, "Too small player number");
        Assert.IsNotNull(GetComponent<Rigidbody>(), "Missing player Rigidbody");
        Assert.IsTrue(selectableDances.Length >= 1, "No selectable dances");
    }

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
    }

    void Update ()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal_" + playerNumber);
        movementDirection.y = 0;
        movementDirection.z = Input.GetAxisRaw("Vertical_" + playerNumber);
        movementDirection = movementDirection.normalized;
        if (!IsStanding())
        {
            fallDance.Perform(gameObject);
            if (fallDance.TimeLeft <= 0.0f)
            {
                selectedDanceIndex = 0;
                selectableDances[selectedDanceIndex].StartDancing(gameObject);
                UpdateLinePoints(selectableDances[selectedDanceIndex].GetEffectRadius());
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
                selectableDances[selectedDanceIndex].StartDancing(gameObject);
                UpdateLinePoints(selectableDances[selectedDanceIndex].GetEffectRadius());
            }
            else if (Input.GetButtonDown("PreviousDance_" + playerNumber))
            {
                --selectedDanceIndex;
                if (selectedDanceIndex < 0)
                {
                    selectedDanceIndex = selectableDances.Length - 1;
                }
                selectableDances[selectedDanceIndex].StartDancing(gameObject);
                UpdateLinePoints(selectableDances[selectedDanceIndex].GetEffectRadius());
            }
            else
            {
                selectableDances[selectedDanceIndex].Perform(gameObject);
            }
        }
    }

    public void FallOver()
    {
        if(IsStanding())
        {
            fallDance.StartDancing(gameObject);
            UpdateLinePoints(selectableDances[selectedDanceIndex].GetEffectRadius());
        }
    }

    public bool IsStanding()
    {
        return fallDance.TimeLeft <= 0.0f;
    }

    void UpdateLinePoints(float radius)
    {
        if(radius <= 0.0f)
        {
            line.enabled = false;
        }
        else
        {
            line.enabled = true;
            float x;
            float z;
            float angle = 20f;
            for (int i = 0; i < (segments + 1); i++)
            {
                x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
                line.SetPosition(i, new Vector3(x, 0, z));
                angle += (360f / segments);
            }
        }
    }
}
