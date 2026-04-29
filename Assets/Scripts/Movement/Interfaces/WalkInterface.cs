using UnityEngine;

public interface IMovement
{
    void Move(Vector2 direction);
}

public interface Ijumpable
{
    void JumpNow();
}
