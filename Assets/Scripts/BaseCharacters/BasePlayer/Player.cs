using UnityEngine;
[RequireComponent(typeof(PlayerInputManager))]
public class Player : MonoBehaviour
{
    PlayerInputManager inputManager;
    IMovement movement;
    Ijumpable jumpable;
    Vector2 walkInput;
    private void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        movement = GetComponent<IMovement>();
        jumpable = GetComponent<Ijumpable>();
    }
    void Update()
    {
        walkInput = inputManager.WalkInput;
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
