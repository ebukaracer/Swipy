using Racer.SaveSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Racer.Utilities.SingletonPattern;

/// <summary>
/// Handles UI-States
/// </summary>

public class UIManager_Game : Singleton<UIManager_Game>
{
    public event Action<float> FinishedTimeCallback;

    int currentLevel;

    [SerializeField]
    CommentGenerator commentGenerator;

    Button continueOrRetryB;

    [SerializeField]
    GameoverUITween gameoverUITween;

    [Space(5), SerializeField]
    Image continueOrRetryI;

    [Header("Game over UI stuffs")]

    [SerializeField]
    TextMeshProUGUI timeT;

    [SerializeField]
    TextMeshProUGUI commentT;

    [SerializeField]
    TextMeshProUGUI nextLevelT;

    [Space(5), SerializeField]
    GameObject[] elements;

    [Space(5), SerializeField]
    Sprite[] continueOrRetrySpr;


    private void Start()
    {
        currentLevel = SaveSystem.GetData("CurrentLevelIndex", 1);

        continueOrRetryB = continueOrRetryI.GetComponentInChildren<Button>();

        RatingCalculator.OnColorReturned += RatingCalculator_OnCurrentColor;

        GameController.Instance.OnReturnedRateCount += Instance_OnReturnedRate;

        GameController.Instance.GameoverCallback += Instance_GameoverCallback;
    }

    private void Instance_GameoverCallback(bool isGameover)
    {
        GameoverStuffs();

        if (isGameover)
        {
            SetCommentText(commentGenerator.GetZeroStarComment());

            SetNextLevelText(0);

            continueOrRetryI.sprite = continueOrRetrySpr[0];
        }
    }


    private void GameoverStuffs()
    {
        gameoverUITween.SetActive();

        continueOrRetryB.onClick.AddListener(CheckWin);
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
                SetCommentText(commentGenerator.GetOneStarComment());
                break;
            case 2:
                SetCommentText(commentGenerator.GetTwoStarComment());
                break;
            case 3:
                SetCommentText(commentGenerator.GetThreeStarComment());
                break;
        }
    }


    void SetFinishedTime()
    {
        // TODO: Investigate
        // var value = Math.Round((decimal)timeCounter.GetTotalTime(), 2);

        var totalTime = TimeController.Instance.GetTotalTime();

        timeT.SetText("{0:2}", totalTime);

        FinishedTimeCallback?.Invoke(totalTime);
    }


    void SetCommentText(string t)
    {
        commentT.text = t;
    }

    void CheckWin()
    {
        LoadNewScene(0);
    }


    void SetNextLevelText(int index)
    {
        nextLevelT.SetText("Level: {0}", currentLevel + index);
    }


    public void ExitScene()
    {
        LoadNewScene(-1);
    }

    void LoadNewScene(int index) =>
        Utilities.LoadNewScene(index);


    private void OnDestroy()
    {
        RatingCalculator.OnColorReturned -= RatingCalculator_OnCurrentColor;
    }
}