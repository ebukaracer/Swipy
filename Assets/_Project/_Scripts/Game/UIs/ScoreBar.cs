using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

internal class ScoreBar : MonoBehaviour
{
    private Score _score;
    private Image _scoreBarI;

    [Range(0, 1), SerializeField]
    protected float smoothDuration = .25f;

    private void Awake()
    {
        _score = GetComponent<Score>();

        _scoreBarI = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        _scoreBarI.fillAmount = 0;

        _score.OnScoreChanged += ScoreFill_OnScoreChanged;
    }

    private void ScoreFill_OnScoreChanged(float amt)
    {
        StartCoroutine(SmoothChange(amt));
    }

    protected virtual IEnumerator SmoothChange(float amount = 0)
    {
        var initialFillAmt = _scoreBarI.fillAmount;

        float elapsed = 0;

        while (elapsed < smoothDuration)
        {
            elapsed += Time.deltaTime;

            _scoreBarI.fillAmount = Mathf.Lerp(initialFillAmt, amount, elapsed / smoothDuration);

            yield return 0;
        }

        _scoreBarI.fillAmount = amount;
    }
}
