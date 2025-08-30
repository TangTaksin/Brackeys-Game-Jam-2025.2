using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Stamina : MonoBehaviour
{
    [SerializeField] Image staminaFillImage;
    [SerializeField] float smoothSpeed = 5f;
    [SerializeField] float lowStaminaThreshold = 0.2f;

    Transform _cookieTransform;
    float targetFill = 1f;
    Tween pulseTween;

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

        // Kill tween ทั้งหมดที่เกี่ยวข้อง
        DOTween.Kill(transform);
        DOTween.Kill(staminaFillImage);

        if (pulseTween != null)
        {
            pulseTween.Kill();
            pulseTween = null;
        }

        transform.localScale = Vector3.one;
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
        DOTween.Kill(staminaFillImage);

        if (pulseTween != null)
        {
            pulseTween.Kill();
            pulseTween = null;
        }
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
        if (_cookieTransform == null) return;

        Vector3 targetPos = _cookieTransform.position;
        targetPos.z = transform.position.z;
        transform.position = targetPos;
    }

    void UpdateGuageValue(float curf, float maxf)
    {
        targetFill = Mathf.Clamp01(curf / maxf);

        if (staminaFillImage == null) return;

        if (targetFill <= lowStaminaThreshold)
        {
            staminaFillImage.DOColor(Color.red, 0.2f);

            if (pulseTween == null || !pulseTween.IsActive())
            {
                pulseTween = transform.DOScale(1.1f, 0.4f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            }
        }
        else
        {
            staminaFillImage.DOColor(Color.white, 0.2f);

            if (pulseTween != null)
            {
                pulseTween.Kill();
                pulseTween = null;
                transform.DOScale(1f, 0.2f);
            }
        }
    }

    void SmoothUpdateFill()
    {
        if (staminaFillImage == null) return;
        staminaFillImage.fillAmount = Mathf.Lerp(staminaFillImage.fillAmount, targetFill, smoothSpeed * Time.deltaTime);
    }
}
