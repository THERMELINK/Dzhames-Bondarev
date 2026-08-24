using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ExtractPointManager : MonoBehaviour
{
    public float extractTimer = 2f;
    [SerializeField] bool timerStarted = false;
    [SerializeField] int mapNumber;


    // Update is called once per frame
    void Update()
    {
        if (timerStarted && extractTimer > 0)
        {
            extractTimer -= Time.deltaTime;
        }
        else if (extractTimer < 0 && timerStarted)
        {
            timerStarted = false;
            print("extracted");
            GameStateManager.instance.WinPlayer();
            GameProgressManager.instance.CompleteLevel(mapNumber);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject playerObject = GameStateManager.instance.TellPlayerObject();
        if (collision.gameObject == playerObject)
        {
            if (timerStarted != true)
            {
                StartTimer(extractTimer);
            }
        }
    }

    void StartTimer(float time)
    {
        timerStarted = true;
    }
}
