using System.Collections.Generic;
using UnityEngine;

namespace TapToKill {
    public class ObjectPooler : MonoBehaviour, IObjectPooler {

        #region Fields
        [SerializeField] private GameObject _objPrefab;
        [SerializeField] private int _pooledAmn = 10;

        [SerializeField] private List<GameObject> _pooledObjects = new List<GameObject>();

        public int activeObject {
            get {
                var activeObjects = 0;
                for(int i = 0; i < _pooledObjects.Count; i++) {
                    if(_pooledObjects[i].activeSelf)
                        activeObjects++;
                }
                return activeObjects;
            }
        }
        #endregion

        #region MonoBehavior
        private void Awake() {
            for(int i = _pooledObjects.Count; i < _pooledAmn; i++) {
                GetNewObject();
            }
        }
        private void OnEnable() {
            GameEventsContainer.OnGameOver += DisableAll;
            GameEventsContainer.OnGameRestart += DisableAll;
        }
        private void OnDisable() {
            GameEventsContainer.OnGameOver -= DisableAll;
            GameEventsContainer.OnGameRestart -= DisableAll;
        }
        #endregion

        #region Behaviour
        private GameObject GetNewObject(bool activeState = false) {
            GameObject obj = Instantiate(_objPrefab) as GameObject;
            obj.transform.SetParent(this.transform);
            obj.SetActive(activeState);
            _pooledObjects.Add(obj);
            return obj;
        }
        public void DisableAll() {
            for(int i = 0; i < _pooledObjects.Count; i++) {
                _pooledObjects[i].SetActive(false);
            }
        }
        public T GetPooledObject<T>() {
            for(int i = 0; i < _pooledObjects.Count; i++) {
                if(!_pooledObjects[i].activeSelf) {
                    _pooledObjects[i].SetActive(true);
                    return _pooledObjects[i].GetComponent<T>();
                }
            }
            return GetNewObject(true).GetComponent<T>();
        }
        #endregion
    }
}
