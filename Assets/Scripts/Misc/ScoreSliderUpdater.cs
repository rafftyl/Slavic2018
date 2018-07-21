using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class ScoreSliderUpdater : MonoBehaviour, IGameStateReceiver
{
    public int playerNumber;
    private GameState gameState;
    private Slider slider;

	void Start ()
    {
        slider = GetComponent<Slider>();
        Assert.IsNotNull(slider, "Missing slider");
        Assert.IsTrue(playerNumber > 0 && playerNumber <= GameState.MAX_PLAYERS, "Invalid player number");
	}
	
	void Update ()
    {
        slider.value = gameState.GetPlayerScore(playerNumber) / GameState.WIN_SCORE;
	}

    public GameState GameState { set => gameState = value; }
}
