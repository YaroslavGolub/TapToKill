using UnityEngine;
using TapToKill;

namespace TapToKill.UI {
    public abstract class Panel : MonoBehaviour {
        #region COPY_PASTE_TXT
        // Start
        #region PROPS & FIELDS
        #endregion
        #region BAHAVIOUR
        #endregion
        #region BASE_BEHAVIOUR
        #endregion
        #region GAMEPLAY_EVENTS
        #endregion
        // End
        #endregion

        #region PROPS & FIELDS
        #endregion
        #region MonoBehaviour
        private void Start() {
            Init();
        }
        private void OnEnable() {
            AssignButtons();
            AssignExtraEvents();
            GameEventsContainer.OnGameStart += OnGameStart;
            GameEventsContainer.OnGameRestart += OnGameRestart;
            GameEventsContainer.OnGameOver += OnGameOver;
            GameEventsContainer.OnGameEnd += OnGameEnd;
            GameEventsContainer.OnGamePause += OnGamePaused;
        }
        private void OnDisable() {
            CleanButtons();
            CleanExtraEvents();
            GameEventsContainer.OnGameStart -= OnGameStart;
            GameEventsContainer.OnGameRestart -= OnGameRestart;
            GameEventsContainer.OnGameOver -= OnGameOver;
            GameEventsContainer.OnGamePause -= OnGamePaused;
        }
        #endregion
        #region BEHAVIOUR
        public abstract void Init();
        public abstract void Show();
        public abstract void Hide();
        public virtual void AssignButtons() { }
        public virtual void CleanButtons() { }
        public virtual void AssignExtraEvents() { }
        public virtual void CleanExtraEvents() { }
        #endregion
        #region GAMEPLAY_EVENT
        public abstract void OnGameStart();
        public abstract void OnGameRestart();
        public virtual void OnGameOver() { }
        public virtual void OnGameEnd() { }
        public virtual void OnGamePaused(bool paused) { }
        #endregion
    } 
}
