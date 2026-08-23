using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    GameProgressManager managerInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Dictionary<int,bool> levelCompleted = new Dictionary<int,bool>();

    private void Awake()
    {
        //making a singleton out of this manager
        if(managerInstance != null && managerInstance != this)
        {
            Destroy(gameObject);
        }
        managerInstance = this;
        //making this object dont destroy on load, otherwise player would not make progress while game running
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //adding 3 levels (for now)
        levelCompleted.Add(1, false);
        levelCompleted.Add(2, false);
        levelCompleted.Add(3, false);
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
}
