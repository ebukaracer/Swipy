using UnityEngine;
using TMPro;
using UnityEngine.UI;

internal class InfoPane : MonoBehaviour
{
    [SerializeField] private Sprite star;
    [SerializeField] private Sprite starOutline;

    [Space(5), SerializeField]
    private Image[] stars;

    [Space(5), Header("TEXTS")]
    [SerializeField] private TextMeshProUGUI timeT;
    [SerializeField] private TextMeshProUGUI swipeCountT;
    [SerializeField] private TextMeshProUGUI currentLevelT;

    [Space(5), Header("MISC")]
    [SerializeField] private Button btn;


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
        currentLevelT.text = $"{levelIndex}";
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
