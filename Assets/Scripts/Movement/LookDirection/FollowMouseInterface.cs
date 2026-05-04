using UnityEngine;
using static FollowMouseRotate;

public interface Ilookable
{
    Vector2 getMouseWorldSpacePosition();

    void RotatePlayerToPosition();
}