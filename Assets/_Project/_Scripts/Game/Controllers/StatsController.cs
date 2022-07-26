using Racer.SaveSystem;
using UnityEngine;

class StatsController : MonoBehaviour
{
    [SerializeField]
    DirectionController directionController;

    // Combo Stuffs
    int savedCombo;
    int maxCombo;

    // Time Stuffs
    float fastestTime = 100f;
    float savedFastestTime;
    float slowestTime;



    private void Start()
    {
        savedCombo = SaveSystem.GetData<int>("Combo");

        savedFastestTime = SaveSystem.GetData<float>("Fastest_Time");

        if (savedFastestTime != 0)
            fastestTime = savedFastestTime;

        slowestTime = SaveSystem.GetData<float>("Slowest_Time");

        GameManager.OnCurrentState += GameManager_OnCurrentState;

        UIManager_Game.Instance.FinishedTimeCallback += Instance_FinishedTimeCallback;

        directionController.OnComboChanged += DirectionController_OnComboChanged;
    }

    private void Instance_FinishedTimeCallback(float finishTime)
    {
        SaveTime(finishTime);
    }

    private void DirectionController_OnComboChanged(int maxCombo)
    {
        this.maxCombo = maxCombo;
    }

    private void GameManager_OnCurrentState(GameStates state)
    {
        if (state == GameStates.Playing)
            return;

        SaveCombo();

    }

    private void SaveCombo()
    {
        if (maxCombo > savedCombo)
            SaveSystem.SaveData("Combo", maxCombo);
    }

    void SaveTime(float finishTime)
    {

        if (finishTime > slowestTime)
        {
            slowestTime = finishTime;

            SaveSystem.SaveData("Slowest_Time", slowestTime);
        }
        if (finishTime < fastestTime)
        {
            fastestTime = finishTime;

            SaveSystem.SaveData("Fastest_Time", fastestTime);
        }
    }


    private void OnDestroy()
    {
        GameManager.OnCurrentState -= GameManager_OnCurrentState;
    }
}