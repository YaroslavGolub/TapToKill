using UnityEngine;
using UnityEngine.UI;

namespace TapToKill.UI {
    public class ScorePanel : Panel {
        #region PROPS & FIELDS
        [Space(10), SerializeField] private Transform _body;
        [Space(10), SerializeField] private Text _txtScore;
        #endregion
        #region BAHAVIOUR
        private void UpdateScoreView(int score) {
            _txtScore.text = score.ToString();
        }
        private void ResetScore() {
            _txtScore.text = "0";
        }
        #endregion
        #region BASE_BEHAVIOUR
        public override void Init() {
            _body.localScale = Vector3.one;
            _body.gameObject.SetActive(true);
            UpdateScoreView(0);
        }
        public override void Show() {
            _body.ScaleUp();
        }
        public override void Hide() {
            _body.ScaleDown();
        }
        public override void AssignExtraEvents() {
            GameplayEventsContainer.OnScoreValueChange += UpdateScoreView;
        }
        public override void CleanExtraEvents() {
            GameplayEventsContainer.OnScoreValueChange -= UpdateScoreView;
        }
        #endregion
        #region GAMEPLAY_EVENTS
        public override void OnGameStart() {
            Show();
        }
        public override void OnGameEnd() {
            ResetScore();
            Show();
        }
        public override void OnGameRestart() {
            ResetScore();
            Show();
        }
        public override void OnGamePaused(bool paused) {
        }
        public override void OnGameOver() {
        }
        #endregion
    }
}
