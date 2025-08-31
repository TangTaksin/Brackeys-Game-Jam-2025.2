using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private AnimationCurve movementSpeedCurve;
    [SerializeField] private float brakeTime = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpSpeedBoost = 1.2f;

    [Header("Runtime Values")]
    [HideInInspector] public float speedMultiplier = 1f;

    [Space]
    [SerializeField] GameObject onDeadParticle;

    private bool isMoving = false;
    private float moveInputValue;
    private bool isAlive = true;
    private bool isGrounded = false;

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
        DipArea_Move.OnAreaStay += OnMove;
        DipArea_Move.OnAreaExit += OnMove;

        DipArea_Jump.OnAreaEnter += OnJump;
    }

    void EventUnsubscription()
    {
        DipArea_Move.OnAreaEnter -= OnMove;
        DipArea_Move.OnAreaStay -= OnMove;
        DipArea_Move.OnAreaExit -= OnMove;

        DipArea_Jump.OnAreaEnter -= OnJump;
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void OnMove(DipInput dinput)
    {
        if (isAlive)
        {
            isMoving = dinput.is_inputing;
            moveInputValue = dinput.f_value;
        }
    }

    public void StopPlayer()
    {
        isAlive = false;
        isMoving = false;
        rb.linearVelocity = Vector2.zero;
    }

    public void HandleMovement()
    {
        if (isMoving && isGrounded)
        {
            var trueSpeed = movementSpeedCurve.Evaluate(moveInputValue);
            rb.linearVelocity = new Vector2(trueSpeed * speedMultiplier, rb.linearVelocity.y);
        }
        else if (isGrounded)
        {
            float deceleration = Mathf.Abs(rb.linearVelocity.x) / brakeTime;
            float newX = Mathf.MoveTowards(rb.linearVelocity.x, 0, deceleration * Time.deltaTime);
            rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y);
        }
    }

    public void Die()
    {
        if (!isAlive) return;
        StopPlayer();

        onDeadParticle.transform.position = transform.position;
        Instantiate(onDeadParticle);

        GameManager.Instance.GameOver(GameManager.gameoverType.smiley_down);
        Destroy(this.gameObject);
    }



    private void OnJump(DipInput dinput)
    {
        if (isGrounded == true && GameManager.Instance.IsPlaying)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.cookie_Jump_sfx);
            // Calculate speed boost based on input direction
            float horizontalBoost = moveInputValue * jumpSpeedBoost;
            float newXVelocity = rb.linearVelocity.x + horizontalBoost;

            rb.linearVelocity = new Vector2(newXVelocity, rb.linearVelocity.y);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
