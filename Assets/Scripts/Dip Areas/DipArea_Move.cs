using System;
using UnityEngine;

public class DipArea_Move : DipArea
{
    protected DipInput _input;

    public static Action<DipInput> OnAreaEnter;
    public static Action<DipInput> OnAreaStay;
    public static Action<DipInput> OnAreaExit;

    protected override void OnEnterEffect()
    {
        _input.is_inputing = true;
        OnAreaEnter?.Invoke(_input);
    }

    protected override void OnStayEffect()
    {
        _input.f_value = _cookieDepthf;
        OnAreaStay?.Invoke(_input);
    }

    protected override void OnExitEffect()
    {
        _input.is_inputing = false;
        OnAreaExit?.Invoke(_input);
    }
}
