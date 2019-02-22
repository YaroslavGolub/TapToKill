using UnityEngine;

namespace TapToKill {
    public class ScoreManager : MonoBehaviour {

        #region Fields
        public int Score {
            get { return _score; }
            private set {
                _score = value;
                GameplayEventsContainer.OnScoreValueChange(_score);
            }
        }
        public int HighScore {
            get {
                return PlayerPrefs.GetInt(SAVE_HIGH_SCHORE, 0);
            }
            private set {
                PlayerPrefs.SetInt(SAVE_HIGH_SCHORE, value);
            }
        }
        private int _score = 0;
        private bool _isCounting = false;
        private bool _isHighScore = false;

        private const string SAVE_HIGH_SCHORE = "HIGH_SCORE";
        #endregion

        #region MonoBehaviour
        private void OnEnable() {
            GameEventsContainer.OnGameStart += OnGameStart;
            GameEventsContainer.OnGameRestart += OnGameRestart;
            GameEventsContainer.OnGameOver += OnGameOver;
            GameEventsContainer.OnGamePause += OnGamePaused;
            GameplayEventsContainer.AddScore += OnAddScore;
        }
        private void OnDisable() {
            GameEventsContainer.OnGameStart -= OnGameStart;
            GameEventsContainer.OnGameRestart -= OnGameRestart;
            GameEventsContainer.OnGameOver -= OnGameOver;
            GameEventsContainer.OnGamePause -= OnGamePaused;
            GameplayEventsContainer.AddScore -= OnAddScore;
        }
        #endregion
        #region Behaviour
        private void ResetScore() {
            Score = 0;
            _isCounting = true;
            _isHighScore = false;
        }
        #endregion

        #region GameplayEvents
        private void OnAddScore(int scoreToAdd) {
            if(!_isCounting)
                return;
            Score += scoreToAdd;
        }
        #endregion
        #region GameEvents
        private void OnGameStart() {
            ResetScore();
        }
        private void OnGameRestart() {
            ResetScore();
        }
        private void OnGamePaused(bool paused) {
            _isCounting = !paused;
        }
        private void OnGameOver() {
            _isCounting = false;
            if(Score > HighScore) {
                HighScore = Score;
                _isHighScore = true;
            }
            GameplayEventsContainer.SendFinalScore(Score,HighScore);
        }
        #endregion
    }
}
