using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cookie : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform defaultPosition;

    [Space]
    [SerializeField] float _staminaMax;
    [SerializeField] float _returnSpeed = 5f;
    [SerializeField] float _returnThreshold = 0.25f;

    float _stamina_current;
    float stamina_current
    {
        get { return _stamina_current; }
        set 
        { 
            _stamina_current = value; 
            OnStaminaUpdate?.Invoke(_stamina_current, _staminaMax); 
        }
    }

    
    bool _isReturning;

    public static Action<Cookie> OnInit;
    public static Action<float, float> OnStaminaUpdate;
    public static Action OnCookieEaten;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stamina_current = _staminaMax;

        OnInit?.Invoke(this);
    }

    

    private void Update()
    {
        RepositionCookie();
    }

    public void OnMouseDrag()
    {
        GrabCookie();
    }
    
    void OnMouseDown()
    {

    }

    private void OnMouseUp()
    {
        _isReturning = true;
    }

    void GrabCookie()
    {
        if (_isReturning)
            return;

        var cursor = Mouse.current.position.ReadValue();
        var newPos = cam.ScreenToWorldPoint(cursor);
        transform.position = new Vector2(newPos.x, newPos.y);
    }

    void EatCookie()
    {
        Init();
        OnCookieEaten?.Invoke();
    }

    void RepositionCookie()
    {
        if (!defaultPosition) 
            return;

        if (_isReturning)
        {
            // ค่อย ๆ กลับไป topPosition
            transform.position = Vector3.Lerp(transform.position, defaultPosition.position, _returnSpeed * Time.deltaTime);

            // ถ้าใกล้ถึง topPosition ให้ตรงพอดี
            if (Vector3.Distance(transform.position, defaultPosition.position) < _returnThreshold)
            {
                EatCookie();
                _isReturning = false;
            }
        }
    }

    public void ReduceStamina(float amount)
    {
        stamina_current -= amount;
        Mathf.Clamp(stamina_current, 0, _staminaMax);
    }

}
