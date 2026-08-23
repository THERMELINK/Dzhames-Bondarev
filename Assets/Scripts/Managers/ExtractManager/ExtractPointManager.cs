using UnityEngine;

public class ExtractPointManager : MonoBehaviour
{
    public float extractTimer = 2f;
    bool timerStarted = false;

    // Update is called once per frame
    void Update()
    {
        if (timerStarted && extractTimer > 0)
        {
            extractTimer -= Time.deltaTime;
        }
        else if(extractTimer < 0)
        {
            timerStarted = false;
            print("extracted");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject playerObject = GameStateManager.instance.TellPlayerObject();
        if (collision.gameObject == playerObject)
        {
            if (timerStarted == true)
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
