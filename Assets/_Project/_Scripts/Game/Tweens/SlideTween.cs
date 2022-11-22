using DG.Tweening;
using System.Collections;
using UnityEngine;

internal class SlideTween : MonoBehaviour
{
    private float _elapsedTime;

    [SerializeField,
     Tooltip("Time(s) to return to original position when mouse button is up")]
    private float duration;

    [Space(5),
     SerializeField,
     Tooltip("Animation properties")]
    private TweenProperty2 tween;

    private void OnEnable()
    {
        DoBounceIn();
    }

    /// <summary>
    /// Smoothly moves to target position without snapping.
    /// </summary>
    public IEnumerator MovePosition(Vector3 initialPos, Vector3 finalPos)
    {
        _elapsedTime = 0;

        while (_elapsedTime < duration)
        {
            var newPos = Vector3.Lerp(initialPos, finalPos, _elapsedTime / duration);

            transform.position = newPos;

            _elapsedTime += Time.deltaTime;

            yield return 0;
        }

        transform.position = finalPos;
    }

    /// <summary>
    /// Bounces in when instantiated.
    /// </summary>
    private void DoBounceIn()
    {
        transform.DOPunchScale(tween.endValue, duration, 0).SetEase(tween.EaseType);
    }
}