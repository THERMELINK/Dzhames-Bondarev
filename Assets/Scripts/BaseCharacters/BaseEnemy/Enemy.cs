using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;
[RequireComponent(typeof(EnemyInputManager))]
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// the enemy has 3 states, inactive, patrolling and aggressive
    /// </summary>
    enum enemyStates
    {
        Inactive,
        Patrolling,
        Aggro
    }

    [SerializeField] enemyStates currentState = enemyStates.Inactive;
    [SerializeField] GameObject equippedGun;

    //interfaces on enemy
    enemyStates previousState;
    IMovement movement;
    Ijumpable jumpable;
    Ilookable lookable;
    Ishootable shootable;
    Ipatrol patrol;

    //vector to keep track of the walk input
    Vector2 walkInput = Vector2.zero;

    //vector to keep track of patrol points and current patrol point
    Vector2 patrolPointA;
    Vector2 patrolPointB;
    Vector2 lockedOnCurrentPoint;

    EnemyInputManager inputManager;

    bool isWalking = false;
    bool waitingForJump = false;



    void Start()
    {
        inputManager = GetComponent<EnemyInputManager>();
        shootable = GetComponentInChildren<Ishootable>();
        movement = GetComponent<IMovement>();
        jumpable = GetComponent<Ijumpable>();
        lookable = GetComponent<Ilookable>();
        patrol = GetComponent<Ipatrol>();
        jumpable = GetComponent<Ijumpable>();

        //both patrol points are startposition - or + 2 (left and right from origin)
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


    //handles an action based on the current state
    void HandleStateManager()
    {
        switch (currentState)
        {
            case enemyStates.Inactive:
                break;
            case enemyStates.Patrolling:
                patrol?.HandlePatrol();
                //adds a jump in the patrol because they feel like it
                if (waitingForJump == false)
                {
                    StartCoroutine(RandomJumpTimer());
                }
                break;
            case enemyStates.Aggro:
                HandleAggro();
                break;
        }
    }

    /// <summary>
    /// handles the aggressive fase of the enemy
    /// basically it looks at the player and shoots its equipped gun
    /// </summary>
    void HandleAggro()
    {
        lookable?.RotatePlayerToPosition(inputManager.TargetPosition);
        shootable?.LookAtTarget(inputManager.TargetPosition);
        if (inputManager.ShootPressed)
        {
            shootable?.ShootBullet(inputManager.TargetPosition);
        }
    }


    /// <summary>
    /// activates the jump Ienumerator 
    /// </summary>
    IEnumerator RandomJumpTimer()
    {
        waitingForJump = true;
        //interchangeable during runtime
        GetComponent<Ijumpable>()?.JumpNow();
        print(GetComponent<Ijumpable>());
        print("ememyJump");
        yield return new WaitForSeconds(1);
        waitingForJump = false;
    }

    /// <summary>
    /// walks to a position, but with a small delay after getting to the point
    /// currently does not function well, so removed it from aggro behavior
    /// </summary>
    IEnumerator WalkToPositionWithDelay(Vector2 thisWalkInput, float movementSpeed)
    {
        isWalking = true;
        float timer = 1f;
        while (timer > 0)
        {
            movement?.Move(thisWalkInput * movementSpeed);
            timer -= Time.deltaTime;
            yield return null; //apparently this waits just for 1 frame
        }
        yield return new WaitForSeconds(2);
        isWalking = false;
    }

    /// <summary>
    /// walks to a position according to an input and movement speed
    /// </summary>
    public void WalkToPosition(Vector2 thisWalkInput, float movementSpeed)
    {
        movement?.Move(thisWalkInput * movementSpeed);
    }
}
