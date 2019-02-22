using UnityEngine;

namespace TapToKill {
    public abstract class EnemyPopup : MonoBehaviour, IInteractable, ISpawnable {

        #region Fields
        protected float _healthPoints;

        [SerializeField] protected float _initHealth;
        [SerializeField] protected int _pointsToAdd;
        [SerializeField] protected Vector3 _spawmOffset;

        #endregion
        #region MonoBehaviour
        private void Update() {
            OnUpdate();
        }
        private void OnEnable() {

        }
        private void OnDisable() {

        }
        #endregion
        #region Behaviour
        public void Show() {
            transform.localScale = Vector3.zero;
            _healthPoints = _initHealth;
            transform.ScaleUp();
            OnInitialize();
        }
        public void Hide() {
            transform.ScaleDown(Disable);
        }
        public void Stop() {
            transform.CheckActiveTween();
        }
        
        protected virtual void OnInitialize() { }
        protected virtual void OnUpdate() { }
        public void Disable() {
            this.gameObject.SetActive(false);
        }
        public void OnTap() {
            TakeDamage();
        }
        public void OnTap(float damage) {
            TakeDamage(damage);
        }
        protected void TakeDamage(float damage = 1) {
            _initHealth -= damage;
            if(_initHealth <= 0) {
                _initHealth = 0;
                GameplayEventsContainer.AddScore(_pointsToAdd);
                Disable();
            }
        }
        public void Spawn(Transform parent, Vector3 position) {
            this.transform.SetParent(parent);
            transform.localPosition = position+_spawmOffset;
            Show();
        }
        #endregion
    }

}