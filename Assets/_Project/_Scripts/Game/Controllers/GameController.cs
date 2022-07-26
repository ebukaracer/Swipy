using Racer.SaveSystem;
using System;
using UnityEngine;
using static Racer.Utilities.SingletonPattern;

class GameController : Singleton<GameController>
{
    int currentLevelIndex;

    int mostRecentLevel;

    float totalSwipeTime;

    [SerializeField]
    HealthBar healthBar;

    [SerializeField]
    RateTween rateAnimation;

    public event Action<int> OnReturnedRateCount;

    public event Action<bool> GameoverCallback;



    private void Start()
    {
        currentLevelIndex = SaveSystem.GetData("CurrentLevelIndex", 1);

        mostRecentLevel = SaveSystem.GetData("MostRecentLevel", 1);

        totalSwipeTime = SaveSystem.GetData<float>($"TotalSwipeTime_{currentLevelIndex}");
    }


    public void GameCompletion(bool healthStatus)
    {
        // Lost
        if (healthStatus)
            Fail();
        // Won
        else
            Won();

        GameoverCallback?.Invoke(healthStatus);
    }

    void Won()
    {
        // Logic  

        var currentSwipeTime = TimeController.Instance.GetTotalTime();

        int rateCount = RatingCalculator.CalculateRating(currentSwipeTime, totalSwipeTime, healthBar.HealthCount);

        rateAnimation.ShowStarAnimation(rateCount);

        OnReturnedRateCount?.Invoke(rateCount);

        SaveSystem.SaveData($"StarCount_{currentLevelIndex}", rateCount);


        if (currentLevelIndex == mostRecentLevel)
            mostRecentLevel++;

        currentLevelIndex++;

        SaveSystem.SaveData("CurrentLevelIndex", currentLevelIndex);

        SaveSystem.SaveData("MostRecentLevel", mostRecentLevel);
    }

    void Fail()
    {
        // Game over UI

        SaveSystem.SaveData($"StarCount_{currentLevelIndex}", 0);
    }
}
