using UnityEngine;
using UnityEngine.UI;

namespace TapToKill.UI {
    public class StartPanel : Panel {
        #region PROPS & FIELDS
        [Space(10), SerializeField] private Transform _body;
        [Space(10), SerializeField] private Button _btnStart;
        #endregion
        #region BAHAVIOUR
        private void OnStartBtnClick() {
            GameEventsContainer.OnGameStart();
        }
        #endregion
        #region BASE_BEHAVIOUR
        public override void Init() {
            _body.localScale = Vector3.zero;
            _body.gameObject.SetActive(false);
            _btnStart.interactable = false;
        }
        public override void Show() {
            _body.ScaleUp(() => {
                _btnStart.interactable = true;
            });
        }
        public override void Hide() {
            _btnStart.interactable = false;
            _body.ScaleDown();
        }
        public override void AssignButtons() {
            _btnStart.onClick.AddListener(OnStartBtnClick);
        }
        public override void CleanButtons() {
            _btnStart.onClick.RemoveAllListeners();
        }
        public override void AssignExtraEvents() {
            GameEventsContainer.OnGameLoaded += Show;
        }
        public override void CleanExtraEvents() {
            GameEventsContainer.OnGameLoaded -= Show;
        }
        #endregion
        #region GAMEPLAY_EVENTS
        public override void OnGameStart() {
            Hide();
        }
        public override void OnGameRestart() {
           
        }
        public override void OnGameEnd() {
            Show();
        }
        public override void OnGamePaused(bool paused) {
        }
        public override void OnGameOver() {
        }
        #endregion
    }
}
