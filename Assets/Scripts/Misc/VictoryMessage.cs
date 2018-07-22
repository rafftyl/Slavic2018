using UnityEngine;
using UnityEngine.UI;


public class VictoryMessage : MonoBehaviour, IGameStopListener, IGameStateReceiver
{
    public Text text;
    private GameState gameState;
    public GameState GameState { set { gameState = value; gameObject.SetActive(false); } }

    public void GameStopped()
    {
        gameObject.SetActive(true);
        text.text = "Winner: Player " + gameState.WinnerNumber;
    }
}
