using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTween

public class UI_Stamina : MonoBehaviour
{
    [SerializeField] Image staminaFillImage;
    [SerializeField] float smoothSpeed = 5f;
    [SerializeField] float lowStaminaThreshold = 0.2f; // เหลือต่ำกว่า 20% ถือว่าใกล้หมด

    Transform _cookieTransform;
    float targetFill = 1f;
    Tween pulseTween; // เก็บ tween ที่วิ่งอยู่ป้องกันซ้อนกัน

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void Update()
    {
        UpdateGuagePosition();
        SmoothUpdateFill();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
        pulseTween?.Kill();
    }

    #region event subscription

    void SubscribeEvents()
    {
        Cookie.OnInit += GetCookie;
        Cookie.OnStaminaUpdate += UpdateGuageValue;
    }

    void UnsubscribeEvents()
    {
        Cookie.OnInit -= GetCookie;
        Cookie.OnStaminaUpdate -= UpdateGuageValue;
    }

    #endregion

    void GetCookie(Cookie cookie)
    {
        _cookieTransform = cookie.transform;
    }

    void UpdateGuagePosition()
    {
        Vector3 targetPos = _cookieTransform.position;
        targetPos.z = transform.position.z; // keep current Z
        transform.position = targetPos;
    }

    void UpdateGuageValue(float curf, float maxf)
    {
        targetFill = Mathf.Clamp01(curf / maxf);

        if (targetFill <= lowStaminaThreshold)
        {
            // เปลี่ยนสีเป็นแดง
            staminaFillImage.DOColor(Color.red, 0.2f);

            // ถ้ายังไม่มี pulse ให้เริ่ม
            if (pulseTween == null || !pulseTween.IsActive())
            {
                pulseTween = transform.DOScale(1.1f, 0.4f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            }
        }
        else
        {
            // กลับสีขาว
            staminaFillImage.DOColor(Color.white, 0.2f);

            // หยุด pulse กลับเป็น scale เดิม
            if (pulseTween != null)
            {
                pulseTween.Kill();
                transform.DOScale(1f, 0.2f);
            }
        }
    }

    void SmoothUpdateFill()
    {
        if (!staminaFillImage) return;
        staminaFillImage.fillAmount = Mathf.Lerp(staminaFillImage.fillAmount, targetFill, smoothSpeed * Time.deltaTime);
    }
}
