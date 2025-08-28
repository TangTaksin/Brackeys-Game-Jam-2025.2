using UnityEngine;

public abstract class HazardBase : MonoBehaviour
{
    public HazardTarget target = HazardTarget.Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(target.ToString()))
        {
            ApplyEffect(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(target.ToString()))
        {
            OnStayEffect(collision.gameObject);
        }
    }

    // Abstract methods for children to override
    protected abstract void ApplyEffect(GameObject target);
    protected virtual void OnStayEffect(GameObject target) { }
}

public enum HazardTarget
{
    Player, Enemy
}
