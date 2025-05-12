using UnityEngine;
public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    float xForce = 10; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * xForce,ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
