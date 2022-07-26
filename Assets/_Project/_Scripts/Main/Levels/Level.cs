using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    TextMeshProUGUI levelT;

    Button btn;

    public event Action<Level> OnHasClicked;


    public float SwipeTime { get; set; }

    public int SwipeCount { get; set; }

    public int Rating { get; set; }

    public int GetLevelNumber { get; private set; }

    public bool HasUnlocked { get; set; }


    private void Awake()
    {
        levelT = GetComponentInChildren<TextMeshProUGUI>();

        btn = GetComponent<Button>();

        btn.onClick.AddListener(OnClicked);
    }


    public void SetLevelNumber(int index)
    {
        levelT.text = index.ToString();

        GetLevelNumber = index;
    }

    void OnClicked() => OnHasClicked?.Invoke(this);

}
