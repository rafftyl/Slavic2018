﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmIndicator : MonoBehaviour, IGameStateReceiver, IGameStartListener, IRhythmListener
{
    [SerializeField]
    GameObject center;

    [SerializeField]
    int playerIndex = 1;

    Dancer dancer;
    GameState gameState;
    float indicatorSpeed;
    float timeTillNext;

    public GameState GameState { set => gameState = value; }

    Vector3 startIndicatorPos;

    public void GameStarted()
    {
        dancer = gameState.GetDancer(playerIndex);
        dancer.onDanceResult += Dancer_onDanceResult;
    }

    private void Dancer_onDanceResult(bool success)
    {
        if(!success)
        {
            StartCoroutine(ColorChange(Color.red, duration));
        }
        else
        {
            StartCoroutine(ColorChange(Color.green, duration));
        }
    }

    bool pulse = false;
    float duration = 0;
    float time = 0;
    public void MetronomeTick(int measure, int beatNumber, float intensity, bool accent, float timeToNextTick)
    {
        timeTillNext = timeToNextTick;
        if (dancer.CurrentDance.Accents.Contains(beatNumber))
        {
            pulse = true;
            duration = 0.3f * timeTillNext;
            time = 0;
        }
    }

    void Update()
    {
        if (pulse)
        {
            time += Time.deltaTime;
            if (time < duration)
            {
                Vector3 scale = Vector3.one + Vector3.one * Mathf.Sin(2 * Mathf.PI * time / duration);
                center.transform.localScale = scale;
            }
            else
            {
                center.transform.localScale = Vector3.one;
                pulse = false;
            }
        }
    }

    IEnumerator ColorChange(Color color, float dur)
    {
        var image = center.GetComponent<Image>();
        Color prev = image.color;
        image.color = color;
        yield return new WaitForSeconds(dur);
        image.color = prev;
    }
}
