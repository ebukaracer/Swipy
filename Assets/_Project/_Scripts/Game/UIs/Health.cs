using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    float currentHealth;

    public int initialHealth;

    public event Action<float> OnHealthChange = delegate { };


    private void OnEnable()
    {
        currentHealth = initialHealth;
    }

    public void ModifyHealth(float damageAmount)
    {
        if (GameManager.Instance.IsGameover)
            return;

        // 1-0
        currentHealth -= damageAmount;

        OnHealthChange?.Invoke(0);

        if (currentHealth <= 0)
            GameManager.Instance.SetGameState(GameStates.GameOver);
    }
}
