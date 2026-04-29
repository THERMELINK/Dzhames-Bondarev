using System.Collections;
using UnityEngine;

public class Jump : MonoBehaviour, Ijumpable
{
    int jumpPower = 5;
    bool canJump = true;
    bool collidedWithGround = true;
    public void JumpNow()
    {

        if (canJump == true && collidedWithGround == true)
        {
            print("jump");
            StartCoroutine(ActualJump());
        }
    }
    IEnumerator ActualJump()
    {
        Vector2 jumpVector = Vector2.up * jumpPower;
        gameObject.GetComponent<Rigidbody2D>().AddForce(jumpVector, ForceMode2D.Impulse);
        canJump = false;
        yield return new WaitForSeconds(1.5f);
        canJump = true;
        print("can jump again");
    }
}
