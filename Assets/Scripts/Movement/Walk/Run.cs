using System.Collections;
using UnityEngine;

public class Run : MonoBehaviour, IMovement
{
    int speed = 10;
    float runTimerDefaultValue = 2f;
    float characterRunTimer = 2f;

    bool isResettingTimer = false;

    /// <summary>
    /// moves in a direction with the speed in mind
    /// for running script, also checks if running is allowed
    /// </summary>
    public void Move(Vector2 direction)
    {
        //if the timer is above 0 
        if (characterRunTimer > 0)
        {
            //let the character run
            Vector3 actualMovement = direction * Time.deltaTime * speed;
            gameObject.transform.position += actualMovement;
            characterRunTimer -= Time.deltaTime;
        }
        else
        {
            if (isResettingTimer != true)
            {
                StartCoroutine(ResetTimer());
            }
        }

        IEnumerator ResetTimer()
        {
            isResettingTimer = true;
            yield return new WaitForSeconds(3);
            characterRunTimer = runTimerDefaultValue;
            isResettingTimer = false;
        }
    }
}
