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
    const float BEAT_TOLERANCE = 1.0f;

    [SerializeField]
    List<DanceAnimationPair> danceAnimationPairs;

    [SerializeField]
    RhythmSpriteAnimation spriteAnimation;

    [SerializeField]
    private int playerNumber = 1;
    public int PlayerNumber { get => playerNumber; }
    private Fall fallDance = new Fall();
    private Dance[] selectableDances = { new Rodeo(), new HipSweep(), new Dab(), new CookCopter() };
    Dance currentDance;
    public Dance CurrentDance { get => currentDance; }

    private int selectedDanceIndex = 0;
    private Vector3 movementDirection;
    public Vector3 MovementDirection { get => movementDirection; }
    
    float previousBeatTimestamp;
    float beatPressedTimestamp;
    float beatDuration = 0;
    float beatTolerance = 0;
    float lastLateBeat = 0;
    float lastLatePress = 0;
    int previousBeatNumber = 0;
    
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

    void Update ()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal_" + playerNumber);
        movementDirection.y = 0;
        movementDirection.z = Input.GetAxisRaw("Vertical_" + playerNumber);
        movementDirection = movementDirection.normalized;

        if (Input.GetButtonUp("Beat_" + playerNumber))
        {
            beatPressedTimestamp = Time.time;

            if (Mathf.Abs(lastLateBeat - beatPressedTimestamp) < beatTolerance)
            {
                Debug.Log("Succesful beat " + previousBeatNumber + " tick tmstp: " + previousBeatTimestamp + " beat pressed: " + beatPressedTimestamp + " tolerance: " + beatTolerance);
                currentDance.Perform(gameObject);
                lastLateBeat = 0;
                lastLatePress = 0.0f;
            }
            else if(lastLatePress == 0.0f)
            {
                lastLatePress = beatPressedTimestamp;                
            }
        }

        if (lastLateBeat > 0.0f)
        {
            if (Mathf.Abs(previousBeatTimestamp - beatPressedTimestamp) > beatTolerance)
            {
                //Debug.Log("Unsuccesful beat " + previousBeatNumber + " tick tmstp: " + previousBeatTimestamp + " beat pressed: " + beatPressedTimestamp + " tolerance: " + beatTolerance);
                currentDance.Fail(gameObject);
                lastLateBeat = 0.0f;
            }
        }

        if(lastLatePress > 0.0f && Mathf.Abs(Time.time - lastLatePress) > beatTolerance)
        {
            //Debug.Log("Unsuccesful beat " + previousBeatNumber + " tick tmstp: " + previousBeatTimestamp + " beat pressed: " + beatPressedTimestamp + " tolerance: " + beatTolerance);
            currentDance.Fail(gameObject);
            lastLateBeat = 0.0f;
            lastLatePress = 0.0f;
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
        previousBeatNumber = beatNumber;

        if (currentDance.Accents.Contains(previousBeatNumber))
        {
            previousBeatTimestamp = Time.time;
            beatDuration = timeToNextTick;
            beatTolerance = BEAT_TOLERANCE * beatDuration;
           
            if (Mathf.Abs(previousBeatTimestamp - beatPressedTimestamp) < beatTolerance)
            {
                Debug.Log("Succesful beat " + previousBeatNumber + " tick tmstp: " + previousBeatTimestamp + " beat pressed: " + beatPressedTimestamp + " tolerance: " + beatTolerance);
                currentDance.Perform(gameObject);
                lastLateBeat = 0;
                lastLatePress = 0;
            }
            else
            {
                lastLateBeat = previousBeatTimestamp;                
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
