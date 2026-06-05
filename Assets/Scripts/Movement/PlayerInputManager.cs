using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 WalkInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool ShootPressed { get; private set; }
    public bool ReloadPressed { get; private set; }

    public bool DropGunPressed { get; private set; }
    public bool Interact { get; private set; }
    public float Scroll { get; private set; }

    GameStateManager stateManager;
    [SerializeField] bool ActionsAllowed = true;

    private void Awake()
    {
        stateManager = GameStateManager.instance;
        stateManager.CanPlayerMove += ChangePlayerMovementEnabled;
    }

    private void Update()
    {
        if (ActionsAllowed)
        {
            WalkInput = new Vector2(Input.GetAxis("Horizontal"), 0);
            JumpPressed = Input.GetButton("Jump");
            ShootPressed = Input.GetButton("Fire");
            ReloadPressed = Input.GetButton("Reload");
            DropGunPressed = Input.GetButton("DropGun");
            Interact = Input.GetButton("Interact");
            Scroll = Input.GetAxis("Scroll");
        }
    }

    void ChangePlayerMovementEnabled(bool b)
    {
        print("recieved" + b);
        ActionsAllowed = b;
    }
}
