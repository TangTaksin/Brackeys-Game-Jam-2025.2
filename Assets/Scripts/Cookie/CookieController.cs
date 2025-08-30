using UnityEngine;

public class CookieController : MonoBehaviour
{
    public float maxDipTime = 3f;   // เวลาจุ่มได้สูงสุด
    private float dipTimer = 0f;
    private bool isDipping = false;

    public Transform cookieVisual; // drag sprite cookie เข้ามาใน Inspector

    public delegate void DipAction(bool dipping);
    public static event DipAction OnDip;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDipping = true;
            dipTimer = 0f;
            OnDip?.Invoke(true); // แจ้งว่ากำลังจุ่ม
        }

        if (Input.GetKey(KeyCode.Space))
        {
            dipTimer += Time.deltaTime;
            if (dipTimer > maxDipTime)
            {
                Debug.Log("คุกกี้หล่นลงก้นแก้ว! Game Over");

                if (cookieVisual != null)
                    Destroy(cookieVisual.gameObject); // ทำลายคุกกี้

                OnDip?.Invoke(false); // ส่งสัญญาณหยุด Player
                //GameManager.Instance.GameOver();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && isDipping)
        {
            isDipping = false;
            OnDip?.Invoke(false); // แจ้งว่าหยุดจุ่ม
        }
    }
}
