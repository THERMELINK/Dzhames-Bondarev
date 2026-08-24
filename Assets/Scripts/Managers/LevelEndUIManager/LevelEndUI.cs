using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] string mainMenuName = "MainMenu";

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
    }
}
