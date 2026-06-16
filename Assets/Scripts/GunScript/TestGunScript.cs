using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestGunScript : MonoBehaviour, Ishootable
{
    [SerializeField] GameObject gunOwner;
    [SerializeField] GameObject bulletToShoot;
    [SerializeField] GameObject bulletShootPoint;

    public float localXPosition = 0.3f;
    public float bulletspeed = 5f;
    public float shotTimer = 0.4f;
    public int shotsInMagazine = 20;
    public int shotsLeft = 20;
    bool canShoot = true;
    bool isReloading = false;

    void Start()
    {
        gameObject.transform.localPosition = new Vector3(localXPosition, 0.10f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gunOwner != null)
        {
            
        }
    }

    private void FixedUpdate()
    {
        Initialise();
    }

    public void ShootBullet(Vector2 actualTarget)
    {
        if (canShoot && shotsLeft > 0)
        {
            shotsLeft--;
            StartCoroutine(createBullet());
        }
        else if (shotsLeft <= 0)
        {
            //click sound idk
        }
    }

    public void ReloadMagazine()
    {
        if (isReloading == false)
        {
            StartCoroutine(reloadTimer(2));
        }
    }

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

    public void Initialise()
    {
        gunOwner = gameObject.transform.parent.gameObject;
    }

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

    IEnumerator reloadTimer(float timeToReload)
    {
        canShoot = false;
        isReloading = true;
        yield return new WaitForSeconds(timeToReload);
        shotsLeft = shotsInMagazine;
        canShoot = true;
        isReloading = false;
    }

    Vector2 GetMousePosition() => Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
}
