using System.Collections;
using UnityEngine;

public class EnemyGunHolder : MonoBehaviour
{
    public LayerMask playerLayer;
    public LayerMask wallLayer;
    public float shootInterval = 0.3f;
    public float rayLength = 100f;
    public Transform firePoint; // نقطة الإطلاق
    public GameObject bulletPrefab; // Prefab الرصاصة

    private bool isShooting = false;

    void Update()
    {
        DetectAndShoot();
    }

    void DetectAndShoot()
    {
        Vector2 direction = -transform.right; // ← لأنك تطلق باتجاه اليسار
        RaycastHit2D[] hits = Physics2D.RaycastAll(firePoint.position, direction, rayLength, wallLayer | playerLayer);
        Debug.DrawRay(firePoint.position, direction * rayLength, Color.red);

        foreach (var hit in hits)
        {
            if (((1 << hit.collider.gameObject.layer) & wallLayer) != 0)
            {
                return; // وُجد جدار بين العدو واللاعب
            }

            if (((1 << hit.collider.gameObject.layer) & playerLayer) != 0)
            {
                if (!isShooting)
                    StartCoroutine(ShootRepeatedly(direction));
                return;
            }
        }

        isShooting = false;
        StopAllCoroutines();
    }

    IEnumerator ShootRepeatedly(Vector2 direction)
    {
        isShooting = true;

        while (true)
        {
            ShootBullet(direction);
            yield return new WaitForSeconds(shootInterval);
        }
    }

    void ShootBullet(Vector2 direction)
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }
}