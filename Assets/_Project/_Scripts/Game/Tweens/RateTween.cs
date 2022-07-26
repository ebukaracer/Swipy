using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using Racer.SoundManager;

public class RateTween : SlideTween
{
    [SerializeField]
    Sprite rateImage;

    [SerializeField]
    Image[] imagesUI;

    [SerializeField]
    AudioClip rateSfx;


    IEnumerator ShowStar(int count)
    {
        yield return Utilities.GetWaitForSeconds(.5f);


        for (int i = 0; i < count; i++)
        {
            yield return Utilities.GetWaitForSeconds(.5f);

            BounceIn(i);
        }
    }

    protected override void BounceIn(int i)
    {
        imagesUI[i].sprite = rateImage;

        imagesUI[i].transform.DOPunchScale(endValue, duration, 1).SetEase(easeType);

        SoundManager.Instance.PlaySfx(rateSfx);
    }

    public void ShowStarAnimation(int count)
    {
        if (count <= 0) return;

        StartCoroutine(ShowStar(count));
    }
}
