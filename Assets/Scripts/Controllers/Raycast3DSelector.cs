using UnityEngine;
using Zenject;

namespace TapToKill {
    public class Raycast3DSelector : IObjectSelector {
        private Camera _cam;

        [Inject]
        public void Constructor([Inject(Id = CameraType.InputCamera)] Camera cam) {
            _cam = cam;
        }
        public T Select<T>(Vector2 screenPosition) {
            var ray = _cam.ScreenPointToRay(screenPosition);
            var raycastHit = new RaycastHit();

            if(Physics.Raycast(ray, out raycastHit)) {
                var t = raycastHit.collider.GetComponent<T>();
                return t;
            }
            return default;
        }
    }
}
