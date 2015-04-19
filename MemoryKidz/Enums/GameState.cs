using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryKidz
{
    /// <summary>
    /// Enum which represents all possible Gamestates of the Game
    /// </summary>
    public enum GameState
    {
        None,
        Startscreen,
        MainMenuSession,
        MainMenuNoSession,
        Running,
        OptionMenu,
        Highscore,
        Endgame,
        ChooseDifficulty,
        ImageDetailView
    }
}
