using UnityEngine;


interface Health
{
    void RemoveHealth(float amount);

    void AddHealth(float amount);

    float TellHealth();

    public bool CheckIfDead();
}
