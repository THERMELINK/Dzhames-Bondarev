using UnityEngine;
[RequireComponent(typeof(EnemyInputManager))]
public class Enemy : MonoBehaviour
{

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
    void Start()
    {
        inputManager = GetComponent<EnemyInputManager>();
        shootable = GetComponentInChildren<Ishootable>();
        movement = GetComponent<IMovement>();
        jumpable = GetComponent<Ijumpable>();
        lookable = GetComponent<Ilookable>();
    }

    // Update is called once per frame
    void Update()
    {
        walkInput = inputManager.WalkInput;
    }
    private void FixedUpdate()
    {
        lookable?.RotatePlayerToPosition(inputManager.TargetPosition);
        shootable?.LookAtTarget(inputManager.TargetPosition);
        print(walkInput);
        movement?.Move(walkInput);
        if (inputManager.ShootPressed)
        {

            shootable?.ShootBullet(inputManager.TargetPosition);
            print("i want to shoot :((((");
        }
    }
}
