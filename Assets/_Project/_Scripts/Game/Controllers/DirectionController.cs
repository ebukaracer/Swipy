using Racer.SoundManager;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

internal class DirectionController : MonoBehaviour
{
    private Color _original;
    private Color _newColor;
    private Direction _previousDir;

    private int _comboCount = 1;
    private int _maxCombo;

    private bool _isStarting;

    public event Action<int> OnNewCombo;

    [SerializeField] private Image[] directions;
    [SerializeField] private TextMeshProUGUI directionT;

    [Space(5), Header("REFERENCES")]
    [SerializeField] private Direction currentDir;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ComboController comboController;

    [Space(5), Header("CLIPS")]
    [SerializeField] private AudioClip errorClip;
    [SerializeField] private AudioClip swipeClip;


    private void Awake()
    {
        _original = directions[0].color;
        _newColor = Color.white;
    }

    private void Start()
    {
        SpawnController.Instance.OnHasSpawned += Instance_OnHasSpawned;
    }

    /// <summary>
    /// Generates a random direction as soon as a slide is spawned.
    /// </summary>
    /// <param name="slideController"></param>
    private void Instance_OnHasSpawned(SlideController slideController)
    {
        _previousDir = currentDir;
        currentDir = GenerateDirection();

        directionT.text = $"{currentDir}";
        directions[(int)currentDir].color = _newColor;

        slideController.OnMovedToDirection += SlideController_OnMovedToDirection; 
    }

    private void SlideController_OnMovedToDirection(Direction dir)
    {
        if (_isStarting)

            if (_previousDir == currentDir && dir == currentDir)
            {
                _comboCount++;

                if (_comboCount > _maxCombo)
                {
                    _maxCombo = _comboCount;

                    OnNewCombo?.Invoke(_maxCombo);
                }
            }
            else
                _comboCount = 1;
        else
            _isStarting = true;


        if (_comboCount > 1)
            comboController.PunchScale(_comboCount);

        for (int i = 0; i < directions.Length; i++)
        {
            directions[i].color = _original;
        }

        if (dir != currentDir)
        {
            healthBar.ModifyHealth();

            SoundManager.Instance.PlaySfx(errorClip, .5f);
        }
        else
            SoundManager.Instance.PlaySfx(swipeClip);
    }


    private static Direction GenerateDirection()
    {
        var i = Range(0, 4);

        switch (i)
        {
            case 0:
                return Direction.Up;
            case 1:
                return Direction.Down;
            case 2:
                return Direction.Left;
            case 3:
                return Direction.Right;
        }

        return 0;
    }

    private void OnDestroy()
    {
        SpawnController.Instance.OnHasSpawned -= Instance_OnHasSpawned;
    }
}