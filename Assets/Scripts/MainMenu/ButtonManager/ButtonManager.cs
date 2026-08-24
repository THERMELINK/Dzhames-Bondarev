using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject UIgroup1;
    [SerializeField] GameObject UIgroup2;
    [SerializeField] GameObject mapButtonGameObject;
    public int SelectedMap = 0;
    bool enableMapScreen = false;


    private void Start()
    {
        RefreshUIGroup();
        mapButtonGameObject.GetComponent<MapButtonStorage>().UpdateButtons();
        mapButtonGameObject.GetComponent<MapButtonStorage>().UpdateKillCountText();

    }
    public void SelectMap(MapNumberTeller map)
    {
        if (map.TellIfMapUnlocked())
        {
            SelectedMap = map.TellMapNumber();
            StartScene(map.TellSceneName());
        }
    }

    public void ContinueToMapScreen()
    {
        enableMapScreen = true;
        RefreshUIGroup();
        mapButtonGameObject.GetComponent<MapButtonStorage>().UpdateButtons();
        RefreshUIGroup();
    }
    public void Quit()
    {
        print("not yet");
    }

    public void GoBack()
    {
        enableMapScreen = false;
        RefreshUIGroup();
    }

    private void RefreshUIGroup()
    {
        UIgroup1.SetActive(!enableMapScreen);
        UIgroup2.SetActive(enableMapScreen);
    }

    void StartScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}

