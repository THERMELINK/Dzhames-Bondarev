using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;
[RequireComponent(typeof(EnemyInputManager))]
public class Enemy : MonoBehaviour
{
    enum enemyStates
    {
        Inactive,
        Patrolling,
        Aggro
    }

    [SerializeField] enemyStates currentState = enemyStates.Inactive;
    enemyStates previousState;

    //enemyInputManager

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    IMovement movement;
    Ijumpable jumpable;
    //rotates the playersprite!!
    Ilookable lookable;
    Ishootable shootable;
    Vector2 walkInput = Vector2.zero;
    [SerializeField] GameObject equippedGun;
    EnemyInputManager inputManager;
    [SerializeField] Vector2 testInput;
    bool isWalking = false;
    Vector2 patrolPointA;
    Vector2 patrolPointB;
    Vector2 lockedOnCurrentPoint;



    void Start()
    {
        inputManager = GetComponent<EnemyInputManager>();
        shootable = GetComponentInChildren<Ishootable>();
        movement = GetComponent<IMovement>();
        jumpable = GetComponent<Ijumpable>();
        lookable = GetComponent<Ilookable>();

        float offset = 2;
        Vector2 startPosition = transform.position;
        patrolPointA = new Vector2(startPosition.x - offset, startPosition.y);
        patrolPointB = new Vector2(startPosition.x + offset, startPosition.y);
        previousState = currentState;
    }

    // Update is called once per frame
    void Update()
    {
        walkInput = inputManager.WalkInput;
    }
    private void FixedUpdate()
    {
        HandleStateManager();
    }

    void HandleStateManager()
    {
        switch (currentState)
        {
            case enemyStates.Inactive:
                break;
            case enemyStates.Patrolling:
                HandlePatrol();
                break;
            case enemyStates.Aggro:
                HandleAggro();
                break;
        }
    }

    void HandlePatrol()
    {
        if (Vector2.Distance(transform.position, lockedOnCurrentPoint) < 0.1f)
        {
            if (lockedOnCurrentPoint == patrolPointA)
                lockedOnCurrentPoint = patrolPointB;
            else
                lockedOnCurrentPoint = patrolPointA;
        }
        Vector2 direction = (lockedOnCurrentPoint - (Vector2)transform.position).normalized;
        print(direction);
        print(lockedOnCurrentPoint);
        WalkToPosition(direction, 0.6f);
    }

    void HandleAggro()
    {
        //walk towards the player and shoot in the meantime
        lookable?.RotatePlayerToPosition(inputManager.TargetPosition);
        shootable?.LookAtTarget(inputManager.TargetPosition);
        if (inputManager.ShootPressed)
        {
            shootable?.ShootBullet(inputManager.TargetPosition);
        }
        if (isWalking == false)
        {
            StartCoroutine(WalkToPositionWithDelay(inputManager.TargetPosition, 0.3f));
        }
    }

    IEnumerator WalkToPositionWithDelay(Vector2 thisWalkInput, float movementSpeed)
    {
        isWalking = true;
        float timer = 1f;
        if (timer > 0)
        {
            movement?.Move(thisWalkInput * movementSpeed);
            timer -= Time.deltaTime;
            yield return null; //apparently this waits just for 1 frame
        }
        //yield return new WaitForSeconds(2);
        isWalking = false;
    }
    void WalkToPosition(Vector2 thisWalkInput, float movementSpeed)
    {
        movement?.Move(thisWalkInput * movementSpeed);
    }
}
