using UnityEngine;
using UnityEngine.UI;

public class UI_Stamina : MonoBehaviour
{
    [SerializeField] Image staminaFillImage;

    Transform _cookieTransform;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void Update()
    {
        UpdateGuagePosition();
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
        var fillvalue = curf / maxf;

        staminaFillImage.fillAmount = fillvalue;
    }
}
