using Racer.SaveSystem;
using System;
using UnityEngine;

internal class Score : MonoBehaviour
{
    private int _initial;
    private float _current;

    public event Action<float> OnScoreChanged;

    private void Awake()
    {
        _current = 0;

        _initial = SaveSystem.GetData<int>
            ($"SwipeCount_{SaveSystem.GetData("CurrentLevelIndex", 1)}");
    }

    public void ModifyScore(float amt)
    {
        _current += amt;

        var pctChange = _current / _initial;

        OnScoreChanged?.Invoke(pctChange);
    }
}
