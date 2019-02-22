using System;

namespace TapToKill {
    public static class GameplayEventsContainer {
        public static Action<int> AddScore = delegate { };
        public static Action<int> OnScoreValueChange = delegate { };
        /// <param name="score">final score</param>
        /// <param name="best scpre">final best score</param>
        public static Action<int,int> SendFinalScore = delegate { };
        /// <param name="timeLeft">time left</param>
        /// <param name="timeProgress">time progress from 1 to 0</param>
        public static Action<float,float> OnTimerTick = delegate { };
    } 
}
