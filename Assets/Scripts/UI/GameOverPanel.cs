using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TapToKill.UI {
    public class GameOverPanel : Panel {
        #region Fields
        [Space(10), SerializeField] private Transform _body;

        [Space(5), SerializeField] private Button _btnRestart;

        [Space(10), SerializeField] private Text _txtGameOver;

        [Header("Score fields"), SerializeField] private Text _txtScoreView;
        [SerializeField] private Text _txtBestScoreView;

        [Header("Score holders"), SerializeField] private Transform _scoreHolder;
        [SerializeField] private Transform _bestScoreHolder;

        #region score fields
        private int _tempScore;
        private int _finalBestScore;
        private int _finalScore;
        #endregion

        #region const fields
        private const float SHOW_DELAY = 0.375f;
        private const float SCORE_UPDATE_TIME = 0.375f;
        private const string TXT_GAME_OVER = "GAME OVER";
        #endregion
        #endregion
        #region BEHAVIOUR
        private void UpdateScoreView(int score, int bestScore) {
            _txtScoreView.text = score.ToString();
            _txtBestScoreView.text = bestScore.ToString();
        }
        private void UpdateScoreData(int score, int bestScore) {
            _tempScore = 0;
            _finalScore = score;
            _finalBestScore = bestScore;
            UpdateScoreView(_tempScore, _finalBestScore);
        }
        private void OnPlayBtnClick() {
            _btnRestart.interactable = false;
            GameEventsContainer.OnGameEnd();
        }
        #region COUNT_SCORE
        private void CountScore() {
            DOTween.To(() => _tempScore, x => _tempScore = x, _finalScore, SCORE_UPDATE_TIME)
                .SetEase(Ease.OutCubic)
                .OnStart(OnCountStart)
                .OnUpdate(OnCountUpdate)
                .OnComplete(OnCountComplete);
        }
        private void OnCountStart() {
        }
        private void OnCountUpdate() {
            UpdateScoreView(_tempScore, _finalBestScore);
        }
        private void OnCountComplete() {
            UpdateScoreView(_finalScore, _finalBestScore);
            print($"Final score = /{_finalScore}, best score = /{_finalBestScore}");
            _btnRestart.interactable = true;
        }
        #endregion
        #endregion
        #region BASE_BEHAVIOUR
        public override void Init() {
            UpdateScoreView(0, 0);
            _btnRestart.interactable = false;
            _body.localScale = Vector3.zero;
            _body.gameObject.SetActive(false);
        }
        public override void AssignButtons() {
            _btnRestart.onClick.AddListener(OnPlayBtnClick);
        }
        public override void CleanButtons() {
            _btnRestart.onClick.RemoveAllListeners();
        }
        public override void AssignExtraEvents() {
            GameplayEventsContainer.SendFinalScore += UpdateScoreData;
        }
        public override void CleanExtraEvents() {
            GameplayEventsContainer.SendFinalScore -= UpdateScoreData;
        }
        public override void Show() {
            if(SHOW_DELAY > 0) {
                float t = 0;
                DOTween.To(() => t, x => t = x, SHOW_DELAY, SHOW_DELAY).OnComplete(() => {
                    _body.ScaleUp(CountScore, 0.5f);
                });
            }
        }
        public override void Hide() {
            _btnRestart.interactable = false;
            _body.ScaleDown(0.5f);
        }
        #endregion
        #region GAMEPLAY_EVENTS
        public override void OnGameOver() {
            Show();
        }
        public override void OnGameRestart() {
        }
        public override void OnGameEnd() {
            Hide();
        }
        public override void OnGameStart() {

        }
        private void OnLevelFinished() {
            Show();
        }
        #endregion
    }
}
