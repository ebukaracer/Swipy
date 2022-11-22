using Racer.SoundManager;
using System;
using UnityEngine;
using static Racer.Utilities.SingletonPattern;

internal class GameManager : StaticInstance<GameManager>
{
    public static event Action<GameState> OnCurrentState;
    
    public bool IsGameover => currentState == GameState.GameOver;

    [SerializeField] private GameState currentState;
    [SerializeField] private AudioClip gameoverSfx;


    private void Start()
    {
        SetGameState(GameState.Playing);
    }

    public void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.GameOver:
                SoundManager.Instance.PlaySfx(gameoverSfx);
                break;
        }

        currentState = state;
        OnCurrentState?.Invoke(currentState);
    }
}
