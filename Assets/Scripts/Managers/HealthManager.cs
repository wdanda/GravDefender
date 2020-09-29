using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int currentHealth = 1000;

    private Player player = null;

    void Awake()
    {
        SetupSingleton();
        player = FindObjectOfType<Player>();
        if (!player) {
            Debug.LogError("Unable to find Player object");
            return;
        }
        currentHealth = player.GetInitialHealth();
    }

    public void Hit(int points)
    {
        this.currentHealth -= points;
        if (this.currentHealth <= 0) {
            this.currentHealth = 0;
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void ResetHealth()
    {
        Destroy(gameObject);
    }

    private void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

}
