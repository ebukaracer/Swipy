using UnityEngine;
using System.Collections;
using UnityEngine.UI;

class ScoreBar : MonoBehaviour
{
    Score score;

    Image scoreBarI;

    [Range(0, 1)]
    public float duration = .25f;


    private void Start()
    {
        score = GetComponent<Score>();

        scoreBarI = transform.GetChild(0).GetComponent<Image>();

        scoreBarI.fillAmount = 0;

        score.OnScoreChanged += ScoreFill_OnScoreChanged;
    }

    private void ScoreFill_OnScoreChanged(float amt)
    {
        StartCoroutine(SmoothChange(amt));
    }


    protected virtual IEnumerator SmoothChange(float amount)
    {
        float initialFillAmt = scoreBarI.fillAmount;

        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            scoreBarI.fillAmount = Mathf.Lerp(initialFillAmt, amount, elapsed / duration);

            yield return null;
        }

        scoreBarI.fillAmount = amount;
    }
}
