using System.Collections;
using UnityEngine;

public class DashEnemy : MonoBehaviour
{
    float speed = 3f;
    float rayDistance = 0.1f;
    float checkEnemyRayLength = 3;
    public float dashPowerX = 300f;
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
        JumpOnPlayer();
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
        Vector2 origin = transform.position + new Vector3(direction.x,0,0) ;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance, wallLayer);
        Debug.DrawRay(origin, direction * rayDistance, Color.red);
        if (hit.collider != null)
        {
            direction *= -1;
            Flip();
        }
    }
    void JumpOnPlayer()
    {
        Vector3 origin = transform.position;
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, checkEnemyRayLength, wallLayer | playerLayer);
        Debug.DrawRay(origin, direction * checkEnemyRayLength, Color.blue);
        foreach (var hit in hits)
        {
            if (((1 << hit.collider.gameObject.layer) & wallLayer) != 0)
            {
                return;
            }
            if (((1 << hit.collider.gameObject.layer) & playerLayer) != 0)
            {
                allowMove = false;
                StartCoroutine(WaitBeforeDash());
                return;
            }
        }
    }
    IEnumerator WaitBeforeDash()
    {
        while (true)
        {
            Debug.Log("wait before dash");
            yield return new WaitForSeconds(0.5f);
            Debug.Log("waiting is done");
            StartCoroutine(DashNow());
            yield break;
        }
    }
    IEnumerator DashNow()
    {
        Debug.Log("DashNow");
        if (isGrounded)
        {
            rb.AddForce(new Vector2(direction.x * dashPowerX, 0), ForceMode2D.Impulse);
        }
        allowMove = true;
        yield return null;
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