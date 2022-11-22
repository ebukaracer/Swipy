using Racer.SaveSystem;
using System;
using UnityEngine;
using static Racer.Utilities.SingletonPattern;

internal class GameController : Singleton<GameController>
{
    private int _currentLevelIndex;
    private int _mostRecentLevel;
    private float _requiredTime;

    public event Action<int> OnReturnedRateCount;
    public event Action<bool> GameoverCallback;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private RateTween rateAnimation;


    protected override void Awake()
    {
        base.Awake();

        _currentLevelIndex = SaveSystem.GetData("CurrentLevelIndex", 1);
        _mostRecentLevel = SaveSystem.GetData("MostRecentLevel", 1);
        _requiredTime = SaveSystem.GetData<float>($"TotalSwipeTime_{_currentLevelIndex}");
    }

    public void GameCompletion(bool healthStatus)
    {
        if (healthStatus)
            Lost();
        else
            Won();

        GameoverCallback?.Invoke(healthStatus);
    }

    private void Won()
    {
        var time = TimeController.Instance.GetTotalTime();
        var rateCount = RatingCalculator.CalculateRating(time, _requiredTime, healthBar.HealthStatus);

        rateAnimation.ShowStarAnimation(rateCount);
        OnReturnedRateCount?.Invoke(rateCount);

        SaveSystem.SaveData($"StarCount_{_currentLevelIndex}", rateCount);


        if (_currentLevelIndex == _mostRecentLevel)
            _mostRecentLevel++;

        _currentLevelIndex++;

        SaveSystem.SaveData("CurrentLevelIndex", _currentLevelIndex);
        SaveSystem.SaveData("MostRecentLevel", _mostRecentLevel);
    }

    private void Lost()
    {
        SaveSystem.SaveData($"StarCount_{_currentLevelIndex}", 0);
    }
}
