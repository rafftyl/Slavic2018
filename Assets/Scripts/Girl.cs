using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class Girl : MonoBehaviour, IGameStateReceiver
{
    private GameState gameState;
    public RhythmSpriteSequence unimpressed;
    public RhythmSpriteSequence idle;
    public RhythmSpriteSequence happy;
    public RhythmSpriteSequence excited;
    public RhythmSpriteSequence slap;
    private RhythmSpriteAnimation rhythmSpriteAnimation;

    [SerializeField]
    public float WOO_TOP_LIMIT = 5.0f;
    [SerializeField]
    public float WOO_EXCITED_LIMIT = 4.0f;
    private const int WOO_EXCITED_INDEX = 3;
    [SerializeField]
    public float WOO_HAPPY_LIMIT = 2.0f;
    private const int WOO_HAPPY_INDEX = 2;
    [SerializeField]
    public float WOO_IDLE_LIMIT = 0.0f;
    private const int WOO_IDLE_INDEX = 1;
    [SerializeField]
    public float WOO_BOTTOM_LIMIT = -2.0f;
    private const int WOO_UNIMPRESSED_INDEX = 0;
    [SerializeField]
    public float WOO_DECAY = 0.1f;
    [SerializeField]
    public float SLAP_COOLDOWN = 1.0f;
    [SerializeField]
    public float REACTION_RANGE = 3.0f;
    [SerializeField]
    public float SCORE_RATE = 0.3f;
    [SerializeField]
    public float VELOCITY = 1.0f;
    [SerializeField]
    public float DISTANCE_TOLERANCE = 1.0f;

    public const int NOT_TAKEN = -1;
    private int takenBy = NOT_TAKEN;
    private const int SLAP_ANIMATION_INDEX = -1;
    private float slapCooldown = 0.0f;
    Dictionary<int, float> currentWooFactors = new Dictionary<int, float>();
    KeyValuePair<float, RhythmSpriteSequence>[] animationChangeBorders;
    private int currentAnimationIndex = SLAP_ANIMATION_INDEX;


    private void Awake()
    {
        rhythmSpriteAnimation = GetComponentInChildren<RhythmSpriteAnimation>();
        Assert.IsNotNull(rhythmSpriteAnimation, "Missing rhythm sprite animation");
        Assert.IsNotNull(unimpressed, "Missing unimpressed rhythm sprite sequence");
        Assert.IsNotNull(idle, "Missing idle rhythm sprite sequence");
        Assert.IsNotNull(happy, "Missing happy rhythm sprite sequence");
        Assert.IsNotNull(excited, "Missing excited rhythm sprite sequence");
        Assert.IsNotNull(slap, "Missing slap rhythm sprite sequence");
        animationChangeBorders = new KeyValuePair<float, RhythmSpriteSequence>[4];
        animationChangeBorders[WOO_UNIMPRESSED_INDEX] = new KeyValuePair<float, RhythmSpriteSequence>(WOO_BOTTOM_LIMIT, null);
        animationChangeBorders[WOO_IDLE_INDEX] = new KeyValuePair<float, RhythmSpriteSequence>(WOO_IDLE_LIMIT, null);
        animationChangeBorders[WOO_HAPPY_INDEX] = new KeyValuePair<float, RhythmSpriteSequence>(WOO_HAPPY_LIMIT, null);
        animationChangeBorders[WOO_EXCITED_INDEX] = new KeyValuePair<float, RhythmSpriteSequence>(WOO_EXCITED_LIMIT, null);
    }

    private void Update()
    {
        if (slapCooldown > 0.0f)
        {
            slapCooldown -= Time.deltaTime;
        }
        
        for (int i = 1; i <= GameState.MAX_PLAYERS; ++i)
        {
            if (currentWooFactors.ContainsKey(i) && i != takenBy)
            {
                if (currentWooFactors[i] > WOO_IDLE_LIMIT)
                {
                    currentWooFactors[i] -= Time.deltaTime * WOO_DECAY;
                    if (currentWooFactors[i] < WOO_IDLE_LIMIT)
                    {
                        currentWooFactors[i] = WOO_IDLE_LIMIT;
                    }
                }
                else if (currentWooFactors[i] < WOO_IDLE_LIMIT)
                {
                    currentWooFactors[i] += Time.deltaTime * WOO_DECAY;
                    if (currentWooFactors[i] > WOO_IDLE_LIMIT)
                    {
                        currentWooFactors[i] = WOO_IDLE_LIMIT;
                    }
                }
            }
        }
        if(takenBy != NOT_TAKEN)
        {
            gameState.AddScoreForPlayer(takenBy, SCORE_RATE * Time.deltaTime);
            Collider[] collidersInRange = Physics.OverlapSphere(transform.position, 1000.0f);
            Dancer dancer = null;
            foreach (var collider in collidersInRange)
            {
                dancer = collider.gameObject.GetComponent<Dancer>();
                if (dancer != null && dancer.PlayerNumber == takenBy)
                {
                    break;
                }
            }
            if(dancer != null && Vector3.Distance(transform.position, dancer.transform.position) > DISTANCE_TOLERANCE)
            {
                GetComponent<Rigidbody>().velocity = (dancer.transform.position - transform.position).normalized * VELOCITY;
            }
            else
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        DetermineAnimation();
    }

    public void Woo(int playerNumber, float wooFactor)
    {
        if(takenBy == NOT_TAKEN)
        {
            if (currentWooFactors.ContainsKey(playerNumber))
            {
                currentWooFactors[playerNumber] += wooFactor;
            }
            else
            {
                currentWooFactors.Add(playerNumber, wooFactor);
                currentWooFactors[playerNumber] += WOO_IDLE_LIMIT;
            }
            if (wooFactor > 0 && currentWooFactors[playerNumber] >= WOO_TOP_LIMIT)
            {
                takenBy = playerNumber;
            }
            else if (wooFactor < 0 && currentWooFactors[playerNumber] < WOO_BOTTOM_LIMIT)
            {
                currentWooFactors[playerNumber] = WOO_BOTTOM_LIMIT;
            }
        }
    }

    public void Retaliation(int playerNumber)
    {
        if (currentWooFactors.ContainsKey(playerNumber))
        {
            currentWooFactors[playerNumber] = WOO_BOTTOM_LIMIT;
            if(takenBy == playerNumber)
            {
                takenBy = NOT_TAKEN;
            }
        }
        slapCooldown = SLAP_COOLDOWN;
    }

    private void DetermineAnimation()
    {
        int newAnimationIndex = 0;
        KeyValuePair<int, float> closestDancerData = new KeyValuePair<int, float>(-1, 10000.0f);
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, REACTION_RANGE);
        foreach (var collider in collidersInRange)
        {
            Dancer dancer = collider.gameObject.GetComponent<Dancer>();
            if (dancer != null)
            {
                if(closestDancerData.Key == -1 || Vector3.Distance(gameObject.transform.position, dancer.transform.position) < closestDancerData.Value)
                {
                    closestDancerData = new KeyValuePair<int, float>(dancer.PlayerNumber, Vector3.Distance(gameObject.transform.position, dancer.transform.position));
                }
            }
        }
        if(!currentWooFactors.ContainsKey(closestDancerData.Key))
        {
            currentWooFactors.Add(closestDancerData.Key, WOO_IDLE_LIMIT);
        }
        if(slapCooldown > 0.0f)
        {
            newAnimationIndex = SLAP_ANIMATION_INDEX;
        }
        else if(currentWooFactors[closestDancerData.Key] >= WOO_EXCITED_LIMIT)
        {
            newAnimationIndex = WOO_EXCITED_INDEX;
        }
        else if (currentWooFactors[closestDancerData.Key] >= WOO_HAPPY_LIMIT)
        {
            newAnimationIndex = WOO_HAPPY_INDEX;
        }
        else if (currentWooFactors[closestDancerData.Key] >= WOO_IDLE_LIMIT)
        {
            newAnimationIndex = WOO_IDLE_INDEX;
        }
        else
        {
            newAnimationIndex = WOO_UNIMPRESSED_INDEX;
        }
        if (newAnimationIndex != currentAnimationIndex)
        {
            currentAnimationIndex = newAnimationIndex;
            switch(currentAnimationIndex)
            {
                case SLAP_ANIMATION_INDEX:
                    rhythmSpriteAnimation.SpriteSequence = slap;
                break;
                    
                case WOO_EXCITED_INDEX:
                    rhythmSpriteAnimation.SpriteSequence = excited;
                    break;
                    
                case WOO_HAPPY_INDEX:
                    rhythmSpriteAnimation.SpriteSequence = happy;
                    break;

                case WOO_IDLE_INDEX:
                    rhythmSpriteAnimation.SpriteSequence = idle;
                    break;
                    
                case WOO_UNIMPRESSED_INDEX:
                    rhythmSpriteAnimation.SpriteSequence = unimpressed;
                break;
            }
        }
    }

    public GameState GameState { set => gameState = value; }
}
