using UnityEngine;

public class LinearMovingPlatform : Platform
{
    [SerializeField] private Vector2 moveDirection = Vector2.right;
    [SerializeField] private float distance = 5f;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingToEnd = true;

    GameObject _childPlayer;

    protected override void Awake()
    {
        base.Awake();
        startPos = platformTransform.position;
        endPos = startPos + (Vector3)(moveDirection.normalized * distance);
    }

    protected override void Move()
    {
        if (movingToEnd)
        {
            platformTransform.position = Vector3.MoveTowards(platformTransform.position, endPos, speed * Time.deltaTime);
            if (Vector3.Distance(platformTransform.position, endPos) < 0.05f)
                movingToEnd = false;
        }
        else
        {
            platformTransform.position = Vector3.MoveTowards(platformTransform.position, startPos, speed * Time.deltaTime);
            if (Vector3.Distance(platformTransform.position, startPos) < 0.05f)
                movingToEnd = true;
        }
    }

    // ทำให้ Player ติดไปกับ Platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _childPlayer = collision.gameObject;
            _childPlayer.transform.SetParent(transform);
        }
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var active = collision.gameObject.activeInHierarchy;

        if (collision.gameObject == _childPlayer)
        {
            if (active)
            {
                _childPlayer.transform.SetParent(null);
                _childPlayer = null;
            }
        }
    }
}
