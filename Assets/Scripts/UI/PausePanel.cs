using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TapToKill.UI {
    public class PausePanel : Panel {
        #region PROPS & FIELDS
        [Space(10), SerializeField] private Transform _body;
        [Space(10), SerializeField] private Button _btnPause;
        [SerializeField] private Button _btnResume;
        [SerializeField] private Button _btnRestart;
        #endregion

        #region BAHAVIOUR
        private void OnPauseBtnCLick() {
            print("OnPauseBtnCLick");
            GameEventsContainer.OnGamePause(true);
        }
        private void OnResumeBtnClick() {
            GameEventsContainer.OnGamePause(false);
            Hide();
        }
        private void OnRestartBtnClick() {
            GameEventsContainer.OnGamePause(false);
            Hide(OnHideComplete);
        }
        #endregion
        #region BASE_BEHAVIOUR
        public override void Init() {
            _body.localScale = Vector3.zero;
            _body.gameObject.SetActive(false);
            _btnPause.interactable = false;
            _btnResume.interactable = false;
            _btnRestart.interactable = false;
        }
        public override void Show() {
            _body.transform.localScale = Vector3.one;
            _body.gameObject.SetActive(true);
            _btnPause.interactable = false;
            _btnResume.interactable = true;
            _btnRestart.interactable = true;
            Time.timeScale = 0;
        }
        public override void Hide() {
            _btnResume.interactable = false;
            _btnRestart.interactable = false;
            _body.ScaleDown(() => { _btnPause.interactable = true; });
        }
        public void Hide(System.Action callback) {
            _btnResume.interactable = false;
            _btnRestart.interactable = false;
            _body.ScaleDown(OnHideComplete);
        }
        private void OnHideComplete() {
            GameEventsContainer.OnGameRestart();
        }
        #endregion
        #region GAMEPLAY_EVENTS
        public override void AssignButtons() {
            _btnPause.onClick.AddListener(OnPauseBtnCLick);
            _btnRestart.onClick.AddListener(OnRestartBtnClick);
            _btnResume.onClick.AddListener(OnResumeBtnClick);
        }
        public override void CleanButtons() {
            _btnPause.onClick.RemoveAllListeners();
            _btnRestart.onClick.RemoveAllListeners();
            _btnResume.onClick.RemoveAllListeners();
        }
        public override void OnGameStart() {
            _btnPause.interactable = true;
        }
        public override void OnGameRestart() {
            _btnPause.interactable = true;
        }
        public override void OnGamePaused(bool paused) {
            if(paused) {
                Show();
            } else {
                Time.timeScale = 1.0f;
                //DOTween.To(() => Time.timeScale, x => Time.timeScale = x,1, 0.3f);
            }
        }
        #endregion
    }
}
