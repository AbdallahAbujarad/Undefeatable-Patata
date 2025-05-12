using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && bulletPrefab != null)
        {
            Instantiate(bulletPrefab,transform.position,transform.rotation);
        }
    }
}
