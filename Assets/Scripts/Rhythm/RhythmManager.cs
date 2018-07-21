using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class RhythmManager : MonoBehaviour, IAudioManagerReceiver, IGameStartListener, IGamePauseListener, IGameStopListener
{
    const int SECONDS_IN_MINUTE = 60;

    [SerializeField]
    AudioTrack audioTrack; //TODO: enable multiple tracks
    int currentMeasure = 0;
    int currentBeat = 0;
    float currentBeatTime = 0;

    RhythmData currentRhythmData;
    List<IRhythmListener> rhythmListeners = new List<IRhythmListener>();

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
       
    }

    public void GameStopped()
    {
        throw new System.NotImplementedException();
    }

    public void RegisterRhythmListener(IRhythmListener rhythmListener)
    {
        rhythmListeners.Add(rhythmListener);
    }

    int warmupUpdates = 50;
    int currentUpdate = 0;
    void FixedUpdate()
    {        
        Assert.IsNotNull(AudioManager);
        ++currentUpdate;
        if (currentUpdate > warmupUpdates)
        {
            if (AudioManager.CurrentClip == null)
            {
                AudioManager.PlayMusic(audioTrack.AudioClip);
            }
            else
            {
                Assert.IsNotNull(audioTrack);
                currentRhythmData = audioTrack.GetRhythmDataForMeasure(currentMeasure);
                float currentTimeSignature = currentRhythmData.TimeSignature;
                float currentBeatDuration = SECONDS_IN_MINUTE / currentRhythmData.BeatsPerMinute;

                currentBeatTime += Time.fixedDeltaTime;
                if (currentBeatTime > currentBeatDuration)
                {
                    currentBeatTime = 0;
                    ++currentBeat;
                    if (currentBeat == currentTimeSignature)
                    {
                        ++currentMeasure;
                        currentBeat = 0;
                    }

                    foreach (var listener in rhythmListeners)
                    {
                        listener.MetronomeTick(currentMeasure, currentBeat, currentRhythmData.Intensity, currentRhythmData.IsAccentOnBeat(currentBeat), currentBeatDuration);
                    }
                }
            }
        }
        
    }
    
}