using UnityEngine;
using DG.Tweening;

public static partial class Extension
{
    private const float SCALE_UP_VALUE = 1.1f;
    private const float SCALE_DOWN_VALUE = 0.0f;
    private const float SCALE_UP_TIME = 0.5f;
    private const float SCALE_DOWN_TIME = 0.2f;

    public static void ScaleUpY(this Transform tr) {
        tr.gameObject.SetActive(true);
        CheckActiveTween(tr, new Vector3(1, 0));
        if(tr.localScale.y < 0.5f)
            tr.DOScaleY(SCALE_UP_VALUE, SCALE_UP_TIME).
            OnComplete(() => tr.DOScaleY(1, SCALE_DOWN_TIME));
    }
    public static void ScaleUpY(this Transform tr, System.Action callback) {
        tr.gameObject.SetActive(true);
        CheckActiveTween(tr, new Vector3(1, 0));
        if(tr.localScale.y < 0.5f)
            tr.DOScaleY(SCALE_UP_VALUE, SCALE_UP_TIME).
            OnComplete(() => tr.DOScaleY(1, SCALE_DOWN_TIME)
                .OnComplete(() => {
                    callback?.Invoke();
                }));
    }
    public static void ScaleUp(this Transform tr) {
        tr.gameObject.SetActive(true);
        CheckActiveTween(tr, Vector3.zero);
        if(tr.localScale.y < 0.5f)
            tr.DOScale(SCALE_UP_VALUE, SCALE_UP_TIME).
                OnComplete(() => tr.DOScale(1, SCALE_DOWN_TIME));
    }
    public static void ScaleUp(this Transform tr, System.Action callback) {
        tr.gameObject.SetActive(true);
        CheckActiveTween(tr, Vector3.zero);
        if(tr.localScale.y < 0.5f)
            tr.DOScale(SCALE_UP_VALUE, SCALE_UP_TIME).
                OnComplete(() => tr.DOScale(1, SCALE_DOWN_TIME)
                    .OnComplete(() => { callback?.Invoke(); }));
    }
    public static void ScaleUp(this Transform tr, System.Action callback, float speedIncrement) {
        tr.gameObject.SetActive(true);
        CheckActiveTween(tr, Vector3.zero);
        if(tr.localScale.y < 0.5f)
            tr.DOScale(SCALE_UP_VALUE, SCALE_UP_TIME * speedIncrement).
                OnComplete(() => tr.DOScale(1, SCALE_DOWN_TIME * speedIncrement)
                    .OnComplete(() => { callback?.Invoke(); }));
    }
    public static void ScaleUp(this Transform tr, float duration) {
        tr.gameObject.SetActive(true);
        CheckActiveTween(tr, Vector3.zero);
        if(tr.localScale.y < 0.5f)
            tr.DOScale(1.0f, duration);
    }
    public static void ScaleUp(this Transform tr, float duration, float scaleVal) {
        tr.gameObject.SetActive(true);
        CheckActiveTween(tr, Vector3.zero);
        if(tr.localScale.y < 0.5f)
            tr.DOScale(scaleVal, duration);
    }
    public static void ScaleDownY(this Transform tr) {
        CheckActiveTween(tr, new Vector3(1, 1));
        if(tr.localScale.y > 0.5f)
            tr.DOScaleY(SCALE_UP_VALUE, SCALE_DOWN_TIME)
                .OnComplete(() => tr.DOScaleY(SCALE_DOWN_VALUE, SCALE_UP_TIME)
                    .OnComplete(() => { tr.gameObject.SetActive(false); }));
    }
    public static void ScaleDown(this Transform tr) {
        CheckActiveTween(tr);
        if(tr.localScale.y > 0.5f)
            tr.DOScale(SCALE_UP_VALUE, SCALE_DOWN_TIME)
                .OnComplete(() => tr.DOScale(SCALE_DOWN_VALUE, SCALE_UP_TIME)
                    .OnComplete(() => { tr.gameObject.SetActive(false); }));
    }
    public static void ScaleDown(this Transform tr, System.Action callback) {
        CheckActiveTween(tr);
        if(tr.localScale.y > 0.5f)
            tr.DOScale(SCALE_UP_VALUE, SCALE_DOWN_TIME)
                .OnComplete(() => tr.DOScale(SCALE_DOWN_VALUE, SCALE_UP_TIME)
                    .OnComplete(() => {
                        tr.gameObject.SetActive(false);
                        callback?.Invoke();
                    }));
    }
    public static void ScaleDown(this Transform tr, float speedIncrement) {
        CheckActiveTween(tr);
        if(tr.localScale.y > 0.5f)
            tr.DOScale(SCALE_UP_VALUE, SCALE_DOWN_TIME * speedIncrement)
                .OnComplete(() => tr.DOScale(SCALE_DOWN_VALUE, SCALE_UP_TIME * speedIncrement)
                    .OnComplete(() => { tr.gameObject.SetActive(false); }));
    }
    public static void ScaleDown(this Transform tr, float speedIncrement, System.Action callback) {
        CheckActiveTween(tr);
        if(tr.localScale.y > 0.5f)
            tr.DOScale(SCALE_UP_VALUE, SCALE_DOWN_TIME * speedIncrement)
                .OnComplete(() => tr.DOScale(SCALE_DOWN_VALUE, SCALE_UP_TIME * speedIncrement)
                    .OnComplete(() => {
                        callback?.Invoke();
                        tr.gameObject.SetActive(false);
                    }));
    }
    /// <summary>
    /// Check and kill active transform tween
    /// </summary>
    /// <param name="tr">Transform attached to the gameobject</param>
    public static void CheckActiveTween(this Transform tr) {
        if(DOTween.IsTweening(tr))
            DOTween.Kill(tr);
    }

    /// Check and kill active transform tween and sets local scale to the given value
    /// </summary>
    /// <param name="tr">Transform attached to the gameobject</param>
    /// <param name="scale">scale value</param>
    public static void CheckActiveTween(this Transform tr, Vector3 scale) {
        if(DOTween.IsTweening(tr)) {
            DOTween.Kill(tr);
            tr.localScale = scale;
        }
    }

    /// Check and kill active transform tween and sets local scale to the given value, and sets gameObject active state
    /// </summary>
    /// <param name="tr">Transform attached to the gameobject</param>
    /// <param name="scale">scale value</param>
    /// <param name="activeState">== GameObject.SetActive(bool)</param>
    public static void CheckActiveTween(this Transform tr, Vector3 scale, bool activeState) {
        if(DOTween.IsTweening(tr)) {
            DOTween.Kill(tr);
            tr.localScale = scale;
            tr.gameObject.SetActive(activeState);
        }
    }
}
