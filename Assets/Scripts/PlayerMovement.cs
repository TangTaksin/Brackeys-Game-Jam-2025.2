using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private bool moving = false;
    private bool isAlive = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CookieController.OnDip += HandleDip;
    }

    void Update()
    {
        if (!isAlive) 
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (moving)
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    void HandleDip(bool dipping)
    {
        if (isAlive)
            moving = dipping;
    }

    public void StopPlayer()
    {
        isAlive = false;
        moving = false;
    }

    private void OnDestroy()
    {
        CookieController.OnDip -= HandleDip;
    }
}
