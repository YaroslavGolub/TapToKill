using UnityEngine;

namespace TapToKill.Scriptable {
    [CreateAssetMenu(fileName = "Assets/Scriptable/Settings/ApplicationConfig", menuName = "Config/ApplicationConfig")]
    public class AppConfig : ScriptableObject {
        public string RemoteConfigAddress => _remoteConfigAddress;
        [SerializeField] private string _remoteConfigAddress = "";
        public string ConfigFileName => _configFileName;
        [SerializeField] private string _configFileName = "";

    }
}
