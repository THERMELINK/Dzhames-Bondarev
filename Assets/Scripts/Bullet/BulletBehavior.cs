using System.Collections;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public Vector2 thisDirection = Vector2.zero;
    public int thisBulletSpeed = 0;
    public int thisBulletDamage = 20;
    public bool isInitialized = false;
    Health detectedHealth;

    // Update is called once per frame
    void Update()
    {
        if (isInitialized)
        {
            //fix this
            transform.Translate((thisDirection * thisBulletSpeed) * Time.deltaTime); 
        }
    }

    public void initialiseBullet(Vector2 direction, int bulletSpeed)
    {
        thisDirection = direction;
        thisBulletSpeed = bulletSpeed;
        StartCoroutine(BulletDestroyTimer(3f));
        isInitialized = true;
    }

    IEnumerator BulletDestroyTimer(float amountOfTimeActive)
    {
        yield return new WaitForSeconds(amountOfTimeActive);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Health>() != null)
        {
            print("hit detected");
            detectedHealth = collision.gameObject.GetComponent<Health>();
            detectedHealth?.RemoveHealth(thisBulletDamage);
            Destroy(gameObject); 
        }
    }
}
