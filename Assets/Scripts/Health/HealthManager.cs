using System.Linq.Expressions;
using UnityEngine;

public class HealthManager : MonoBehaviour, Health
{
    float currentHealth = 0;
    float maxHealth = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RemoveHealth(float amount)
    {

    }

    public void AddHealth(float amount)
    {

    }

    public void TellHealth()
    {

    }

    public bool CheckIfDead()
    {
        bool returnThing = (currentHealth <= 0) ? true : false;
        return returnThing;
    }
}
