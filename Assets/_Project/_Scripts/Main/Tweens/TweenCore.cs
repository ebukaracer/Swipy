using System;
using DG.Tweening;
using UnityEngine;

internal class TweenCore
{
    public float Duration = .2f;
    public Ease EaseType = Ease.Linear;
}

internal class TweenProperty : TweenCore
{
    public Vector2 StartValue;
    public Vector2 EndValue;
}

[Serializable]
internal class TweenProperty2 : TweenCore
{
    public Vector3 endValue;
}

[Serializable]
internal class UIElement : TweenProperty
{
    public RectTransform rectTransform;
}

[Serializable]
internal class UICanvasGroup : TweenProperty2
{
    public CanvasGroup canvasGroup;
}

