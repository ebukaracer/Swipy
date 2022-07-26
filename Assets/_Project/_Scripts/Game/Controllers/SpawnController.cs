using System;
using UnityEngine;
using Racer.SaveSystem;
using static Racer.Utilities.SingletonPattern;

public class SpawnController : Singleton<SpawnController>
{
    public event Action<DragController> OnHasSpawned;

    DragController dragController;

    int count = 0;

    int spawnAmount;

    [SerializeField]
    Score scoreFill;


    protected override void Awake()
    {
        base.Awake();

        // Gotten from Level_Amount for each level
        spawnAmount = SaveSystem.GetData<int>($"SwipeCount_{SaveSystem.GetData("CurrentLevelIndex",1)}");
    }

    private void Start()
    {
        SpawnSlide();
    }

    public void SpawnSlide()
    {
        // Health exhausted
        if (GameManager.Instance.IsGameover)
        {
            GameController.Instance.GameCompletion(true);
            return;
        }

        var slide = ObjectPool.Instance.GetObject();

        if (slide is null)
            return;


        dragController = slide.GetComponent<DragController>();

        OnHasSpawned?.Invoke(dragController);

        dragController.OnDestroyed += SpawnManager_OnDestroyed;

        // Bonus Score
        count++;
    }

    private void SpawnManager_OnDestroyed()
    {
        if (count < spawnAmount)
            SpawnSlide();

        // Player finished swiping
        else
        {
            GameManager.Instance.SetGameState(GameStates.GameOver);

            GameController.Instance.GameCompletion(false);
        }

        scoreFill.ModifyScore(1f);
    }

    private void OnDestroy()
    {
        dragController.OnDestroyed -= SpawnManager_OnDestroyed;
    }
}