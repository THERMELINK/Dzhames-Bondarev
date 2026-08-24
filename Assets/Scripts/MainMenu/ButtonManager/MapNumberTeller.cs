using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapNumberTeller : MonoBehaviour
{
    [SerializeField] int iAmMapNumber;
    [SerializeField] string sceneName;
    [SerializeField] bool IsUnlocked = false;

    public int TellMapNumber()
    {
        return iAmMapNumber;
    }
    public string TellSceneName()
    {
        return sceneName;
    }

    public bool TellIfMapUnlocked() => IsUnlocked;

    public void UnlockMap()
    {
        IsUnlocked = true;
    }
}

