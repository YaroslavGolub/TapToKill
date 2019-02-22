using UnityEngine;

namespace TapToKill {
    public interface ISpawnable {
        void Spawn(Transform parent, Vector3 position);
    }
}
