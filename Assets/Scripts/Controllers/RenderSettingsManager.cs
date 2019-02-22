using UnityEngine;
using Zenject;

namespace TapToKill {
    public class RenderSettingsManager : MonoBehaviour {
        [SerializeField] private SceneRenderConfig _renderConfig;

        [Inject]
        public void Constructor(SceneRenderConfig renderConfig) {
            _renderConfig = renderConfig;
        }

        private void Awake() {
            SetSceneRenderSettings();
        }

        private void SetSceneRenderSettings() {
            RenderSettings.skybox = _renderConfig.skybox;
            RenderSettings.fog = _renderConfig.fogEnabled;
            RenderSettings.fogColor = _renderConfig.fogColor;
            RenderSettings.fogStartDistance = _renderConfig.fogStart;
            RenderSettings.fogEndDistance = _renderConfig.fogEnd;
            RenderSettings.fogMode = _renderConfig.fogMode;
        }
    } 
}
