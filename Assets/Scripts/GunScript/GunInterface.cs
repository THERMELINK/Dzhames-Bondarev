using UnityEngine;

public interface Ishootable
{
    void ShootBullet(Vector2 target);

    void ReloadMagazine();

    void LookAtTarget(Vector2 target);

    void CheckForParent();
}
