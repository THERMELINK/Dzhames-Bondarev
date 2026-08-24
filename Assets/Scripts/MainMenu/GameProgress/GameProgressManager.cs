using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Dictionary<int, bool> levelCompleted = new Dictionary<int, bool>();

    [SerializeField] int enemiesKilled = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //adding 3 levels (for now)
        levelCompleted.Add(0, false);
        levelCompleted.Add(1, false);
        levelCompleted.Add(2, false);

    }
    private void OnEnable()
    {
        HealthManager.OnEnemyDeath += ChangeKillCount;
    }
    private void OnDisable()
    {
        HealthManager.OnEnemyDeath -= ChangeKillCount;
    }

    public void CompleteLevel(int number)
    {
        if (levelCompleted.ContainsKey(number))
        {
            levelCompleted[number] = true;
        }
        else
        {
            print("could not find level");
        }
    }

    public Dictionary<int, bool> TellWhichLevelsCompleted() => levelCompleted;
    public int TellKillCount() => enemiesKilled;

    public void ChangeKillCount(int amount)
    {
        enemiesKilled += amount;
    }


}
