using System;
using System.Linq.Expressions;
using UnityEngine;

public class HealthManager : MonoBehaviour, Health
{
    float currentHealth = 0;
    float maxHealth = 100;
    public static event Action<int> OnEnemyDeath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }


    /// <summary>
    /// removes a certain amount of health from the game object it is on
    /// also checks if its dead after health is removed
    /// </summary>
    public void RemoveHealth(float amount)
    {
        currentHealth -= amount;
        if (CheckIfDead())
        {
            BeDead();
        }
    }

    /// <summary>
    /// this method adds a certain amount of health
    /// not implemented yet, but usefull for pickups
    /// </summary>
    public void AddHealth(float amount)
    {
        currentHealth += amount;
    }


    /// <summary>
    /// tells the current health from this object
    /// </summary>
    public float TellHealth() => currentHealth;


    /// <summary>
    /// checks if the health is lower or equal than 0
    /// </summary>
    public bool CheckIfDead()
    {
        bool returnThing = (currentHealth <= 0) ? true : false;
        return returnThing;
    }

    /// <summary>
    /// currently removes the game object from the scene
    /// </summary>
    void BeDead()
    {
        if (gameObject == GameStateManager.instance.TellPlayerObject())
        {
            GameStateManager.instance.FailPlayer();
        }
        else
        {
            //object is not the player, so it is an enemy
            //trigger add 1 to killcount
            OnEnemyDeath?.Invoke(1);
            Destroy(gameObject);
        }

    }


}
