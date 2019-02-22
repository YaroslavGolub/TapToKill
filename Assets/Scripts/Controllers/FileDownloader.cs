using System.Collections;
using UnityEngine;
using TapToKill.Extensions;
using UnityEngine.Networking;

namespace TapToKill.Misc {
    public class FileDownloader : MonoBehaviour, IInitializable {
        public static FileDownloader Instance => _instance;
        private static FileDownloader _instance;

        public bool Initialized => _isInitialized;
        private bool _isInitialized = false;
        private bool _isDownloaded = false;
        private Coroutine _downloadRoutine = null;

        private System.Action<byte[], string> _onDownloadFinished = null;

        #region Behaviour
        public void Initialize() {
            if(_instance == null) {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
                _isInitialized = true;
            } else {
                Destroy(this.gameObject);
            }
        }

        public void DownloadFile(string uri, System.Action<byte[], string> onDownloadFinishedCallback) {
            _onDownloadFinished += onDownloadFinishedCallback;
            _downloadRoutine = StartCoroutine(DownloadFileRoutine(uri));
        }
        public void StopDownload() {
            if(_downloadRoutine != null) {
                StopCoroutine(_downloadRoutine);
                _downloadRoutine = null;
            }
            _onDownloadFinished = null;
        }
        private IEnumerator DownloadFileRoutine(string uri) {
            _isDownloaded = false;
            var unityWebRequest = new UnityWebRequest(uri);
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return unityWebRequest.SendWebRequest();
            if(unityWebRequest.isNetworkError || unityWebRequest.isHttpError) {
                var msg = $"Error ocurred while downloading from {uri}. \nError::/{unityWebRequest.error}";
                _onDownloadFinished?.Invoke(null, msg);
                _onDownloadFinished = null;
                Debug.LogError(msg);
            } else {
                _onDownloadFinished.SafeInvoke(unityWebRequest.downloadHandler.data, string.Empty);
                _onDownloadFinished = null;
                Debug.Log($"Download of data from {uri} finished");
                _isDownloaded = true;
            }
        }
        #endregion
    }
}
