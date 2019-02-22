using UnityEngine;

namespace TapToKill.UI {
    public class InitPanel : Panel {
        #region PROPS & FIELDS
        [Space(10), SerializeField] private Transform _body;
        #endregion
        #region BAHAVIOUR
        #endregion
        #region BASE_BEHAVIOUR
        public override void Init() {
            _body.localScale = Vector3.one;
            _body.gameObject.SetActive(true);
        }
        public override void Show() {
        }
        public override void Hide() {
            _body.gameObject.SetActive(false);
        }
        public override void AssignExtraEvents() {
            GameEventsContainer.OnGameLoaded += Hide;
        }
        public override void CleanExtraEvents() {
            GameEventsContainer.OnGameLoaded -= Hide;
        }
        #endregion
        #region GAMEPLAY_EVENTS
        public override void OnGameStart() {
        }
        public override void OnGameRestart() {
        }
        public override void OnGamePaused(bool paused) {
        }
        public override void OnGameOver() {
        }
        #endregion
    }
}
