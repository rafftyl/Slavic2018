using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    [SerializeField]
    RhythmManager rhythmManager;

    [SerializeField]
    AudioManager audioManager;

    List<IGameStartListener> gameStartListeners = new List<IGameStartListener>();
    List<IGameStopListener> gameStopListeners = new List<IGameStopListener>();
    List<IGamePauseListener> gamePauseListeners = new List<IGamePauseListener>();

    void Awake()
    {
        Assert.IsNotNull(rhythmManager);
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        //TODO: generalize dependency injection
        foreach (var obj in rootObjects)
        {
            var rhythmListeners = obj.GetComponentsInChildren<IRhythmListener>();
            foreach (var listener in rhythmListeners)
            {
                rhythmManager.RegisterRhythmListener(listener);
            }

            gameStartListeners.AddRange(obj.GetComponentsInChildren<IGameStartListener>());
            gameStopListeners.AddRange(obj.GetComponentsInChildren<IGameStopListener>());
            gamePauseListeners.AddRange(obj.GetComponentsInChildren<IGamePauseListener>());

            var gameStateReceivers = obj.GetComponentsInChildren<IGameStateReceiver>();
            foreach (var receiver in gameStateReceivers)
            {
                receiver.GameState = this;
            }

            var audioReceivers = obj.GetComponentsInChildren<IAudioManagerReceiver>();
            foreach (var receiver in audioReceivers)
            {
                receiver.AudioManager = audioManager;
            }

            //TODO: inject remaining dependencies
        }
    }

    public void StartGame()
    {
        foreach(var listener in gameStartListeners)
        {
            listener.GameStarted();
        }
    }

    public void PauseGame()
    {
        foreach(var listener in gamePauseListeners)
        {
            listener.GamePaused();
        }
    }

    public void ResumeGame()
    {
        foreach (var listener in gamePauseListeners)
        {
            listener.GameResumed();
        }
    }

    public void StopGame()
    {
        foreach(var listener in gameStopListeners)
        {
            listener.GameStopped();
        }
    }

    public void RestartGame()
    {
        StopGame();
        StartGame();
    }

    void Start()
    {
        StartGame();
    }

}