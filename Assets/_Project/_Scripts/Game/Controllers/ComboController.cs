using DG.Tweening;
using Racer.SoundManager;
using System.Collections;
using TMPro;
using UnityEngine;

class ComboController : SlideTween
{
    IEnumerator initialRoutine;

    TextMeshProUGUI comboT;

    string originalStr;

    [Space(10), SerializeField]
    AudioClip comboSfx;

    private void Awake()
    {
        comboT = GetComponent<TextMeshProUGUI>();

        originalStr = comboT.text;
    }

    protected override void BounceIn(int i)
    {
        comboT.SetText("Combo {0}!", i);

        SoundManager.Instance.PlaySfx(comboSfx);

        comboT.rectTransform.DOPunchScale(endValue, duration, 1).SetEase(easeType)
            .OnComplete(delegate
            {
                if (initialRoutine != null)
                    StopCoroutine(initialRoutine);

                initialRoutine = DelayBeforeClear();

                StartCoroutine(initialRoutine);
            });
    }

    IEnumerator DelayBeforeClear()
    {
        yield return Utilities.GetWaitForSeconds(duration * duration);

        comboT.text = originalStr;
    }

    public void PunchScale(int count) => BounceIn(count);
}