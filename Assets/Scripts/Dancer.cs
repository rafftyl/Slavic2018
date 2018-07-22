using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;

[System.Serializable]
class DanceAnimationPair
{
    public string danceName;
    public RhythmSpriteSequence sequence;
}

public class Dancer : MonoBehaviour, IRhythmListener, IGameStateReceiver
{
    const float BEAT_TOLERANCE = 0.65f;

    [SerializeField]
    List<DanceAnimationPair> danceAnimationPairs;

    [SerializeField]
    RhythmSpriteAnimation spriteAnimation;

    [SerializeField]
    private int playerNumber = 1;
    public int PlayerNumber { get => playerNumber; }
    private Fall fallDance = new Fall();
    private Dance[] selectableDances = { /*new Rodeo(),*/  new Dab(), new HipSweep(), new CookCopter() };
    Dance currentDance;
    public Dance CurrentDance { get => currentDance; }

    private int selectedDanceIndex = 0;
    private Vector3 movementDirection;
    public Vector3 MovementDirection { get => movementDirection; }

    float beatTolerance = 0;
    
    private int segments = 50;
    LineRenderer line;

    bool isStanding = true;
    public bool IsStanding
    {
        get => isStanding;
        set
        {
            isStanding = value;
            if(isStanding)
            {
                selectedDanceIndex = 0;
                currentDance = selectableDances[selectedDanceIndex];
                currentDance.StartDancing(gameObject);
                SetDanceAnimation(currentDance.Name);
                UpdateLinePoints();
            }            
        }
    }

    public GameState GameState { set => value.RegisterDancer(this, playerNumber); }

    float timeToAccent = 0;
    float timeToNextAccent = 0;

    void Awake ()
    {
        Assert.IsNotNull(spriteAnimation);
        Assert.IsTrue(playerNumber > 0, "Too small player number");
        Assert.IsNotNull(GetComponent<Rigidbody>(), "Missing player Rigidbody");
        Assert.IsTrue(selectableDances.Length >= 1, "No selectable dances");
        currentDance = selectableDances[selectedDanceIndex];
    }

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        UpdateLinePoints();
    }

    bool timeSet = false;
    void Update ()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal_" + playerNumber);
        movementDirection.y = 0;
        movementDirection.z = Input.GetAxisRaw("Vertical_" + playerNumber);
        movementDirection = movementDirection.normalized;

        timeToAccent -= Time.deltaTime;
        timeToNextAccent -= Time.deltaTime;

        if (Input.GetButtonDown("Beat_" + playerNumber))
        {
            if (Mathf.Abs(timeToAccent) < beatTolerance || Mathf.Abs(timeToNextAccent) < beatTolerance)
            {
                CurrentDance.Perform(gameObject);
            }
            else
            {
                Debug.Log("Failure");
                CurrentDance.Fail(gameObject);
            }
        }

        if (IsStanding)
        {
            if (Input.GetButtonDown("NextDance_" + playerNumber))
            {
                ++selectedDanceIndex;
                if (selectedDanceIndex >= selectableDances.Length)
                {
                    selectedDanceIndex = 0;
                }
                currentDance = selectableDances[selectedDanceIndex];
                currentDance.StartDancing(gameObject);
                SetDanceAnimation(currentDance.Name);
                UpdateLinePoints();
            }
            else if (Input.GetButtonDown("PreviousDance_" + playerNumber))
            {
                --selectedDanceIndex;
                if (selectedDanceIndex < 0)
                {
                    selectedDanceIndex = selectableDances.Length - 1;
                }
                currentDance = selectableDances[selectedDanceIndex];
                currentDance.StartDancing(gameObject);
                SetDanceAnimation(currentDance.Name);
                UpdateLinePoints();
            }
        }
    }

    public void FallOver()
    {
        if (IsStanding)
        {
            fallDance.StartDancing(gameObject);
            currentDance = fallDance;
            SetDanceAnimation(fallDance.Name);
            isStanding = false;
            UpdateLinePoints();
        }
    }

    public void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick)
    {
        beatTolerance = BEAT_TOLERANCE * timeToNextTick;
        int nextBeat = beatNumber + 1;
        if (nextBeat == 4)
        {
            nextBeat = 0;
        }

        if (CurrentDance.Accents.Contains(nextBeat))
        {
            if (!timeSet)
            {
                timeToAccent = timeToNextTick;
                timeSet = true;
            }
            else
            {
                timeToNextAccent = timeToNextTick;
                timeSet = false;
            }
        }        
    }

    void SetDanceAnimation(string danceName)
    {
        var animationPair = danceAnimationPairs.Find((DanceAnimationPair pair) => { return pair.danceName == danceName; });
        Assert.IsNotNull(animationPair);
        spriteAnimation.SpriteSequence = animationPair.sequence;
    }

    void UpdateLinePoints()
    {
        float radius = currentDance.GetEffectRadius();
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
