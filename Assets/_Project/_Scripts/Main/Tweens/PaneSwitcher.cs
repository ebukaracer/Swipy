using System.Collections.Generic;
using DG.Tweening;
using Racer.Utilities;
using UnityEngine;
using UnityEngine.UI;

internal class PaneSwitcher : MonoBehaviour
{
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private ScrollRect _scrollRect;

    private readonly Stack<Vector2> _panelInitialPos = new Stack<Vector2>();
    private readonly Stack<RectTransform> _panels = new Stack<RectTransform>();

    [SerializeField] private GameObject mainPanel;
    [SerializeField] private TweenProperty2 pageUI;


    private void Awake()
    {
        _panels.Push(mainPanel.GetComponent<RectTransform>());
    }

    // Button targeting respective panes
    public void SlideIn(RectTransform rectTransform)
    {
        ComponentCheck(rectTransform, true);

        _panelInitialPos.Push(rectTransform.anchoredPosition);

        rectTransform.DOAnchorPos(pageUI.endValue, pageUI.Duration).SetEase(pageUI.EaseType)
            .OnComplete(() =>
            {
                var rmv = _panels.Pop();

                ComponentCheck(rmv, false);

                _panels.Push(rectTransform);
            });
    }

    // Back button in all panes
    public void SlideOut(RectTransform rectTransform)
    {
        ComponentCheck(rectTransform, true);

        var rt = _panels.Pop();

        rt.DOAnchorPos(_panelInitialPos.Pop(), pageUI.Duration).SetEase(pageUI.EaseType)
            .OnComplete(() =>
            {
                ComponentCheck(rt, false);

                _panels.Push(rectTransform);
            });
    }

    private void ComponentCheck(Component rectTransform, bool check)
    {
        // Fadeout pane, if canvas group is available.
        if (rectTransform.TryGetComponent(out _canvasGroup))
        {
            _canvasGroup.alpha = check == false ? 0 : 1;
            _canvasGroup.interactable = check;
            _canvasGroup.blocksRaycasts = check;
        }

        // Disable pane canvas, if canvas is available.
        else if (rectTransform.TryGetComponent(out _canvas))
        {
            _canvas.enabled = check;

            if (rectTransform.TryGetComponent(out _scrollRect) && !check)
                _scrollRect.content.anchoredPosition = pageUI.endValue;
        }

        // Disable pane gameobject, if non of the above is available.
        else
            rectTransform.gameObject.ToggleActive(check);
    }
}