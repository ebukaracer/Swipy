using Racer.SaveSystem;
using Racer.SoundManager;
using System;
using UnityEngine;
using static Racer.Utilities.SingletonPattern;

public enum GameStates
{
    Playing,
    GameOver
}

class GameManager : StaticInstance<GameManager>
{
    public static event Action<GameStates> OnCurrentState;

    public float GetStartDelay => startDelay;

    // For Visualization purpose
    [SerializeField]
    GameStates currentState;

    // Straightforward Check
    public bool IsGameover { get => currentState == GameStates.GameOver; }

    [Space(10), SerializeField]
    private float startDelay;

    [SerializeField]
    AudioClip gameoverSfx;



    private void Start()
    {
        SetGameState(GameStates.Playing);
    }

    /// <summary>
    /// Sets the current state of game
    /// </summary>
    /// <param name="state">Actual state to transition to</param>
    public void SetGameState(GameStates state)
    {
        switch (state)
        {
            case GameStates.GameOver:
                SoundManager.Instance.PlaySfx(gameoverSfx);
                break;
        }

        currentState = state;

        // Updates other scripts listening to the game's current state
        OnCurrentState?.Invoke(state);
    }

    public void SavePoint()
    {
        SaveSystem.Save();
    }
}
