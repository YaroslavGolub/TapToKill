using UnityEngine;

namespace TapToKill {
    [System.Serializable]
    public struct Boundary {
        public float xMin => _xMinPos;
        public float xMax => _xMaxPos;
        public float y => _yPos;
        public float zMin => _zMinPos;
        public float zMax => _zMaxPos;

        [SerializeField] private float _xMinPos;
        [SerializeField] private float _xMaxPos;
        [SerializeField] private float _yPos;
        [SerializeField] private float _zMinPos;
        [SerializeField] private float _zMaxPos;
    }
}
