using System;
using UnityEngine;
using Racer.SaveSystem;
using static Racer.Utilities.SingletonPattern;

internal class SpawnController : Singleton<SpawnController>
{
    private SlideController _slideController;

    private bool _isGameover;
    private int _count;
    private int _spawnAmount;

    public event Action<SlideController> OnHasSpawned;

    [SerializeField] private Score scoreFill;


    protected override void Awake()
    {
        base.Awake();

        _spawnAmount = SaveSystem.GetData<int>
            ($"SwipeCount_{SaveSystem.GetData("CurrentLevelIndex", 1)}");
    }

    private void Start()
    {
        SpawnSlide();

        GameManager.OnCurrentState += GameManager_OnCurrentState;
    }

    public void SpawnSlide()
    {
        if (_isGameover)
        {
            GameController.Instance.GameCompletion(true);
            return;
        }

        var slide = ObjectPool.Instance.GetObject();

        if (!slide)
            return;

        if (!slide.TryGetComponent(out _slideController)) return;

        OnHasSpawned?.Invoke(_slideController);

        _slideController.OnDestroyed += SpawnManager_OnDestroyed;

        _count++;
    }

    private void SpawnManager_OnDestroyed()
    {
        if (_count < _spawnAmount)
            SpawnSlide();

        else
        {
            GameManager.Instance.SetGameState(GameState.GameOver);

            GameController.Instance.GameCompletion(false);
        }

        scoreFill.ModifyScore(1f);
    }

    private void GameManager_OnCurrentState(GameState value)
    {
        _isGameover = value == GameState.GameOver;
    }

    private void OnDestroy()
    {
        _slideController.OnDestroyed -= SpawnManager_OnDestroyed;

        GameManager.OnCurrentState -= GameManager_OnCurrentState;
    }
}