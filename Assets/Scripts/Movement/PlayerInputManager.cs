using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public bool CheckJumpInput()
    {
        bool jumpInput = Input.GetButton("Jump");
        return jumpInput;
    }

    public Vector2 CheckWalkInput()
    {
        Vector2 walkInput = new Vector2(Input.GetAxis("Horizontal"), 0);
        return walkInput;
    }
}
