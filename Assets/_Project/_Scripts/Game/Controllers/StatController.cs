using Racer.SaveSystem;
using UnityEngine;

internal class StatController : MonoBehaviour
{
    // Combo
    private int _savedCombo;
    private int _maxCombo;

    // Timing
    private float _fastestTime;
    private float _slowestTime;

    [SerializeField]
    private DirectionController directionController;


    private void Awake()
    {
        _savedCombo = SaveSystem.GetData<int>("Combo");
        _fastestTime = SaveSystem.GetData<float>("Fastest_Time");
        _slowestTime = SaveSystem.GetData<float>("Slowest_Time");
    }

    private void Start()
    {
        GameManager.OnCurrentState += GameManager_OnCurrentState;
        UIManagerGame.Instance.FinishedTimeCallback += Instance_FinishedTimeCallback;
        directionController.OnNewCombo += DirectionController_OnNewCombo;
    }

    private void Instance_FinishedTimeCallback(float finishTime)
    {
        SaveTime(finishTime);
    }

    private void DirectionController_OnNewCombo(int maxCombo)
    {
        _maxCombo = maxCombo;
    }

    private void GameManager_OnCurrentState(GameState state)
    {
        if (state == GameState.Playing)
            return;

        SaveCombo();
    }

    private void SaveCombo()
    {
        if (_maxCombo > _savedCombo)
            SaveSystem.SaveData("Combo", _maxCombo);
    }

    private void SaveTime(float finishTime)
    {
        if (_slowestTime == 0 && _fastestTime == 0)
        {
            SaveSystem.SaveData("Fastest_Time", finishTime);
            SaveSystem.SaveData("Slowest_Time", finishTime);
        }
        else if (finishTime > _slowestTime)
            SaveSystem.SaveData("Slowest_Time", finishTime);

        else if (finishTime < _fastestTime)
            SaveSystem.SaveData("Fastest_Time", finishTime);
    }

    private void OnDestroy()
    {
        GameManager.OnCurrentState -= GameManager_OnCurrentState;
        UIManagerGame.Instance.FinishedTimeCallback -= Instance_FinishedTimeCallback;
        directionController.OnNewCombo -= DirectionController_OnNewCombo;
    }
}