using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public GameObject gunHolder;
    Rigidbody2D rb;
    public float moveSpeed = 3;
    public float jumpPower = 5;
    public float dashPower = 10;
    public float dashTime = 0.1f;
    public float dashCoolDownTime = 0.5f;
    public float slideFactor = 0.1f;
    int jumpCount = 1;
    int maxJumpAllowed = 2;
    int direction = 1;
    bool allowMove = true;
    bool allowJump = true;
    bool allowDash = true;
    public static bool jumpUnlocked;
    public static bool dashUnlocked;
    public static bool gunUnlocked;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt(LevelsManager.jumpPrefsString) == 1)
        {
            jumpUnlocked = true;
        }
        else
        {
            jumpUnlocked = false;
        }

        if (PlayerPrefs.GetInt(LevelsManager.dashPrefsString) == 1)
        {
            dashUnlocked = true;
        }
        else
        {
            dashUnlocked = false;
        }
        if (PlayerPrefs.GetInt(LevelsManager.gunPrefsString) == 1)
        {
            gunUnlocked = true;
        }
        else
        {
            gunUnlocked = false;
        }
        if (gunUnlocked)
        {
            gunHolder.SetActive(true);
        }
        else
        {
            gunHolder.SetActive(false);
        }
        if (allowJump && jumpUnlocked)
        {
            if (Input.GetKeyDown(KeyCode.W) && jumpCount <= maxJumpAllowed)
            {
                rb.velocity = new Vector2(0, jumpPower);
                jumpCount++;
            }
        }
        if (Input.GetKeyDown(KeyCode.C) && allowDash && dashUnlocked)
        {
            StartCoroutine(Dash());
        }
    }
    private void FixedUpdate()
    {
        if (allowMove)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                direction = -1;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x * slideFactor, rb.velocity.y);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 1;
        }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bullet")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Gate 1")
        {
            LevelsManager.Level1Done();
        }
        if (other.gameObject.name == "Gate 2")
        {
            LevelsManager.Level2Done();
        }
        if (other.gameObject.name == "Gate 3")
        {
            LevelsManager.Level3Done();
        }
    }
    IEnumerator Dash()
    {
        while (allowDash)
        {
            allowMove = false;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(dashPower * direction, 0);
            yield return new WaitForSeconds(dashTime);
            allowDash = false;
            allowMove = true;
            rb.gravityScale = 1;
            StartCoroutine(DashCoolDown());
            yield break;
        }
    }
    IEnumerator DashCoolDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(dashCoolDownTime);
            allowDash = true;
            yield break;
        }
    }
}