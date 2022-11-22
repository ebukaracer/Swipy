using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using Racer.SoundManager;
using Racer.Utilities;

internal class RateTween : MonoBehaviour
{
    [SerializeField] private Sprite rateSpr;
    [SerializeField] private Image[] imagesUI;

    [Space(5),
    SerializeField]
    private AudioClip rateSfx;

    [Space(5),
     SerializeField]
    private TweenProperty2 tween;

    private IEnumerator DisplayStar(int count)
    {
        yield return Utility.GetWaitForSeconds(.5f);

        for (int i = 0; i < count; i++)
        {
            yield return Utility.GetWaitForSeconds(.5f);

            BounceIn(i);
        }
    }

    protected virtual void BounceIn(int i)
    {
        imagesUI[i].sprite = rateSpr;
        imagesUI[i].transform.DOPunchScale(tween.endValue, tween.Duration, 1).SetEase(tween.EaseType);

        SoundManager.Instance.PlaySfx(rateSfx);
    }

    public void ShowStarAnimation(int count)
    {
        if (count <= 0) return;

        StartCoroutine(DisplayStar(count));
    }
}
