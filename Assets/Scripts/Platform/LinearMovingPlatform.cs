using UnityEngine;

public class LinearMovingPlatform : Platform
{
    [SerializeField] private Vector2 moveDirection = Vector2.right; // Default move right
    [SerializeField] private float distance = 5f;

    private Vector3 startPos;
    private Vector3 targetPos;

    protected override void Awake()
    {
        base.Awake();
        startPos = platformTransform.position;
        targetPos = startPos + (Vector3)(moveDirection.normalized * distance);
    }

    protected override void Move()
    {
        platformTransform.position = Vector3.MoveTowards(
            platformTransform.position,
            targetPos,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(platformTransform.position, targetPos) < 0.05f)
        {
            // Swap between start and target
            targetPos = (targetPos == startPos)
                ? startPos + (Vector3)(moveDirection.normalized * distance)
                : startPos;
        }
    }
}
