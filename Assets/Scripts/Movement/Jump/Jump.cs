using System.Collections;
using UnityEngine;

public class Jump : MonoBehaviour, Ijumpable
{
    int jumpPower = 5;
    bool canJump = true;
    bool collidedWithGround = true;

    /// <summary>
    /// interface member
    /// lets the gameobject its on jump
    /// </summary>
    public void JumpNow()
    {
        if (canJump == true && collidedWithGround == true)
        {
            StartCoroutine(ActualJump());
        }
    }

    /// <summary>
    /// this method lets the gameobject jump, but checks if its collided with the ground, and if the timer for canjump is over
    /// </summary>
    IEnumerator ActualJump()
    {
        collidedWithGround = false;
        Vector2 jumpVector = Vector2.up * jumpPower;
        gameObject.GetComponent<Rigidbody2D>().AddForce(jumpVector, ForceMode2D.Impulse);
        canJump = false;
        yield return new WaitForSeconds(1.5f);
        canJump = true;
    }

    //checks for collission with ground, or enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collidedWithGround = true;
    }
}
