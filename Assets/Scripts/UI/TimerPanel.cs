using UnityEngine;
using UnityEngine.UI;

namespace TapToKill.UI {
    public class TimerPanel : Panel {
        #region PROPS & FIELDS
        [Space(10), SerializeField] private Transform _body;
        [Space(10), SerializeField] private Text _txtTimeLeft;
        [Space(10), SerializeField] private Image _fillbarImg;

        [Space(10), SerializeField] private Color _colFrom;
        [SerializeField] private Color _colTo;
        #endregion
        #region BAHAVIOUR
        private void UpdateTimer(float timeLeft, float timeProgress) {
            _txtTimeLeft.text = timeLeft.ToString("00");
            _fillbarImg.color = Color.Lerp(_colTo, _colFrom, timeProgress);
            _fillbarImg.transform.localScale = new Vector3(timeProgress, 1, 1);
        }
        private void ResetTimer() {
            _txtTimeLeft.text = "00";
            _fillbarImg.color = _colFrom;
            _fillbarImg.transform.localScale = Vector3.one;
        }
        #endregion
        #region BASE_BEHAVIOUR
        public override void Init() {
            _body.localScale = Vector3.one;
            _body.gameObject.SetActive(true);
            ResetTimer();
        }
        public override void Show() {
            _body.ScaleUp();
            ResetTimer();
        }
        public override void Hide() {
            _body.ScaleDown();
            ResetTimer();
        }
        public override void AssignExtraEvents() {
            GameplayEventsContainer.OnTimerTick += UpdateTimer;
        }
        public override void CleanExtraEvents() {
            GameplayEventsContainer.OnTimerTick -= UpdateTimer;
        }
        #endregion
        #region GAMEPLAY_EVENTS
        public override void OnGameStart() {
            Show();
        }
        public override void OnGameRestart() {
            ResetTimer();
            Show();
        }
        public override void OnGameEnd() {
            ResetTimer();
        }
        public override void OnGamePaused(bool paused) {
        }
        public override void OnGameOver() {
        }
        #endregion
    }
}
