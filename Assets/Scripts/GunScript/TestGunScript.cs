using Unity.VisualScripting;
using UnityEngine;

public class TestGunScript : MonoBehaviour, Ishootable
{
    [SerializeField] GameObject gunOwner;
    public float localXScale = -0.3f;
    void Start()
    {
        gameObject.transform.localPosition = new Vector3(localXScale,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gunOwner != null)
        {
            LookAtTarget();
        }
    }

    private void FixedUpdate()
    {
        Initialise();
    }

    public void ShootBullet()
    {
        print("shootOutput");
    }

    public void LookAtTarget()
    {
        Vector2 from = gunOwner.transform.position;
        Vector2 to = GetMousePosition();
        Vector2 direction = to-from;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Initialise()
    {
        gunOwner = gameObject.transform.parent.gameObject;
    }

    Vector2 GetMousePosition() => Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
}
