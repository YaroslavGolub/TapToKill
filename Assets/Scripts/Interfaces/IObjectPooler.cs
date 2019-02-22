using UnityEngine;

namespace TapToKill {
    public interface IObjectPooler {
        int activeObject { get; }
        T GetPooledObject<T>();

    }
}