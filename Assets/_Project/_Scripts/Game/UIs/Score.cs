using Racer.SaveSystem;
using System;
using UnityEngine;

class Score : MonoBehaviour
{
    int initial;

    float current;

    public event Action<float> OnScoreChanged = delegate { };

    private void Start()
    {
        current = 0;

        initial = SaveSystem.GetData<int>($"SwipeCount_{SaveSystem.GetData("CurrentLevelIndex", 1)}");
    }

    public void ModifyScore(float amt)
    {
        current += amt;

        float pctChange = current / initial;

        OnScoreChanged?.Invoke(pctChange);
    }
}
