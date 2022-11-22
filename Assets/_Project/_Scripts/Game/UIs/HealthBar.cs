using UnityEngine;
using UnityEngine.UI;
using System.Collections;

internal class HealthBar : ScoreBar
{
    public int HealthCount { get; private set; } // Static
    public int HealthStatus { get; private set; } // Dynamic

    [SerializeField]
    private Image[] healthBars;


    private void Start()
    {
        HealthCount = healthBars.Length;
        HealthStatus = HealthCount - 1;
    }

    public void ModifyHealth()
    {
        if (GameManager.Instance.IsGameover)
            return;

        StartCoroutine(SmoothChange());

        GameOverCheck();
    }

    protected override IEnumerator SmoothChange(float amount = 0)
    {
        var initialValue = healthBars[HealthStatus].fillAmount;

        float elapsed = 0;

        while (elapsed < smoothDuration)
        {
            elapsed += Time.deltaTime;

            healthBars[HealthStatus].fillAmount = Mathf.Lerp(initialValue, amount, elapsed / smoothDuration);

            yield return 0;
        }

        healthBars[HealthStatus].fillAmount = amount;

        HealthStatus--;
    }

    private void GameOverCheck()
    {
        if (HealthStatus <= 0)
            GameManager.Instance.SetGameState(GameState.GameOver);
    }
}
