using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject UIgroup1;
    [SerializeField] GameObject UIgroup2;
    public int SelectedMap = 0;
    bool enableMapScreen = false;


    private void Start()
    {
        RefreshUIGroup();
    }
    public void SelectMap(MapNumberTeller map)
    {
        SelectedMap = map.TellMapNumber();
        print(SelectedMap);
    }

    public void ContinueToMapScreen()
    {
        enableMapScreen = true;
        RefreshUIGroup();
    }
    public void Quit()
    {
        print("not yet");
    }

    public void GoBack()
    {
        enableMapScreen=false;
        RefreshUIGroup();
    }

    private void RefreshUIGroup()
    {
        UIgroup1.SetActive(!enableMapScreen);
        UIgroup2.SetActive(enableMapScreen);
    }
}

