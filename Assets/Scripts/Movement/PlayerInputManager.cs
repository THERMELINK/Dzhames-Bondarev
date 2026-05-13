using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 WalkInput { get; private set; }
    public bool JumpPressed { get; private set; }

    public bool ShootPressed { get; private set; }

    private void Update()
    {
        WalkInput = new Vector2(Input.GetAxis("Horizontal"), 0);
        JumpPressed = Input.GetButton("Jump");
        ShootPressed = Input.GetButton("Fire");
    }
}
