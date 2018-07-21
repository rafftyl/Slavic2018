using System.Collections.Generic;
using UnityEngine;


public class Wooable : MonoBehaviour
{
    [SerializeField]
    public float WOO_LIMIT = 1.0f;
    [SerializeField]
    public float WOO_DECAY = 0.1f;
    public const int NOT_TAKEN = -1;

    private int takenBy = NOT_TAKEN;
    Dictionary<int, float> currentWooFactors = new Dictionary<int, float>();

    private void Update()
    {
        if (takenBy == NOT_TAKEN)
        {
            foreach (var entry in currentWooFactors)
            {
                if (currentWooFactors[entry.Key] > 0.0f)
                {
                    currentWooFactors[entry.Key] -= Time.deltaTime * WOO_DECAY;
                    if (currentWooFactors[entry.Key] < 0.0f)
                    {
                        currentWooFactors[entry.Key] = 0.0f;
                    }
                }
            }
        }
    }

    void Woo(int playerIndex, float wooFactor)
    {
        if(takenBy == NOT_TAKEN)
        {
            if (currentWooFactors.ContainsKey(playerIndex))
            {
                currentWooFactors[playerIndex] += wooFactor;
            }
            else
            {
                currentWooFactors.Add(playerIndex, wooFactor);
            }
            if (wooFactor > 0 && currentWooFactors[playerIndex] >= WOO_LIMIT)
            {
                takenBy = playerIndex;
            }
            else if (wooFactor < 0 && currentWooFactors[playerIndex] < 0.0f)
            {
                currentWooFactors[playerIndex] = 0.0f;
            }
        }
    }
}
