using UnityEngine;
using UnityEngine.UI;

public class Cup : DipArea
{
    [SerializeField] Transform _cupTopPoint, _cupBottomPoint;

    [Header("Stamina Reduction")]
    [SerializeField] AnimationCurve _staminaReductionCurve;
   
    [Header("Contained Dip Areas")]
    [SerializeField] DipArea[] containedDipAreas;

    [Space]
    [SerializeField] ParticleSystem _milkSplashParticle;

    private void Start()
    {
        Init();
    }

    void Init()
    {

    }

    private void FixedUpdate()
    {
        ReduceCookieStamina();
    }

    protected override void OnEnterEffect()
    {
        PlaySplash();
    }

    protected override void OnStayEffect()
    {
        TrackCookieDepth();
    }

    protected override void OnExitEffect()
    {
        PlaySplash();
    }

    void PlaySplash()
    {
        if (_milkSplashParticle)
            _milkSplashParticle.Play();
    }

    void TrackCookieDepth()
    {
        if (_cookie)
        {
            var cookie_y = _cookie.transform.position.y;

            var top_y = _cupTopPoint.position.y;
            var bottom_y = _cupBottomPoint.position.y;

            // map top and bottom to 0.0 - 1.0
            // get where cookie_y sit at between 0.0 - 1.0
            _cookieDepthf = ExtensionMethod.Remap(cookie_y, top_y, bottom_y, 0, 1);
            Mathf.Clamp(_cookieDepthf, 0, 1);

            foreach (var dArea in containedDipAreas)
            {
                dArea.SetCookieDepth(_cookieDepthf);
            }
        }
    }

    void ReduceCookieStamina()
    {
        var relative_depth = _staminaReductionCurve.Evaluate(_cookieDepthf);
        _cookie?.ReduceStamina(relative_depth);
    }
}

public struct DipInput
{
    public bool is_inputing;
    public float f_value;
}
