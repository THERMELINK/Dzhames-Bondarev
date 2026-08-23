using UnityEngine;

public class MapNumberTeller : MonoBehaviour
{
    [SerializeField] int iAmMapNumber;

    public int TellMapNumber()
    {
        return iAmMapNumber;
    }
}

