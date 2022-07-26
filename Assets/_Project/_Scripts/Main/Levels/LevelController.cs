using UnityEngine;
using UnityEngine.UI;

class LevelController : MonoBehaviour
{
    int currentlySelectedLevel;

    Level level;

    [Header("Info Bar Elements")]

    [SerializeField]
    InfoPane infoPane;

    [SerializeField]
    Button infoPaneBtnPlay;


    private void Start()
    {
        if (infoPaneBtnPlay.IsInteractable())
            infoPaneBtnPlay.onClick.AddListener(MoveToNextLevel);

        infoPane.transform.localScale = Vector3.zero;
    }


    public void InitializeLevelButtons(int index)
    {
        var go = transform.GetChild(index);

        level = go.GetComponent<Level>();

        int i = index + 1;

        level.SetLevelNumber(i);

        level.SwipeCount = (i * 5);

        // Review
        level.SwipeTime = level.SwipeCount / 20f;

        level.OnHasClicked += Level_OnHasClicked;
    }

    private void Level_OnHasClicked(Level level)
    {
        if (!infoPane.gameObject.activeInHierarchy)
        {
            infoPane.SetRating(level.Rating);

            infoPane.SetInteractable(level.HasUnlocked);

            infoPane.SetCurrentLevel(level.GetLevelNumber);

            currentlySelectedLevel = level.GetLevelNumber;

            infoPane.SetSwipeCount(level.SwipeCount);

            infoPane.SetTime(level.SwipeTime);

            infoPane.gameObject.SetActive(true);
        }
    }

    void MoveToNextLevel()
    {
        LevelManager.Instance.LoadLevelManual(currentlySelectedLevel);

        Utilities.LoadNewScene(1);
    }
}