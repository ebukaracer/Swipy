using Racer.SaveSystem;
using Racer.SoundManager;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

// Order matters 
public enum Directions
{
    UP, DOWN,
    LEFT, RIGHT
}

public class DirectionController : MonoBehaviour
{
    Color original;
    Color newColor;

    int comboCount = 1;
    int maxCombo;

    bool isStarting;

    public event Action<int> OnComboChanged;

    [SerializeField]
    Image[] directions;

    [SerializeField]
    TextMeshProUGUI directionT;

    [Space(10)]

    [SerializeField]
    Directions currentDir;

    Directions previousDir;

    [Space(10)]

    [SerializeField]
    Health playerHealth;

    [Space(5)]

    [SerializeField]
    ComboController comboController;

    [Space(5), SerializeField]
    AudioClip errorClip;

    [SerializeField]
    AudioClip swipeClip;


    private void Awake()
    {
        original = directions[0].color;
        newColor = Color.white;
    }


    private void Start()
    {
        SpawnController.Instance.OnHasSpawned += Instance_OnHasSpawned;
    }



    /// <summary>
    /// Generates a random direction as soon as a slide is spawned.
    /// </summary>
    /// <param name="dragController"></param>
    private void Instance_OnHasSpawned(DragController dragController)
    {
        previousDir = currentDir;

        currentDir = GenerateDirection();

        directionT.text = currentDir.ToString();

        directions[(int)currentDir].color = newColor;

        dragController.OnMovedToDirection += DragController_OnMovedToDirection;
    }

    private void DragController_OnMovedToDirection(Directions dir)
    {
        //Debug.Log("Yea");

        if (isStarting)

            if (previousDir == currentDir && dir == currentDir)
            {
                comboCount++;

                if (comboCount > maxCombo)
                {
                    maxCombo = comboCount;

                    OnComboChanged.Invoke(maxCombo);
                }
            }
            else
                comboCount = 1;
        else
            isStarting = true;

        if (comboCount > 1)
            comboController.PunchScale(comboCount);



        for (int i = 0; i < directions.Length; i++)
        {
            directions[i].color = original;
        }

        if (dir != currentDir)
        {
            playerHealth.ModifyHealth(1f); //Full attack

            SoundManager.Instance.PlaySfx(errorClip, .5f);
        }
        else
            SoundManager.Instance.PlaySfx(swipeClip, .8f);
    }


    Directions GenerateDirection()
    {
        var i = Range(0, 4);

        switch (i)
        {
            case 0:
                return Directions.UP;
            case 1:
                return Directions.DOWN;
            case 2:
                return Directions.LEFT;
            case 3:
                return Directions.RIGHT;
            default:
                break;
        }

        return 0;
    }
}