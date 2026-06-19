using Unity.Mathematics;
using UnityEngine;

public class FollowMouseRotate : MonoBehaviour, Ilookable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //possible positions for the mouse from the player
    enum deltaPositionFromCharacter
    {
        Left,
        Right,
    }
    GameObject thisPlayer;

    void Start()
    {
        thisPlayer = gameObject;
    }

    /// <summary>
    /// gets the mouse position
    /// </summary>
    /// <returns></returns>
    public Vector2 getMouseWorldSpacePosition() => Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
    /// <summary>
    /// this method keeps track of the mouse position according to the player position
    /// if the mouse is on the left of the player, the player should flip to face the left etc
    /// </summary>
    deltaPositionFromCharacter DecideMousePositionToPlayer(Vector2 position)
    {
        float playerPositionX = thisPlayer.transform.position.x;
        float mousePointerPositionX = position.x;
        deltaPositionFromCharacter temp = deltaPositionFromCharacter.Right;

        float result = (playerPositionX - mousePointerPositionX);
        if (result >= 0)
        {
            temp = deltaPositionFromCharacter.Left;
        }
        else
        {
            temp = deltaPositionFromCharacter.Right;
        }
        return temp;
    }

    /// <summary>
    /// changes the player sprite X scale so it faces a certain position 
    /// for example, a mouse position
    /// </summary>
    public void RotatePlayerToPosition(Vector2 position)
    {
        deltaPositionFromCharacter temp = DecideMousePositionToPlayer(position);
        Vector2 currentScale = thisPlayer.transform.localScale;
        SpriteRenderer renderer = thisPlayer.GetComponent<SpriteRenderer>();

        if (temp == deltaPositionFromCharacter.Left)
        {
            renderer.flipX = true;
        }
        else
        {
            renderer.flipX = false;
        }
        //thisPlayer.transform.localScale = currentScale;
    }
}
