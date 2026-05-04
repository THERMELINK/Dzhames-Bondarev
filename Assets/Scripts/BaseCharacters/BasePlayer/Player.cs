using UnityEngine;
[RequireComponent(typeof(PlayerInputManager))]
public class Player : MonoBehaviour
{
    PlayerInputManager inputManager;
    IMovement movement;
    Ijumpable jumpable;
    Ilookable lookable;
    Vector2 walkInput;
    private void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        movement = GetComponent<IMovement>();
        jumpable = GetComponent<Ijumpable>();
        lookable = GetComponent<Ilookable>();
    }
    void Update()
    {
        walkInput = inputManager.WalkInput;
        lookable?.RotatePlayerToPosition();
        if (inputManager.JumpPressed)
        {
            jumpable?.JumpNow();
        }
    }

    private void FixedUpdate()
    {
        movement?.Move(walkInput);
    }
}
