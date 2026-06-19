using System.Collections;
using UnityEngine;

public class BurstFireGun : MonoBehaviour, Ishootable
{
    [SerializeField] GameObject gunOwner;
    [SerializeField] GameObject bulletToShoot;
    [SerializeField] GameObject bulletShootPoint;

    float localXPosition = 0.3f;
    float bulletspeed = 5f;
    float shotTimer = 0.4f;
    int shotsInMagazine = 21;
    int shotsLeft = 20;
    bool canShoot = true;
    bool cooldown = false;
    bool isReloading = false;

    void Start()
    {
        gameObject.transform.localPosition = new Vector3(localXPosition, 0.10f, 0);
    }

    private void FixedUpdate()
    {
        CheckForParent();
    }

    /// <summary>
    /// shoots shoots the gun item
    /// this gun shoots 3 bullet burst, with each bullet having a delay of 0.3 seconds
    /// </summary>
    public void ShootBullet(Vector2 actualTarget)
    {
        if (canShoot && shotsLeft > 0)
        {
            if (cooldown == false)
            {
                StartCoroutine(ShootBullets(3, 0.3f));
            }
        }
        else if (shotsLeft <= 0)
        {
            //click sound idk
        }
    }

    /// <summary>
    /// shoots a series of bullets (burst)
    /// there is a check if there is still enough bullets in the magazine 
    /// </summary>
    IEnumerator ShootBullets(int amount, float delayPerShot)
    {
        cooldown = true;
        for (int i = 0; i < amount; i++)
        {
            if (shotsLeft > 0)
            {
                shotsLeft--;
                StartCoroutine(createBullet());
                yield return new WaitForSeconds(delayPerShot);
            }
        }
        yield return new WaitForSeconds(1.5f);
        cooldown = false;
    }


    /// <summary>
    /// this method starts the reloading timer 
    /// </summary>
    public void ReloadMagazine()
    {
        if (isReloading == false)
        {
            StartCoroutine(reloadTimer(2));
        }
    }

    /// <summary>
    /// this method rotates the gun in a certain direction
    /// </summary>
    public void LookAtTarget(Vector2 target)
    {
        //set Y axis to -1
        float localYposition = 1f;
        Vector2 from = gunOwner.transform.position;
        Vector2 to = target;
        Vector2 direction = to - from;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

        //check if parent sprite if flipped
        if (gameObject.transform.parent.GetComponent<SpriteRenderer>().flipX == true)
        {
            localXPosition = -Mathf.Abs(localXPosition);
            localYposition = -Mathf.Abs(localYposition);
        }
        else
        {
            localXPosition = Mathf.Abs(localXPosition);
            localYposition = Mathf.Abs(localYposition);
        }
        gameObject.transform.localPosition = new Vector3(localXPosition, 0.10f, 0);
        gameObject.transform.localScale = new Vector3(1, localYposition, 1);

        //bulletShootPoint.transform.localPosition = new Vector3(localXScale, 0, 0);
    }

    /// <summary>
    /// checks the parent of this gun, and sets it as the owner
    /// </summary>
    public void CheckForParent()
    {
        gunOwner = gameObject.transform.parent.gameObject;
    }


    /// <summary>
    /// this method creates a single bullet, sets its position to the bullet position (differentiates per gun) and initialises the bullet with a velocity
    /// </summary>
    IEnumerator createBullet()
    {
        canShoot = false;
        GameObject newBullet = Instantiate(bulletToShoot);
        newBullet.transform.position = bulletShootPoint.transform.position;
        newBullet.GetComponent<BulletBehavior>().initialiseBullet(transform.right, 5);
        //set speed + direction
        yield return new WaitForSeconds(shotTimer);
        canShoot = true;
    }

    /// <summary>
    /// this method resets the shots in the magazine after a delay
    /// </summary>
    IEnumerator reloadTimer(float timeToReload)
    {
        canShoot = false;
        isReloading = true;
        yield return new WaitForSeconds(timeToReload);
        shotsLeft = shotsInMagazine;
        canShoot = true;
        isReloading = false;
    }
}
