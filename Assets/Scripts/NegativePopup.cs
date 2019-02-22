using UnityEngine;

namespace TapToKill {
    public class NegativePopup : EnemyPopup {
        private float _minTimeToHide = 3.0f;
        private float _maxTimeToHide = 5.0f;
        private float _timeLeft;
        private bool _isCounting;
        protected override void OnInitialize() {
            _timeLeft = Random.Range(_minTimeToHide, _maxTimeToHide);
            _isCounting = true;
        }
        protected override void OnUpdate() {
            CountTime();
        }
        private void CountTime() {
            if(!_isCounting)
                return;

            _timeLeft -= Time.deltaTime;
            if(_timeLeft <= 0) {
                _isCounting = false;
                Hide();
            }
        }
    }
}
