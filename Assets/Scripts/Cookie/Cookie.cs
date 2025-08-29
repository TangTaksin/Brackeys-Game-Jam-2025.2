using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cookie : MonoBehaviour
{
    [SerializeField] Camera cam;

    [Space]
    [SerializeField] float _staminaMax;

    float _stamina_current;

    public static Action<Cookie> OnInit;
    public static Action<float, float> OnStaminaUpdate;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        _stamina_current = _staminaMax;

        OnInit?.Invoke(this);
    }

    public void OnMouseDrag()
    {
        var cursor = Mouse.current.position.ReadValue();
        var newPos = cam.ScreenToWorldPoint(cursor);;
        transform.position = new Vector2(newPos.x, newPos.y);
    }

    public void ReduceStamina(float amount)
    {
        _stamina_current -= amount;
        Mathf.Clamp(_stamina_current, 0, _staminaMax);

        OnStaminaUpdate?.Invoke(_stamina_current, _staminaMax);
    }

}
