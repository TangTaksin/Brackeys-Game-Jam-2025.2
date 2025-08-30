using UnityEngine;
using UnityEngine.UI;

public class UI_Stamina : MonoBehaviour
{
    [SerializeField] Image staminaFillImage;
    [SerializeField] float smoothSpeed = 5f;

    Transform _cookieTransform;
    float targetFill = 1f;

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
        transform.position = _cookieTransform.position;
    }

    void UpdateGuageValue(float curf, float maxf)
    {
        targetFill = Mathf.Clamp01(curf / maxf);
    }

    void SmoothUpdateFill()
    {
        if (!staminaFillImage) return;
        staminaFillImage.fillAmount = Mathf.Lerp(staminaFillImage.fillAmount, targetFill, smoothSpeed * Time.deltaTime);
    }


}
