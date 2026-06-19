using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour, Ipatrol
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    float patrolPointA;
    float patrolPointB;
    Vector2 lockedOnCurrentPoint;
    bool walkingToPoint = false;

    void Start()
    {
        float offset = 2;
        Vector2 startPosition = transform.position;
        patrolPointA = (startPosition.x - offset);
        patrolPointB = (startPosition.x + offset);
        lockedOnCurrentPoint = new Vector2(patrolPointA, transform.position.y);
    }

    /// <summary>
    /// handles the patrol phase from the enemy
    /// </summary>
    public void HandlePatrol()
    {
        //if it is not walking to a point yet
        if (walkingToPoint == false)
        {
            //walk to a point, this takes X amount of time
            StartCoroutine(WaitOnPoint(1));
        }

        //calculates the direction
        Vector2 direction = (lockedOnCurrentPoint - (Vector2)transform.position).normalized;
        //rotates player in that direction
        GetComponent<Ilookable>()?.RotatePlayerToPosition(lockedOnCurrentPoint);
        //rotates gun in that direction
        GetComponentInChildren<Ishootable>()?.LookAtTarget(lockedOnCurrentPoint);
        GetComponent<Enemy>().WalkToPosition(direction, 0.3f);
    }

    /// <summary>
    /// waits on a point and switches to the next patrol point
    /// </summary>
    public IEnumerator WaitOnPoint(float seconds)
    {
        walkingToPoint = true;
        if (lockedOnCurrentPoint.x == patrolPointA)
        {
            lockedOnCurrentPoint.x = patrolPointB;
        }
        else
        {
            lockedOnCurrentPoint.x = patrolPointA;
        }
        yield return new WaitForSeconds(seconds);
        walkingToPoint = false;
    }
}
