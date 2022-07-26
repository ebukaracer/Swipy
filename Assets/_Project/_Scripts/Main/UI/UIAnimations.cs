using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections.Generic;
using static Racer.Utilities.SingletonPattern;


[Serializable]
class MovementTween
{
    public float duration;

    public Vector2 endValue;

    public Ease easeType;
}

[Serializable]
class ScaleTween : MovementTween { }

[Serializable]
class HandTween : MovementTween
{
    public RectTransform rectTransform;
}

[Serializable]
class HeadingTextTween : HandTween { }

class UIAnimations : Singleton<UIAnimations>
{
    [SerializeField]
    MovementTween movementTween;

    [SerializeField]
    ScaleTween scaleTween;

    [SerializeField]
    HandTween handTween;

    [SerializeField]
    HeadingTextTween headingTextTween;

    [Space(10)]

    [SerializeField]
    GameObject mainPanel;

    Canvas canvas;

    CanvasGroup canvasGroup;

    readonly Stack<Vector2> panelInitialPos = new Stack<Vector2>();

    readonly Stack<RectTransform> panels = new Stack<RectTransform>();

    TweenerCore<Vector2, Vector2, VectorOptions> handAnim;

    TweenerCore<Vector3, Vector3, VectorOptions> headingTextAnim;



    protected override void Awake()
    {
        base.Awake();

        panels.Push(mainPanel.GetComponent<RectTransform>());

        // HandAnim();

        // HeadingAnim();
    }

    #region Slidein_Slideout
    // Action btn
    public void SlideIn(RectTransform rectTransform)
    {
        ComponentCheck(rectTransform, true);

        panelInitialPos.Push(rectTransform.anchoredPosition);

        rectTransform.DOAnchorPos(movementTween.endValue, movementTween.duration).SetEase(movementTween.easeType)
            .OnComplete(() =>
            {
                var rmv = panels.Pop();

                ComponentCheck(rmv, false);

                panels.Push(rectTransform);
            });
    }


    // Cancel btn
    public void SlideOut(RectTransform rectTransform)
    {
        ComponentCheck(rectTransform, true);

        var rmv = panels.Pop();

        rmv.DOAnchorPos(panelInitialPos.Pop(), movementTween.duration).SetEase(movementTween.easeType)
            .OnComplete(() =>
            {
                ComponentCheck(rmv, false);

                panels.Push(rectTransform);
            });
    }
    #endregion

    private void ComponentCheck(RectTransform rectTransform, bool check)
    {
        canvasGroup = rectTransform.GetComponent<CanvasGroup>();

        canvas = rectTransform.GetComponent<Canvas>();

        if (canvasGroup != null)
        {
            canvasGroup.alpha = check == false ? 0 : 1;

            canvasGroup.interactable = check;

            canvasGroup.blocksRaycasts = check;
        }
        else if (canvas != null)
            canvas.enabled = check;
        else
            rectTransform.gameObject.SetActive(check);
    }

    #region Popin_Popout

    public void PopIn(RectTransform rectTransform)
    {
        rectTransform.DOScale(Vector2.one, scaleTween.duration).SetEase(scaleTween.easeType);
    }

    public void PopOut(RectTransform rectTransform)
    {
        rectTransform.DOScale(Vector2.zero, scaleTween.duration).SetEase(scaleTween.easeType)
            .OnComplete(() =>
            rectTransform.gameObject.SetActive(false));
    }
    #endregion

    void HandAnim()
    {
        handAnim = handTween.rectTransform.DOAnchorPos(handTween.endValue, handTween.duration).SetLoops(-1, LoopType.Yoyo).SetEase(handTween.easeType);
    }

    void HeadingAnim()
    {
        headingTextAnim = headingTextTween.rectTransform.DOScale(headingTextTween.endValue, headingTextTween.duration).SetLoops(-1, LoopType.Yoyo).SetEase(headingTextTween.easeType);
    }

    private void OnDestroy()
    {
        // handAnim?.Kill(false);

        // headingTextAnim?.Kill(false);
    }
}
