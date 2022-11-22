using Racer.LoadManager;
using Racer.SaveSystem;
using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Racer.Utilities.SingletonPattern;
using Random = UnityEngine.Random;

/// <summary>
/// Handles UI-States
/// </summary>
internal class UIManagerGame : Singleton<UIManagerGame>
{
    private int _currentLevel;

    private Button _continueOrRetryB;
    private CommentGenerator _commentGenerator;

    public event Action<float> FinishedTimeCallback;

    [Header("GAME OVER UI")]
    [SerializeField] private UICanvasGroup gameoverUI;
    [SerializeField] private Image continueOrRetryI;

    [Space(5)]

    [Header("TEXTS")]
    [SerializeField] private TextMeshProUGUI timeT;
    [SerializeField] private TextMeshProUGUI commentT;
    [SerializeField] private TextMeshProUGUI nextLevelT;

    [Space(5)]

    [Header("MISC")]
    [SerializeField] private Sprite[] continueOrRetrySpr;

    protected override void Awake()
    {
        base.Awake();
        
        _continueOrRetryB = continueOrRetryI.GetComponentInChildren<Button>();
        
        _currentLevel = SaveSystem.GetData("CurrentLevelIndex", 1);

    }

    private void Start()
    {
        _commentGenerator = CommentGenerator.Instance;

        RatingCalculator.OnColorReturned += RatingCalculator_OnCurrentColor;
        GameController.Instance.OnReturnedRateCount += Instance_OnReturnedRate;
        GameController.Instance.GameoverCallback += Instance_GameoverCallback;
    }

    private void Instance_GameoverCallback(bool isGameover)
    {
        GameoverStuffs();

        if (!isGameover) return;

        SetCommentText(_commentGenerator.ZeroStarTexts);
        SetNextLevelText(0);
        continueOrRetryI.sprite = continueOrRetrySpr[0];
    }


    private void GameoverStuffs()
    {
        DisplayGameoverUI(true);
        _continueOrRetryB.onClick.AddListener(CheckWin);
    }


    private void RatingCalculator_OnCurrentColor(Color c)
    {
        timeT.color = c;
    }

    private void Instance_OnReturnedRate(int value)
    {
        SetNextLevelText(1);
        SetFinishedTime();

        continueOrRetryI.sprite = continueOrRetrySpr[1];

        switch (value)
        {
            case 1:
                SetCommentText(_commentGenerator.OneStarTexts);
                break;
            case 2:
                SetCommentText(_commentGenerator.TwoStarTexts);
                break;
            case 3:
                SetCommentText(_commentGenerator.ThreeStarTexts);
                break;
        }
    }

    public void DisplayGameoverUI(bool value)
    {
        if (value)
        {
            gameoverUI.canvasGroup.DOFade(gameoverUI.endValue.x, gameoverUI.Duration);
            gameoverUI.canvasGroup.interactable = true;
            gameoverUI.canvasGroup.blocksRaycasts = true;
        }
        else
            gameoverUI.canvasGroup.DOFade(gameoverUI.endValue.y, gameoverUI.Duration);
    }

    private void SetFinishedTime()
    {
        var totalTime = TimeController.Instance.GetTotalTime();

        timeT.SetText("{0:2}", totalTime);

        FinishedTimeCallback?.Invoke(totalTime);
    }

    private void SetCommentText(IReadOnlyList<string> texts)
    {
        commentT.text = texts[Random.Range(0, texts.Count)];
    }

    private void SetNextLevelText(int index)
    {
        nextLevelT.SetText("Level: {0}", _currentLevel + index);
    }

    // Reload/Next level button
    private static void CheckWin() => LoadNewScene(1);


    // Exit button
    public void ExitScene() => LoadNewScene(0);

    private static void LoadNewScene(int index)
    {
        LoadManager.Instance.LoadSceneAsync(index);
    }

    private void OnDestroy()
    {
        RatingCalculator.OnColorReturned -= RatingCalculator_OnCurrentColor;
        GameController.Instance.OnReturnedRateCount -= Instance_OnReturnedRate;
        GameController.Instance.GameoverCallback -= Instance_GameoverCallback;
    }
}