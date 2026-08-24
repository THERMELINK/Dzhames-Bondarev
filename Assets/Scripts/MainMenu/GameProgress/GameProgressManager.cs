using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Dictionary<int, bool> levelCompleted = new Dictionary<int, bool>();

    private void Awake()
    {
        //making a singleton out of this manager
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        //making this object dont destroy on load, otherwise player would not make progress while game running
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //adding 3 levels (for now)
        levelCompleted.Add(0, true);
        levelCompleted.Add(1, false);
        levelCompleted.Add(2, false);
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

    public void CheckCompletedLevels()
    {
        MapButtonStorage mapButtonstorage = FindFirstObjectByType<MapButtonStorage>();
        List<Button> passedButtons = mapButtonstorage.TellMapButtons();
        for (int i = 0; i < levelCompleted.Count; i++)
        {
            print("mapnumber" + i);
            if (levelCompleted[i] == true)
            {
                passedButtons[i].image.color = Color.green;
            }
            else
            {
                passedButtons[i].image.color = Color.darkRed;
            }
        }
    }


}
