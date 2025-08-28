using UnityEngine;

public abstract class HazardBase : MonoBehaviour
{

    public string targetTag = "Player";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            ApplyEffect(collision.gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            OnStayEffect(collision.gameObject);
        }
    }

    // Abstract methods for children to override
    protected abstract void ApplyEffect(GameObject target);
    protected virtual void OnStayEffect(GameObject target) { }
}
