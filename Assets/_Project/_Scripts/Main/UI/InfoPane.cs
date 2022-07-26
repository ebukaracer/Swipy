using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoPane : MonoBehaviour
{
    [SerializeField]
    Sprite star;

    [SerializeField]
    Sprite starOutline;

    [Space(10)]

    [SerializeField]
    Image[] stars;

    [Space(10)]

    [SerializeField]
    TextMeshProUGUI timeT;

    [SerializeField]
    TextMeshProUGUI swipeCountT;

    [SerializeField]
    TextMeshProUGUI currentLevelT;

    [Space(10)]

    [SerializeField]
    Button btn;

    [SerializeField]
    RectTransform rectTransform;


    private void OnEnable()
    {
        UIAnimations.Instance.PopIn(rectTransform);
    }

    private void OnDisable()
    {
        SetRatingToDefault();
    }


    public void SetTime(float time)
    {
        timeT.SetText("Time: {0}s", time); ;
    }

    public void SetSwipeCount(int count)
    {
        swipeCountT.SetText("Swipe Count: {0}", count);
    }

    public void SetCurrentLevel(int levelIndex)
    {
        currentLevelT.text = levelIndex.ToString();
    }

    public void SetInteractable(bool state)
    {
        btn.interactable = state;
    }

    public void SetRating(int count)
    {
        for (int i = 0; i < count; i++)
        {
            stars[i].sprite = star;
        }
    }

    public void SetRatingToDefault()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = starOutline;
        }
    }
}
