using System.Collections;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    Vector2 thisDirection = Vector2.zero;
    int thisBulletSpeed = 0;
    int thisBulletDamage = 20;
    bool isInitialized = false;
    Health detectedHealth;

    // Update is called once per frame
    void Update()
    {
        //if the bullet is initialized
        if (isInitialized)
        {
            MoveBullet();
        }
    }

    void MoveBullet()
    { 
        //move to a certain direction with the bulletspeed in mind
        transform.Translate((thisDirection * thisBulletSpeed) * Time.deltaTime);
    }

    //initialises itself after being shot from a gun
    public void initialiseBullet(Vector2 direction, int bulletSpeed)
    {
        thisDirection = direction;
        thisBulletSpeed = bulletSpeed;
        StartCoroutine(BulletDestroyTimer(3f));
        isInitialized = true;
    }

    /// <summary>
    /// after a certain amount of time, the bullet destroys itself
    /// </summary>
    IEnumerator BulletDestroyTimer(float amountOfTimeActive)
    {
        yield return new WaitForSeconds(amountOfTimeActive);
        Destroy(gameObject);
    }


    /// <summary>
    /// after detecting a trigger, it tries to find the health interface and removes health
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            print("hit detected");
            detectedHealth = collision.gameObject.GetComponent<Health>();
            detectedHealth?.RemoveHealth(thisBulletDamage);
            Destroy(gameObject);
        }
    }
}
