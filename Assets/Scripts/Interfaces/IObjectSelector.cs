using UnityEngine;

namespace TapToKill {
    public interface IObjectSelector {
        T Select<T>(Vector2 screenPosition);
    }
}
