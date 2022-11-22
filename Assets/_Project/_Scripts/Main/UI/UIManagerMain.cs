using Racer.LoadManager;
using Racer.SaveSystem;
using Racer.Utilities;
using TMPro;
using UnityEngine;

internal class UIManagerMain : SingletonPattern.Singleton<UIManagerMain>
{
    private MainUITween _mainUITween;

    [Header("TEXTS")]
    [SerializeField] private TextMeshProUGUI mainLevelT;
    [SerializeField] private TextMeshProUGUI maxComboT;
    [SerializeField] private TextMeshProUGUI fastestTimeT;
    [SerializeField] private TextMeshProUGUI slowestTimeT;
    [SerializeField] private TextMeshProUGUI totalRatingCountT;

    protected override void Awake()
    {
        base.Awake();

        _mainUITween = GetComponent<MainUITween>();

        InitSavedValues();
    }

    private void InitSavedValues()
    {
        maxComboT.SetText("Max Combo: {0}", SaveSystem.GetData<int>("Combo"));
        fastestTimeT.SetText("Fastest Time: {0:2}", SaveSystem.GetData<float>("Fastest_Time"));
        slowestTimeT.SetText("Slowest Time: {0:2}", SaveSystem.GetData<float>("Slowest_Time"));
        totalRatingCountT.SetText("{0}/180", LevelManager.Instance.TotalRating);

        // Sets the most recent level
        mainLevelT.SetText("Level: {0}", SaveSystem.GetData("MostRecentLevel", 1));
    }

    public void ToggleInfoUI()
    {
        _mainUITween.InfoIn();
    }

    // Reset button
    public void ResetAction()
    {
        SaveSystem.DeleteSaveFile();
        LoadManager.Instance.LoadSceneAsync(0);
    }

    // Exit button
    public void ExitAction()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.Quit();
#else
        Logging.Log("Exited!");
#endif
    }

    // Play button
    public void PlayAction()
    {
        LoadManager.Instance.LoadSceneAsync(1);
    }
}