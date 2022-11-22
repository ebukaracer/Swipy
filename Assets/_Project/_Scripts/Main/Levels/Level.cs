using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-5)]
internal class Level : MonoBehaviour
{
    private TextMeshProUGUI _levelT;
    private Button _btn;

    public event Action<Level> OnHasClicked;

    public float SwipeTime { get; set; }
    public int SwipeCount { get; set; }
    public int Rating { get; set; }
    public int LevelNumber { get; private set; }
    public bool HasUnlocked { get; set; }


    private void Awake()
    {
        _levelT = GetComponentInChildren<TextMeshProUGUI>();
        _btn = GetComponent<Button>();

        _btn.onClick.AddListener(OnClicked);
    }


    public void SetLevelNumber(int index)
    {
        _levelT.text = $"{index}";

        LevelNumber = index;
    }

    private void OnClicked() => OnHasClicked?.Invoke(this);
}
