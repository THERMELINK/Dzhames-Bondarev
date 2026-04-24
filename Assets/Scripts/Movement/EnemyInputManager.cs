using System;
using UnityEngine;

//needs basic walk and basic Jump
public class EnemyInputManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckAddedScripts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckAddedScripts()
    {
        if (gameObject.GetComponent<Walk>() == null)
        {
            gameObject.AddComponent<Walk>();
        }
        if (gameObject.GetComponent<Jump>() == null)
        {
            gameObject.AddComponent<Jump>();
        }
    }
}
