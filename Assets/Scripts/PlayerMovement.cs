using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    private bool moving = false;
    private bool isAlive = true;
    private bool grounded = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CookieController.OnDip += HandleDip;
    }

    void Update()
    {
        if (!isAlive)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        HandleMovement();
    }

    void HandleDip(bool dipping)
    {
        if (isAlive)
            moving = dipping;
    }

    public void StopPlayer()
    {
        isAlive = false;
        moving = false;
        rb.linearVelocity = Vector2.zero;
    }

    public void HandleMovement()
    {
        if (moving)
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    public void Die()
    {
        if (!isAlive) return;
        StopPlayer();
        Destroy(this.gameObject);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // reset Y before jump
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnDestroy()
    {
        CookieController.OnDip -= HandleDip;
    }

}
