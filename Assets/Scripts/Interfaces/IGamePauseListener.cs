
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IGamePauseListener : GameState {

    public void GamePaused();

    public void GameResumed();

}