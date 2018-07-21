using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class RhythmManager : MonoBehaviour, IAudioManagerReceiver, IGameStartListener, IGamePauseListener, IGameStopListener
{
    const int SECONDS_IN_MINUTE = 60;

    [SerializeField]
    AudioTrack audioTrack;
    int currentMeasure = 0;
    int currentBeat = 0;
    int currentTimeSignature = 4;
    float currentBeatDuration = 0;
    float currentBeatTime = 0;

    AudioManager manager;
    public AudioManager AudioManager
    {
        private get => manager;
        set => manager = value;
    }

    public void GamePaused()
    {
        throw new System.NotImplementedException();
    }

    public void GameResumed()
    {
        throw new System.NotImplementedException();
    }

    public void GameStarted()
    {
        Assert.IsNotNull(audioTrack);
        RhythmData rhythmData = audioTrack.GetRhythmDataForMeasure(currentMeasure);
        currentTimeSignature = rhythmData.TimeSignature;
        currentBeatDuration = SECONDS_IN_MINUTE / rhythmData.BeatsPerMinute;
    }

    public void GameStopped()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {

    }
    
}