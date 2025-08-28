using UnityEngine;
using UnityEngine.UI;

public class CookieVisual : MonoBehaviour
{
    public Transform cookie;          // ตัว sprite ของคุกกี้
    public Transform topPosition;     // จุดสูงสุด (คุกกี้ยกขึ้น)
    public Transform dipPosition;     // จุดต่ำสุด (คุกกี้จุ่มลง)
    public float moveSpeed = 5f;

    private bool isDipping = false;

    void OnEnable()
    {
        CookieController.OnDip += HandleDip;
    }

    void OnDisable()
    {
        CookieController.OnDip -= HandleDip;
    }

    void HandleDip(bool dipping)
    {
        isDipping = dipping;
    }

    void Update()
    {
        if (cookie == null) return; // ✅ ถ้าคุกกี้หายไปแล้วไม่ทำอะไร
        Vector3 targetPos = isDipping ? dipPosition.position : topPosition.position;
        cookie.position = Vector3.Lerp(cookie.position, targetPos, Time.deltaTime * moveSpeed);
    }
}
