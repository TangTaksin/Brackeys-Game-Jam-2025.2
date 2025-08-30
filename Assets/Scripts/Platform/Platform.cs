using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    [SerializeField] protected float speed = 2f;
    protected Transform platformTransform;

    private Vector3 previousPosition;

    protected virtual void Awake()
    {
        platformTransform = transform;
        previousPosition = transform.position;

        // Ensure Rigidbody2D exists and is kinematic
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    protected abstract void Move();

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void LateUpdate()
    {
        previousPosition = platformTransform.position;
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.collider.attachedRigidbody;
            if (rb != null)
            {
                Vector3 delta = platformTransform.position - previousPosition;
                rb.MovePosition(rb.position + new Vector2(delta.x, delta.y));
            }
        }
    }

}
