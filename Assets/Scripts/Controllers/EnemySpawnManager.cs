using UnityEngine;
using Zenject;
using System.Collections;
using System.Collections.Generic;

namespace TapToKill {
    public class EnemySpawnManager : MonoBehaviour {
        #region Fields
        [SerializeField] private Boundary _spawnField;
        [SerializeField] private float _enemyRadius;
        [SerializeField] private LayerMask _mask;

        [SerializeField] private int _initiEnemiesOnScreen = 3;
        [Space(10), SerializeField] private int _maxEnemyOnScreen = 15;
        [SerializeField] private float _spawnDelay = 1.0f;
        [SerializeField, Range(0, 1)] private float _negativeEnemySpawnChance = 0.25f;

        private Transform _gameFieldTr;
        private Coroutine _spawnEnemyRotine;
        private bool _canSpawn;
        private bool _isPaused;

        private IObjectPooler _positivePooler;
        private IObjectPooler _negativePooler;

        #endregion

        #region Constructor
        [Inject]
        public void Constructor([Inject(Id = PopupType.NegativePopup)]IObjectPooler negativePooler, [Inject(Id = PopupType.PositivePopup)]IObjectPooler positivePooler, Transform gameField) {
            _negativePooler = negativePooler;
            _positivePooler = positivePooler;
            _gameFieldTr = gameField;
        }
        #endregion
        #region MonoBehaviour
        private void Update() {
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
        private void SpawnEnemy() {
            var spawnPos = GetRandonSpawnPosition();
            var canSpawn = CanSpawn(spawnPos);
            var maxIterations = 100;
            while(!canSpawn) {
                spawnPos = GetRandonSpawnPosition();
                canSpawn = CanSpawn(spawnPos);
                maxIterations--;
                if(maxIterations <= 0) {
                    print("SPAWN LOOP:: To many attempts");
                    return;
                }
            }
            var enemy = (Random.value > _negativeEnemySpawnChance) ? _positivePooler.GetPooledObject<ISpawnable>() : _negativePooler.GetPooledObject<ISpawnable>();
            enemy.Spawn(_gameFieldTr, spawnPos);
        }
        private Vector3 GetRandonSpawnPosition() {
            return new Vector3(
                Random.Range(_spawnField.xMin, _spawnField.xMax),
                _spawnField.y,
                Random.Range(_spawnField.zMin, _spawnField.zMax));
        }
        private bool CanSpawn(Vector3 position) {
            var colliders = Physics.OverlapSphere(position, _enemyRadius, _mask);
            if(colliders == null || colliders.Length < 1) {
                return true;
            } else {
                return false;
            }
        }
        private void StartSpawnRoutine() {
            StopSpawnRoutine();
            _canSpawn = true;
            _spawnEnemyRotine = StartCoroutine(SpawnRoutine());
        }
        private void StopSpawnRoutine() {
            _canSpawn = false;
            if(_spawnEnemyRotine != null) {
                StopCoroutine(_spawnEnemyRotine);
                _spawnEnemyRotine = null;
            }
        }
        private IEnumerator SpawnRoutine() {
            while(_canSpawn) {
                yield return new WaitForSeconds(GetSpawnDelay());
                if(_isPaused)
                    yield return null;
                if(_positivePooler.activeObject + _negativePooler.activeObject < _maxEnemyOnScreen) {
                    SpawnEnemy();
                }
            }
        }
        private float GetSpawnDelay() {
            if(_positivePooler.activeObject + _negativePooler.activeObject < _maxEnemyOnScreen) {
                var delay = Mathf.InverseLerp(4, _maxEnemyOnScreen, _positivePooler.activeObject + _negativePooler.activeObject);
                return delay;
            }
            return _spawnDelay;
        }
        #endregion
        #region GameEvents
        private void OnGameStart() {
            for(int i = 0; i < _initiEnemiesOnScreen; i++) {
                SpawnEnemy();
            }
            StartSpawnRoutine();
        }
        private void OnGameRestart() {
            StopSpawnRoutine();
            OnGameStart();
        }
        private void OnGameOver() {
            StopSpawnRoutine();
        }
        private void OnGamePaused(bool paused) {
            _isPaused = paused;
        }
        #endregion
        #region GameplayEvents
        #endregion
    }
}
