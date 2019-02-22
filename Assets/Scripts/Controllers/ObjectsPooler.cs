using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TapToKill {
    public class ObjectsPooler : MonoBehaviour, IObjectPooler {
        public GameObject[] poolersGO;
        private IObjectPooler[] _poolers;

        public int activeObject {
            get {
                var sum = 0;
                for(int i = 0; i < _poolers.Length; i++) {
                    sum += _poolers[i].activeObject;
                }
                return sum;
            }
        }
        private void Awake() {
            _poolers = new IObjectPooler[poolersGO.Length];
            for(int i = 0; i < poolersGO.Length; i++) {
                _poolers[i] = poolersGO[i].GetComponent<IObjectPooler>();
            }
        }

        public T GetPooledObject<T>() {
            return _poolers[GetRandomPoolerIndex()].GetPooledObject<T>();
        }
        private int GetRandomPoolerIndex() {
            return Random.Range(0, _poolers.Length);
        }
        
    }
}
