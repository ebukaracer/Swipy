using UnityEngine;
using DG.Tweening;

public class SlideTween : MonoBehaviour
{
    [Header("Tween Properties")]
    public Vector3 endValue;

    public float duration;

    public Ease easeType;


    private void OnEnable()
    {
        DoBounce();
    }


    protected virtual void DoBounce()
    {
        transform.DOPunchScale(endValue, duration, 0).SetEase(easeType);
    }

    protected virtual void BounceIn(int i)
    {
        transform.DOPunchScale(endValue, duration, 0).SetEase(easeType);
    }
}
