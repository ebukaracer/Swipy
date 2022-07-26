using UnityEngine;
using UnityEngine.UI;
using System.Collections;

class HealthBar : ScoreBar
{
    int healthBarIndex = 0;

    Health health;

    public int HealthCount { get; private set; }

    [Space(10f)]

    [SerializeField]
    Image[] healthBars;


    private void Start()
    {
        health = GetComponent<Health>();

        HealthCount = health.initialHealth;

        health.OnHealthChange += Health_OnHealthChange;
    }


    private void Health_OnHealthChange(float amt)
    {
        HealthCount--;

        StartCoroutine(SmoothChange(amt));

    }

    protected override IEnumerator SmoothChange(float amount)
    {
        float initialHealthbarValue = healthBars[healthBarIndex].fillAmount;

        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            healthBars[healthBarIndex].fillAmount = Mathf.Lerp(initialHealthbarValue, amount, elapsed / duration);

            yield return null;
        }

        healthBars[healthBarIndex].fillAmount = amount;

        healthBarIndex++;
    }

    private void OnDisable()
    {
        health.OnHealthChange -= Health_OnHealthChange;
    }
}
