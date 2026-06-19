using System.Collections;
using UnityEngine;

public class DoubleJump : MonoBehaviour, Ijumpable
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
            print("jump");
            StartCoroutine(ActualJump());
        }
    }

    /// <summary>
    /// this method is the same as the normal jump, except for now it activates another jump after a certain amount of time
    /// </summary>
    IEnumerator ActualJump()
    {
        collidedWithGround = false;
        Vector2 jumpVector = Vector2.up * jumpPower;
        gameObject.GetComponent<Rigidbody2D>().AddForce(jumpVector, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Rigidbody2D>().AddForce(jumpVector, ForceMode2D.Impulse);
        canJump = false;
        yield return new WaitForSeconds(1.5f);
        canJump = true;
        print("can jump again");
    }

    //checks for collission with ground, enemy or whatever it can find
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collidedWithGround = true;
    }
}
