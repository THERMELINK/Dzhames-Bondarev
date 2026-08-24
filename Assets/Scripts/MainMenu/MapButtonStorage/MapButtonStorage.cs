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
            //if the current map is completed
            if (passedDictionary[i] == true)
            {
                //umlock the next map
                if (i < mapbuttons.Count)
                {
                    mapbuttons[i + 1].GetComponent<MapNumberTeller>().UnlockMap();
                }
                mapbuttons[i].image.color = Color.green;
            }
            else
            {
                if (i > 0 && passedDictionary[i - 1] == true)
                {
                    mapbuttons[i].image.color = Color.white;
                }
                else
                {
                    mapbuttons[i].image.color = Color.darkRed;
                }
            }

            //force tutorial to be open
            if (i == 0)
            {
                mapbuttons[i].GetComponent<MapNumberTeller>().UnlockMap();
                mapbuttons[i].image.color = Color.green;
            }
        }
    }

    public void UpdateKillCountText()
    {
        int amount = GameProgressManager.instance.TellKillCount();
        killcountText.text = "Bondarevs killcount:" + amount.ToString();
    }
}
