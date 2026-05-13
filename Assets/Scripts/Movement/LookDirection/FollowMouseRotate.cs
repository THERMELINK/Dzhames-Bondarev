using Unity.Mathematics;
using UnityEngine;

public class FollowMouseRotate : MonoBehaviour, Ilookable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum mousePositionToPlayer
    {
        Left,
        Right,
    }
    GameObject thisPlayer;

    void Start()
    {
        thisPlayer = gameObject;
    }

    public Vector2 getMouseWorldSpacePosition() => Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
    /// <summary>
    /// this method keeps track of the mouse position according to the player position
    /// if the mouse is on the left of the player, the player should flip to face the left etc
    /// </summary>
    mousePositionToPlayer DecideMousePositionToPlayer()
    {
        float playerPositionX = thisPlayer.transform.position.x;
        float mousePointerPositionX = getMouseWorldSpacePosition().x;
        mousePositionToPlayer temp = mousePositionToPlayer.Right;

        float result = (playerPositionX - mousePointerPositionX);
        if (result >= 0)
        {
            temp = mousePositionToPlayer.Left;
        }
        else
        {
            temp = mousePositionToPlayer.Right;
        }
        return temp;
    }
    public void RotatePlayerToPosition()
    {
        mousePositionToPlayer temp = DecideMousePositionToPlayer();
        Vector2 currentScale = thisPlayer.transform.localScale;

        if (temp == mousePositionToPlayer.Left)
        {
            currentScale.x = -Mathf.Abs(currentScale.x);
        }
        else
        {
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        thisPlayer.transform.localScale = currentScale;
    }
}
