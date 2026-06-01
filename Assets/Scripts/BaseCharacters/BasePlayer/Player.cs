using UnityEngine;
[RequireComponent(typeof(PlayerInputManager))]
public class Player : MonoBehaviour
{
    PlayerInputManager inputManager;
    IMovement movement;
    Ijumpable jumpable;
    Ilookable lookable;
    Ishootable shootable;
    Vector2 walkInput;

    [SerializeField] GameObject equippedGun;
    private void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        movement = GetComponent<IMovement>();
        jumpable = GetComponent<Ijumpable>();
        lookable = GetComponent<Ilookable>();
        shootable = GetComponentInChildren<Ishootable>();
    }
    void Update()
    {
        walkInput = inputManager.WalkInput;
        lookable?.RotatePlayerToPosition();
        if (inputManager.JumpPressed)
        {
            jumpable?.JumpNow();
        }

        if (inputManager.ShootPressed)
        {
            if (equippedGun != null)
            {

                shootable = equippedGun.GetComponent<Ishootable>();
                print("shootInput");
                shootable?.ShootBullet();
            }
        }

        if(inputManager.ReloadPressed)
        {
            shootable.ReloadMagazine();
        }
    }
    //-10 > 40

    private void FixedUpdate()
    {
        movement?.Move(walkInput);
    }
}
