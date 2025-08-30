using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    [SerializeField] protected float speed = 2f;
    protected Transform platformTransform;

    protected virtual void Awake()
    {
        platformTransform = transform;
    }

    protected abstract void Move();

    protected virtual void Update()
    {
        Move();
    }

    // Handles carrying the player
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(transform);
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(null);
    }
}
