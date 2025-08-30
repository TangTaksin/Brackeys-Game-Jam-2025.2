using UnityEngine;

// for draging cookie back when the mouse is release.
public class CookieVisual : MonoBehaviour
{
    public Transform cookie;
    public Transform topPosition;
    public float moveSpeed = 5f;

    private bool isReturning = false;

    void Update()
    {
        if (cookie == null || topPosition == null) return;

        if (isReturning)
        {
            // ค่อย ๆ กลับไป topPosition
            cookie.position = Vector3.Lerp(cookie.position, topPosition.position, moveSpeed * Time.deltaTime);

            // ถ้าใกล้ถึง topPosition ให้ตรงพอดี
            if (Vector3.Distance(cookie.position, topPosition.position) < 0.001f)
            {
                cookie.position = topPosition.position;
                isReturning = false;
            }
        }
    }

    void OnMouseDown()
    {
        // หยุดดึงกลับตอนกดเมาส์
        isReturning = false;
    }

    void OnMouseUp()
    {
        // เริ่มดึงกลับตอนปล่อยเมาส์
        isReturning = true;
    }
}
