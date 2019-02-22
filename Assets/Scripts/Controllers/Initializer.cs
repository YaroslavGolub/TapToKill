using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace TapToKill {
    public class Initializer : MonoBehaviour {
        [SerializeField] public List<GameObject> _initObjects = new List<GameObject>();
        private List<IInitializable> _initializableList = new List<IInitializable>();

        [SerializeField] private bool _loadScene = false;
        private bool _isInitialized = false;
        private bool _receivedCallback = false;

        #region MonoBehaviour
        private void Awake() {
            LoadGUIScene();
            FillInitList();
            StartCoroutine(WaitForInitialization(OnInitializationFinishedCallback));
            StartCoroutine(ServerCallbackWaitImitation());
        }
        private void OnEnable() {
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }
        private void OnDisable() {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }

        #endregion
        #region Behaviour
        private void FillInitList() {
            for(int i = 0; i < _initObjects.Count; i++) {
                var initializable = _initObjects[i].GetComponent<IInitializable>();
                if(initializable != null)
                    _initializableList.Add(initializable);
            }
        }
        private void OnInitializationFinishedCallback() {
            if(_loadScene) {
                SceneManager.LoadScene("game_scene", LoadSceneMode.Additive);
            }
        }
        private IEnumerator WaitForInitialization(System.Action onInitializationFinishedCallback) {
            foreach(var item in _initializableList) {
                item.Initialize();
            }

            while(!_isInitialized) {
                _isInitialized = CheckInitStatus() && _receivedCallback;
                yield return null;
            }

            print("All managers are initialized");

            onInitializationFinishedCallback?.Invoke();
        }
        private IEnumerator ServerCallbackWaitImitation() {
            yield return new WaitForSeconds(1.5f);
            _receivedCallback = true;
        }
        private bool CheckInitStatus() {
            for(int i = 0; i < _initializableList.Count; i++) {
                if(!_initializableList[i].Initialized)
                    return false;
            }
            return true;
        }
        private void LoadGUIScene() {
            SceneManager.LoadScene("gui_scene", LoadSceneMode.Additive);
        }
        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode loadSceneMode) {
            if(scene.name == "game_scene") {
                GameEventsContainer.OnGameLoaded();
            }
        }
        #endregion
    }
}