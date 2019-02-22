using UnityEngine;

namespace TapToKill {
    public class Timer : MonoBehaviour {
        #region Fields
        private const float ROUND_TIME = 60.0f;
       [SerializeField] private float _roundTimeLeft;

        private bool _isCounting;
        #endregion
        #region MonoBehaviour
        private void Update() {
            CountTime();
        }
        private void OnEnable() {
            GameEventsContainer.OnGameStart += OnGameStart;
            GameEventsContainer.OnGameRestart += OnGameRestart;
            GameEventsContainer.OnGameOver += OnGameOver;
            GameEventsContainer.OnGamePause += OnGamePaused;
        }
        private void OnDisable() {
            GameEventsContainer.OnGameStart -= OnGameStart;
            GameEventsContainer.OnGameRestart -= OnGameRestart;
            GameEventsContainer.OnGameOver -= OnGameOver;
            GameEventsContainer.OnGamePause -= OnGamePaused;
        }
        #endregion
        #region Behaviour
        private void ResetAndStartTimer() {
            _roundTimeLeft = ROUND_TIME;
            _isCounting = true;
        }
        private void CountTime() {
            if(!_isCounting)
                return;

            _roundTimeLeft -= Time.deltaTime;
            if(_roundTimeLeft <= 0) {
                GameEventsContainer.OnGameOver();
                _roundTimeLeft = 0;
            }
            GameplayEventsContainer.OnTimerTick(_roundTimeLeft,_roundTimeLeft/ROUND_TIME);
        }
        #endregion
        #region GameEvents
        private void OnGameStart() {
            ResetAndStartTimer();
        }
        private void OnGameRestart() {
            ResetAndStartTimer();
        }
        private void OnGameOver() {
            _isCounting = false;
            print("TIMER::OnGameOver");
        }
        private void OnGamePaused(bool paused) {
            _isCounting = !paused;
        }
        #endregion
        #region GameplayEvents
        #endregion
    }
}
