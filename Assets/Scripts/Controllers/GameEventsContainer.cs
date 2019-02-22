using System;

namespace TapToKill {
    public static class GameEventsContainer {
        public static bool gameOver = false;
        public static bool gameStarted = false;

        public static Action OnGameLoaded = delegate { };
        public static Action OnGameOver = delegate { gameOver = true; };
        public static Action OnGameEnd = delegate { gameOver = false; gameStarted = false; };
        public static Action OnGameRestart = delegate { gameOver = false; gameStarted = true; };
        public static Action OnGameStart = delegate { gameStarted = true; };
        public static Action<bool> OnGamePause = delegate { };

        public static Action<ConfigData> OnGameConfigUpdate = delegate { };
    }
}
