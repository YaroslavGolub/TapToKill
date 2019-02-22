using System;
using UnityEngine;
using System.IO;
using TapToKill.Scriptable;

namespace TapToKill.Misc {
    public class ConfigManager : MonoBehaviour, IInitializable {
        public bool Initialized => _isInitialized;
        private bool _isInitialized = false;

        [SerializeField] private AppConfig _appConfig;
        private ConfigData _remoteConfig = null;

        private string _remoteConfigAdress = string.Empty;
        #region Behaviour
        public void Initialize() {
            PrepareRemoteConfig();
            _isInitialized = true; ;
        }

        private void PrepareRemoteConfig() {
            var fileExist = File.Exists(GetFilePath());
            var configData = string.Empty;
            if(fileExist) {
                print("Cached config file exists. Going to open it.");

                try {
                    configData = File.ReadAllText(GetFilePath());
                    _remoteConfig = JsonUtility.FromJson<ConfigData>(configData);
                } catch(Exception e) {
                    Debug.LogWarning(e.ToString());
                }
            } else {
                Debug.Log("Cached remote config file doesn't exist");
                _remoteConfig = new ConfigData();
            }

            if(string.IsNullOrEmpty(_appConfig.RemoteConfigAddress)) {
                var data = string.Empty;
                data = JsonUtility.ToJson(_remoteConfig);

                try {
                    File.WriteAllText(GetFilePath(), data);
                } catch(Exception e) {
                    Debug.LogWarning(e.ToString());
                }
            }
        }
        private void OnDownloadFinishedCallback(byte[] downloadedData, string erroMsg) {
            if(!string.IsNullOrEmpty(erroMsg)) {
                return;
            }
            var jsonData = System.Text.Encoding.Default.GetString(downloadedData);

            try {
                if(jsonData.Length > 0) {
                    File.WriteAllText(GetFilePath(), jsonData);
                    _remoteConfig = JsonUtility.FromJson<ConfigData>(jsonData);
                    GameEventsContainer.OnGameConfigUpdate(_remoteConfig);
                }
            } catch(Exception e) {
                Debug.LogWarning(e.ToString());
            }
        }
        private string GetFilePath() {
            return Path.Combine(Application.persistentDataPath, _appConfig.ConfigFileName);
        }
        private void DownloadRemoteConfig() {
            if(!string.IsNullOrEmpty(_appConfig.RemoteConfigAddress)) {
                FileDownloader.Instance.DownloadFile(_appConfig.RemoteConfigAddress, OnDownloadFinishedCallback);
            } else {
                print("web address is empty");
            }
        }
        #endregion
        #region MonoBehaviour
        private void OnEnable() {
            GameEventsContainer.OnGameLoaded += OnGameLoaded;
        }
        private void OnDisable() {
            GameEventsContainer.OnGameLoaded -= OnGameLoaded;
        }
        #endregion
        #region GameEvents
        private void OnGameLoaded() {
            DownloadRemoteConfig();
        }
        #endregion
    }
}
