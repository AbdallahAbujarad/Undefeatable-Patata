using System.Collections;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    float speed = 3f;
    float rayDistance = 0.1f;
    public LayerMask wallLayer;
    public LayerMask playerLayer;
    private Rigidbody2D rb;
    private Vector2 direction = Vector2.left;
    bool allowMove = true;
    bool isGrounded = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (allowMove && isGrounded)
        {
            rotateWhenHitWall();
        }
    }
    private void FixedUpdate()
    {
        if (allowMove && isGrounded)
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        else if (!allowMove)
            rb.velocity = new Vector2(0, rb.velocity.y);
    }
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void rotateWhenHitWall()
    {
        Vector2 origin = transform.position + new Vector3(direction.x, 0, 0);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance, wallLayer);
        Debug.DrawRay(origin, direction * rayDistance, Color.red);
        if (hit.collider != null)
        {
            direction *= -1;
            Flip();
        }
    }
        void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;
    }
}