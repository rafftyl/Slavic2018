using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public const int MAX_PLAYERS = 2;
    private const float WIN_SCORE = 10.0f;
    private int winnerNumber = -1;
    public int WinnerNumber { get => winnerNumber; }
    Dictionary<int, float> currentPlayerScore;

    [SerializeField]
    RhythmManager rhythmManager;

    [SerializeField]
    AudioManager audioManager;

    List<IGameStartListener> gameStartListeners = new List<IGameStartListener>();
    List<IGameStopListener> gameStopListeners = new List<IGameStopListener>();
    List<IGamePauseListener> gamePauseListeners = new List<IGamePauseListener>();

    void Awake()
    {
        currentPlayerScore = new Dictionary<int, float>();
        for(int i = 1; i <= MAX_PLAYERS; ++i)
        {
            currentPlayerScore.Add(i, 0.0f);
        }

        Assert.IsNotNull(rhythmManager);
        Assert.IsNotNull(audioManager);

        for(int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; ++sceneIndex)
        {
            List<GameObject> rootObjects = new List<GameObject>();
            Scene scene = SceneManager.GetSceneAt(sceneIndex);
            scene.GetRootGameObjects(rootObjects);
            InjectDependencies(rootObjects);          
        }        
    }

    private void InjectDependencies(List<GameObject> objects)
    {
        //TODO: generalize dependency injection
        foreach (var obj in objects)
        {
            if (obj.activeSelf)
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

    public void AddScoreForPlayer(int playerNumber, float ammount)
    {
        Assert.IsTrue(playerNumber > 0 && playerNumber <= MAX_PLAYERS, "Invalid player number");
        Assert.IsTrue(ammount > 0.0f, "Invalid ammount");
        if(winnerNumber <= -1)
        {
            currentPlayerScore[playerNumber] += ammount;
            if (currentPlayerScore[playerNumber] >= WIN_SCORE)
            {
                winnerNumber = playerNumber;
            }
        }
    }

    public float GetPlayerScore(int playerNumber)
    {
        Assert.IsTrue(playerNumber > 0 && playerNumber <= MAX_PLAYERS, "Invalid player number");
        return currentPlayerScore[playerNumber];
    }
}