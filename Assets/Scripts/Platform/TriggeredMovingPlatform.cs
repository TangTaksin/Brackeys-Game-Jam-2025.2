using UnityEngine;

public class TriggeredMovingPlatform : Platform
{
    [SerializeField] private Vector2 moveDirection = Vector2.right;
    [SerializeField] private float distance = 5f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool moving = false;

    protected override void Awake()
    {
        base.Awake();
        startPos = platformTransform.position;
        targetPos = startPos + (Vector3)(moveDirection.normalized * distance);

        // Ensure Rigidbody2D exists and is kinematic
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    protected override void Move()
    {
        if (!moving) return; // Only move when triggered

        platformTransform.position = Vector3.MoveTowards(
            platformTransform.position,
            targetPos,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(platformTransform.position, targetPos) < 0.05f)
        {
            targetPos = (targetPos == startPos)
                ? startPos + (Vector3)(moveDirection.normalized * distance)
                : startPos;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            moving = true; // Start moving
        }
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            moving = false; // Stop moving when player leaves
        }
    }
}
