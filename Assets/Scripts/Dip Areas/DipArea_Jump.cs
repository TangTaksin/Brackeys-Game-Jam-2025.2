using System;
using UnityEngine;

public class DipArea_Jump : DipArea
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
    
    void FixedUpdate()
    {
        TriggerJump();
    }

    private void TriggerJump()
    {
        if (_cookie)
        {
            _input.is_inputing = true;
            OnAreaEnter?.Invoke(_input);
        }
    }
}
