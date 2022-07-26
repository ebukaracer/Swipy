using DG.Tweening;
using UnityEngine;

class GameoverUITween : SlideTween
{
    [SerializeField]
    RectTransform rectTransform;

    [SerializeField]
    Canvas canvas;


    protected override void DoBounce()
    {
        if (rectTransform != null)
            rectTransform.DOPunchScale(endValue, duration, 1, 0).SetEase(easeType);
    }

    public void SetActive()
    {
        canvas.enabled = true;

        DoBounce();
    }
}
