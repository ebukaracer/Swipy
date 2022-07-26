using Racer.SaveSystem;
using System.Collections.Generic;
using UnityEngine;
using static Racer.Utilities.SingletonPattern;

public class LevelManager : Singleton<LevelManager>
{
    int mostRecentLevel;

    int totalRatings;

    [SerializeField]
    List<Level> levels;

    [SerializeField]
    Transform levelGenerator;

    public int GetTotalRatings { get => totalRatings; }


    // Review
    public void InitializeLevels()
    {
        levels.Clear();

        if (levelGenerator.childCount <= 0)
        {
            return;
        }

        // Populate list
        for (int i = 0; i < levelGenerator.childCount; i++)
        {
            levels.Add(levelGenerator.GetChild(i).GetComponent<Level>());
        }
    }

    private void Start()
    {

        for (int i = 0; i < levels.Count; i++)
        {
            if (SaveSystem.GetData<int>($"SwipeCount_{levels[i].GetLevelNumber}") == 0)
            {
                SaveSystem.SaveData($"SwipeCount_{levels[i].GetLevelNumber}", levels[i].SwipeCount);
            }

            if (SaveSystem.GetData<float>($"TotalSwipeTime_{levels[i].GetLevelNumber}") == 0f)
            {
                SaveSystem.SaveData($"TotalSwipeTime_{levels[i].GetLevelNumber}", levels[i].SwipeTime);
            }

            if (SaveSystem.GetData<int>($"StarCount_{levels[i].GetLevelNumber}") != 0)
            {
                var rateUI = levels[i].GetComponentInChildren<RateInitializer>();

                if (rateUI != null && rateUI.gameObject.activeInHierarchy)
                {
                    int count = SaveSystem.GetData<int>($"StarCount_{levels[i].GetLevelNumber}");

                    rateUI.SetRate(count);

                    levels[i].Rating = count;

                    totalRatings += count;
                }
            }
        }

        mostRecentLevel = SaveSystem.GetData("MostRecentLevel", 1);


        // Level 1 Unlocked here(auto)
        for (int i = 0; i < mostRecentLevel; i++)
        {
            UnlockLevel(i);
        }
    }


    // When player manually selects a level
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
