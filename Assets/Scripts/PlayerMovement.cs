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
        Init();
    }

    private void Init()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        EventSubscription();
    }

    private void OnDisable()
    {
        EventUnsubscription();
    }

    void EventSubscription()
    {
        DipArea_Move.OnAreaEnter += OnMove;
        DipArea_Move.OnAreaExit += OnMove;

        DipArea_Jump.OnAreaEnter += OnJump;
    }

    void EventUnsubscription()
    {
        DipArea_Move.OnAreaEnter -= OnMove;
        DipArea_Move.OnAreaExit -= OnMove;

        DipArea_Jump.OnAreaEnter -= OnJump;
    }

    void Update()
    {
        HandleMovement();
    }

    void OnMove(DipInput dinput)
    {
        if (isAlive)
            moving = dinput.is_inputing;
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

    private void OnJump(DipInput dinput)
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
}
