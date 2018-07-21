using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmIndicator : MonoBehaviour, IGameStateReceiver, IGameStartListener, IRhythmListener
{
    [SerializeField]
    GameObject indicator;

    [SerializeField]
    GameObject center;

    [SerializeField]
    int playerIndex = 1;

    Dancer dancer;
    GameState gameState;
    float indicatorSpeed;

    public GameState GameState { set => gameState = value; }

    Vector3 startIndicatorPos;

    public void GameStarted()
    {
        dancer = gameState.GetDancer(playerIndex);
    }

    public void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick)
    {
        int nextBeat = beatNumber + 1;
        if(nextBeat == 4)
        {
            nextBeat = 0;
        }

        if(dancer.CurrentDance.Accents.Contains(nextBeat))
        {
            indicator.transform.position = startIndicatorPos;
        }
        
        indicatorSpeed = (startIndicatorPos - center.transform.position).magnitude / timeToNextTick;
    }

    void Update()
    {
        Vector3 currentPos = indicator.transform.position;
        currentPos = Vector3.MoveTowards(currentPos, center.transform.position, indicatorSpeed * Time.deltaTime);
        indicator.transform.position = currentPos;
    }
}
