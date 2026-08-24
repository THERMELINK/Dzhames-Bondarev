using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapNumberTeller : MonoBehaviour
{
    [SerializeField] int iAmMapNumber;
    [SerializeField] string sceneName;

    public int TellMapNumber()
    {
        return iAmMapNumber;
    }
    public string TellSceneName()
    {
        return sceneName;
    }
}

