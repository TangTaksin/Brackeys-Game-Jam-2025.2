using UnityEngine;
using UnityEngine.UI;

public class Cup : DipArea
{

    [SerializeField] Transform _cupTopPoint, _cupBottomPoint;
    [Space]
    [SerializeField] AnimationCurve _staminaReductionCurve;
    [Space]
    [SerializeField] DipArea[] containedDipAreas;

    float _cookieDepthf;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        
    }

    private void FixedUpdate()
    {
        TrackCookieDepth();

        ReduceCookieStamina();
    }

    protected override void OnEnterEffect()
    {

    }

    protected override void OnStayEffect()
    {
        
    }

    protected override void OnExitEffect()
    {
        
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
