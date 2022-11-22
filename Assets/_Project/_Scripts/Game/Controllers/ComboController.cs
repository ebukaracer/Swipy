using DG.Tweening;
using Racer.SoundManager;
using System.Collections;
using Racer.Utilities;
using TMPro;
using UnityEngine;

internal class ComboController : MonoBehaviour
{
    private Coroutine _initialRoutine;
    private TextMeshProUGUI _comboT;

    private string _initialText;

    [SerializeField]
    private AudioClip comboSfx;

    [Space(5),
     SerializeField]
    private TweenProperty2 tween;


    private void Awake()
    {
        _comboT = GetComponent<TextMeshProUGUI>();

        _initialText = _comboT.text;
    }

    protected virtual void BounceIn(int i)
    {
        _comboT.SetText("Combo {0}!", i);

        SoundManager.Instance.PlaySfx(comboSfx);

        _comboT.rectTransform.DOPunchScale(tween.endValue, tween.Duration, 1).SetEase(tween.EaseType)
            .OnComplete(delegate
            {
                if (_initialRoutine != null)
                    StopCoroutine(_initialRoutine);

                _initialRoutine = StartCoroutine(nameof(DelayBeforeClear));
            });
    }

    private IEnumerator DelayBeforeClear()
    {
        yield return Utility.GetWaitForSeconds(tween.Duration);

        _comboT.text = _initialText;
    }

    public void PunchScale(int count) => BounceIn(count);
}