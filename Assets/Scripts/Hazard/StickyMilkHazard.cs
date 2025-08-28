using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StickyMilkHazard : HazardBase
{
    [Header("Sticky Milk Settings")]
    [Range(0f, 1f)]
    public float slowMultiplier = 0.5f;

    private string targetTagString;

    private void Awake()
    {
        targetTagString = target.ToString();
    }

    protected override void ApplyEffect(GameObject target)
    {
        PlayerMovement player = target.GetComponent<PlayerMovement>();
        if (player != null)
            player.speedMultiplier = slowMultiplier;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTagString))
            return;

        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
            player.speedMultiplier = 1f;
    }
}
