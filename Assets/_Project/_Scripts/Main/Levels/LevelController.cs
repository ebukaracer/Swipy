using Racer.LoadManager;
using Racer.Utilities;
using UnityEngine;
using UnityEngine.UI;

internal class LevelController : MonoBehaviour
{
    private int _currentlySelectedLevel;
    private Level _level;

    [Header("INFO BAR ELEMENTS")]
    [SerializeField] private InfoPane infoPane;
    [SerializeField] private Button infoPaneBtnPlay;


    private void Start()
    {
        if (infoPaneBtnPlay.IsInteractable())
            infoPaneBtnPlay.onClick.AddListener(MoveToNextLevel);

        infoPane.transform.localScale = Vector3.zero;
    }


    public void InitializeLevelButtons(int index)
    {
        var go = transform.GetChild(index);

        if (!go.TryGetComponent(out _level))
        {
            Logging.LogWarning($"No {nameof(Level)} attached to {go.name}");
            return;
        }

        var i = index + 1;

        _level.SetLevelNumber(i);
        _level.SwipeCount = (i * 5);
        _level.SwipeTime = _level.SwipeCount / 20f;
        _level.OnHasClicked += Level_OnHasClicked;
    }

    private void Level_OnHasClicked(Level level)
    {
        if (infoPane.gameObject.activeInHierarchy) return;

        infoPane.SetRating(level.Rating);
        infoPane.SetInteractable(level.HasUnlocked);
        infoPane.SetCurrentLevel(level.LevelNumber);
        infoPane.SetSwipeCount(level.SwipeCount);
        infoPane.SetTime(level.SwipeTime);

        UIManagerMain.Instance.ToggleInfoUI();

        _currentlySelectedLevel = level.LevelNumber;
    }

    private void MoveToNextLevel()
    {
        LevelManager.Instance.LoadLevelManual(_currentlySelectedLevel);
        LoadManager.Instance.LoadSceneAsync(1);
    }
}