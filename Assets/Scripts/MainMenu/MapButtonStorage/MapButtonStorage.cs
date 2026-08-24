using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonStorage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] List<Button> mapbuttons = new List<Button>();

    public List<Button> TellMapButtons()
    {
        return mapbuttons;
    }
}
