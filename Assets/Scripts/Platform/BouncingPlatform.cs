using UnityEngine;
using DG.Tweening; // Make sure DOTween is installed

public class BouncingPlatform : Platform
{
    [SerializeField] private float bounceForce = 15f;
    [SerializeField] private float squashDuration = 0.1f; // Time to squash
    [SerializeField] private float squashAmount = 0.7f;    // How much to squash (0-1)

    protected override void Move()
    {
        // Stationary platform
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D playerRb = collision.collider.attachedRigidbody;

        if (collision.gameObject.CompareTag("Player") && playerRb != null)
        {
            // Reset vertical velocity and add bounce
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0f);
            playerRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);

            // Animate platform squash and stretch
            AnimateBounce();
        }
    }

    private void AnimateBounce()
    {
        // Squash vertically and stretch horizontally
        platformTransform.DOKill(); // Kill previous animations
        platformTransform
            .DOScaleY(squashAmount, squashDuration)
            .SetLoops(2, LoopType.Yoyo); // Yoyo returns to original scale
        platformTransform
            .DOScaleX(1f + (1f - squashAmount), squashDuration)
            .SetLoops(2, LoopType.Yoyo);
    }
}
