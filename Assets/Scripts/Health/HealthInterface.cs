using UnityEngine;


interface Health
{
    void RemoveHealth(float amount);

    void AddHealth(float amount);

    void TellHealth();

    public bool CheckIfDead();
}
