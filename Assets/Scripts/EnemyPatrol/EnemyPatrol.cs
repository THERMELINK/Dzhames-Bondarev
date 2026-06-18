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
        lockedOnCurrentPoint =new Vector2(patrolPointA,transform.position.y);
    }

    public void HandlePatrol()
    {
        if (walkingToPoint == false)
        {
            StartCoroutine(WaitOnPoint(1));
        }
        Vector2 direction = (lockedOnCurrentPoint - (Vector2)transform.position).normalized;
        GetComponent<Ilookable>()?.RotatePlayerToPosition(lockedOnCurrentPoint);
        GetComponentInChildren<Ishootable>()?.LookAtTarget(lockedOnCurrentPoint);
        GetComponent<Enemy>().WalkToPosition(direction, 0.3f);
    }


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
