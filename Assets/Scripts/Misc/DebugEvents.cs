using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TapToKill {
    public class DebugEvents : MonoBehaviour {
        public void StartGame() {
            GameEventsContainer.OnGameStart();
        }
        public void RestartGame() {
            GameEventsContainer.OnGameRestart();
        }
        public void GameOver() {
            GameEventsContainer.OnGameOver();
        }
    } 
}
