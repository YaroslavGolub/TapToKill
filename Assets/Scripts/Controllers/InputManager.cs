using UnityEngine;
using Zenject;

namespace TapToKill {
    public class InputManager : MonoBehaviour {
        private IObjectSelector _selector;
        private bool _canInput = false;

        [Inject]
        public void Constructor(IObjectSelector selector) {
            _selector = selector;
        }

        #region MonoBehaviour
        private void Update() {
            if(Input.GetMouseButtonDown(0) && _canInput) {
                PerformInput();
            }
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
        private void PerformInput() {
            var selected = _selector.Select<IInteractable>(Input.mousePosition);
            if(selected != null) {
                selected.OnTap();
            }
        }
        #endregion
        #region GameEvents
        private void OnGameStart() {
            _canInput = true;
        }
        private void OnGameRestart() {
            _canInput = true;
        }
        private void OnGameOver() {
            _canInput = false;
        }
        private void OnGamePaused(bool paused) {
            _canInput = !paused;
        }
        #endregion
        #region GameplayEvents
        #endregion
    }
}
