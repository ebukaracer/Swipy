using Racer.SaveSystem;
using System.Collections.Generic;
using Racer.Utilities;
using UnityEngine;
using static Racer.Utilities.SingletonPattern;

internal class LevelManager : Singleton<LevelManager>
{
    private int _mostRecentLevel;

    [SerializeField] private List<Level> levels;
    [SerializeField] private Transform levelGenerator;

    public int TotalRating { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        InitLevelProperties();
    }


    private void InitLevelProperties()
    {
        _mostRecentLevel = SaveSystem.GetData("MostRecentLevel", 1);

        for (int i = 0; i < levels.Count; i++)
        {
            // Swipe Count
            if (SaveSystem.GetData<int>($"SwipeCount_{levels[i].LevelNumber}") == 0)
            {
                SaveSystem.SaveData($"SwipeCount_{levels[i].LevelNumber}", levels[i].SwipeCount);
            }

            // Swipe Time
            if (SaveSystem.GetData<float>($"TotalSwipeTime_{levels[i].LevelNumber}") == 0f)
            {
                SaveSystem.SaveData($"TotalSwipeTime_{levels[i].LevelNumber}", levels[i].SwipeTime);
            }

            // Ratings
            if (SaveSystem.GetData<int>($"StarCount_{levels[i].LevelNumber}") != 0)
            {
                var count = SaveSystem.GetData<int>($"StarCount_{levels[i].LevelNumber}");

                TotalRating += count;

                var rateUI = levels[i].GetComponentInChildren<RateInitializer>();

                if (!rateUI || !rateUI.gameObject.activeInHierarchy) continue;

                rateUI.SetRate(count);

                levels[i].Rating = count;
            }
        }

        for (int i = 0; i < _mostRecentLevel; i++)
        {
            UnlockLevel(i);
        }
    }

    #region Editor Prototype
    public void InitializeLevels()
    {
        levels.Clear();

        if (levelGenerator.childCount <= 0)
            return;

        for (int i = 0; i < levelGenerator.childCount; i++)
        {
            levels.Add(levelGenerator.GetChild(i).GetComponent<Level>());
        }
    }
    #endregion

    // Loads a level from level menu.
    public void LoadLevelManual(int index)
    {
        if (levels[index - 1].HasUnlocked)
        {
            SaveSystem.SaveData("CurrentLevelIndex", index);

            Logging.Log("Loading level: " + index);
        }
    }

    public void UnlockLevel(int index)
    {
        levels[index].HasUnlocked = true;
    }
}
