using UnityEngine;

public class FallingPlatform : Platform
{
    [SerializeField] private float fallDelay = 1f;
    private Rigidbody2D rb;
    private bool triggered = false;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        // Start as kinematic
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    protected override void Move()
    {
        // Falling handled by physics after trigger
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            Invoke(nameof(Drop), fallDelay);
        }
    }

    private void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic; // Let gravity take over
    }
}
