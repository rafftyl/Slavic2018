using System.Collections.Generic;
using UnityEngine;


public class Girl : MonoBehaviour
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
            //TODO: gamestate receiver
            for (int i = 1; i < 10; ++i)
            {
                if(currentWooFactors.ContainsKey(i))
                {
                    if (currentWooFactors[i] > 0.0f)
                    {
                        currentWooFactors[i] -= Time.deltaTime * WOO_DECAY;
                        if (currentWooFactors[i] < 0.0f)
                        {
                            currentWooFactors[i] = 0.0f;
                        }
                    }
                }
            }
        }
        //TODO: animacja
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
            }
            if (wooFactor > 0 && currentWooFactors[playerNumber] >= WOO_LIMIT)
            {
                takenBy = playerNumber;
            }
            else if (wooFactor < 0 && currentWooFactors[playerNumber] < 0.0f)
            {
                currentWooFactors[playerNumber] = 0.0f;
            }
        }
    }

    public void Retaliation(int playerNumber)
    {
        if (currentWooFactors.ContainsKey(playerNumber))
        {
            currentWooFactors[playerNumber] = 0.0f;
            if(takenBy == playerNumber)
            {
                takenBy = NOT_TAKEN;
            }
        }
        //TODO: animacja plaskacza
    }
}
