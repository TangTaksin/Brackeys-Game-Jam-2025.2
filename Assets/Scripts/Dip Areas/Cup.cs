using UnityEngine;
using UnityEngine.UI;

public class Cup : DipArea
{ 
    [SerializeField] DipArea[] containedDipAreas;

    private void FixedUpdate()
    {
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

    void ReduceCookieStamina()
    {
        _cookie?.ReduceStamina(1);
    }
}

public struct DipInput
{
    public bool is_inputing;
    public float f_value;
}
