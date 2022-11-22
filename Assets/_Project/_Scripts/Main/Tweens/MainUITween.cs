using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Racer.Utilities;

internal class MainUITween : MonoBehaviour
{
    private TweenerCore<Vector2, Vector2, VectorOptions> _handAnim;

    [Header("TWEENER")]
    [SerializeField] private UIElement infoUI;
    [SerializeField] private UIElement handUI;


    public void InfoIn()
    {
        infoUI.rectTransform.gameObject.ToggleActive(true);
        infoUI.rectTransform.DOScale(infoUI.EndValue, infoUI.Duration).SetEase(infoUI.EaseType);
    }

    public void InfoOut()
    {
        infoUI.rectTransform.DOScale(infoUI.StartValue, infoUI.Duration).SetEase(infoUI.EaseType)
            .OnComplete(() =>
                infoUI.rectTransform.gameObject.ToggleActive(false));
    }

    private void HandAnim()
    {
        _handAnim = handUI.rectTransform.DOAnchorPos(handUI.EndValue,
                handUI.Duration)
            .SetLoops(-1,
                LoopType.Yoyo)
            .SetEase(handUI.EaseType);
    }

    /*
    private void OnDestroy()
    {
         handAnim?.Kill(false);
    }
    */
}
