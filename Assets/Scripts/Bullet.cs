using UnityEngine;
using UnityEngine.SceneManagement;
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
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        Destroy(gameObject);
    }
}
