using UnityEngine;

namespace TapToKill {
    [CreateAssetMenu(fileName = "SceneRenderConfig", menuName ="Config/SceneLighting")]
    public class SceneRenderConfig : ScriptableObject {
        public Material skybox => _skyBox;
        public bool fogEnabled => _fogEnabled;
        public Color fogColor => _fogColor;
        public float fogStart => _fogStart;
        public float fogEnd => _fogEnd;
        public FogMode fogMode => _fogMode;

        [SerializeField] private Material _skyBox ;
        [SerializeField] private bool _fogEnabled = false;
        [SerializeField] private Color _fogColor = Color.white;
        [SerializeField] private float _fogStart = 0.0f;
        [SerializeField] private float _fogEnd = 0.0f;
        [SerializeField] private FogMode _fogMode = FogMode.Linear;
    } 
}
