using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class EnemyInputManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameStateManager stateManager;
    GameObject player;
    [SerializeField] bool ActionsAllowed = true;
    bool changeTargetPosition = true;
    float minimalDistanceFromPlayer = 4f;
    float shootAtDistance = 12f;

    public Vector2 WalkInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool ShootPressed { get; private set; }
    public bool ReloadPressed { get; private set; }
    public Vector2 TargetPosition { get; private set; }
    private void Start()
    {
        stateManager = GameStateManager.instance;
        stateManager.canEnemyMove += ChangeEnemyMovementEnabled;
        player = stateManager.TellPlayerObject();
        TargetPosition = player.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (ActionsAllowed)
        {
            WalkInput = TellWalkDirection(player);
            ShootPressed = PressShoot();
            if(changeTargetPosition == true)
            {
                StartCoroutine(ChangeTargetPosDelay());
            }
        }
    }

    Vector2 TellWalkDirection(GameObject target)
    {
        Vector2 currentDirection = Vector2.zero;
        float distanceFromPlayer = TellDistanceFromPlayer(target);
        if (MathF.Abs(distanceFromPlayer) > minimalDistanceFromPlayer)
        {
            if (distanceFromPlayer <= 0)
            {
                currentDirection = Vector2.left;
            }
            else if (distanceFromPlayer > 0)
            {
                currentDirection = Vector2.right;
            }
        }
        return currentDirection;
    }
    bool PressShoot()
    {
        bool shootNow = false;
        OverrideTargetPosition(GameStateManager.instance.TellPlayerObject().transform.position);
        if(TellDistanceFromPlayer(player) < shootAtDistance)
        {
            shootNow = true;
        }
        return shootNow;
    }

    void OverrideTargetPosition(Vector2 playerPos)
    {
        TargetPosition = playerPos;
    }

    IEnumerator ChangeTargetPosDelay()
    {
        changeTargetPosition = false;
        TargetPosition = ChangeNormalTargetPosition();
        yield return new WaitForSeconds(1f);
        changeTargetPosition = true;
    }

    Vector2 ChangeNormalTargetPosition()
    {
        Vector2 thisEnemyPos = transform.position;
        Vector2 newTargetPos = new Vector2(thisEnemyPos.x - 2, thisEnemyPos.y);
        return newTargetPos;
    }

    float TellDistanceFromPlayer(GameObject player)
    {
        float distanceFromPlayer = (player.transform.position.x - transform.position.x);
        return distanceFromPlayer;
    }

    void ChangeEnemyMovementEnabled(bool b)
    {
        ActionsAllowed = b;
    }
}
