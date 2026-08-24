using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonStorage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] List<Button> mapbuttons = new List<Button>();
    [SerializeField] TMP_Text killcountText;

    public void UpdateButtons()
    {
        Dictionary<int, bool> passedDictionary = GameProgressManager.instance.TellWhichLevelsCompleted();
        for (int i = 0; i < passedDictionary.Count; i++)
        {
            print("mapnumber" + i);
            if (passedDictionary[i] == true)
            {
                mapbuttons[i].image.color = Color.green;
            }
            else
            {
                mapbuttons[i].image.color = Color.darkRed;
            }
        }
    }

    public void UpdateKillCountText()
    {
        int amount = GameProgressManager.instance.TellKillCount();
        killcountText.text = "Bondarevs killcount:" + amount.ToString();
    }
}
