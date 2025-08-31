using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cookie : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera cam;
    [SerializeField] Transform defaultPosition;

    [Header("Stamina Settings")]
    [SerializeField] float _staminaMax;
    [SerializeField] float _returnSpeed = 5f;
    [SerializeField] float _returnThreshold = 0.25f;

    [Space]
    [SerializeField] GameObject _cookieChunkParticle;

    Rigidbody2D _body;

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

    Vector2 _cursorOffset;
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
        ResetStamina();

        _body = GetComponent<Rigidbody2D>();

        if (!cam)
            cam = Camera.allCameras[1];
        if (!defaultPosition)
            defaultPosition = GameObject.Find("top_position").transform;

        OnInit?.Invoke(this);
    }

    void ResetStamina()
    {
        stamina_current = _staminaMax;
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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.grab_CookieSFX);
        var cursor = Mouse.current.position.ReadValue();
        var wCursor = cam.ScreenToWorldPoint(cursor);
        _cursorOffset = transform.position - wCursor;
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
        var newPos = cam.ScreenToWorldPoint(cursor) + (Vector3)_cursorOffset;

        _body.MovePosition(new Vector2(transform.position.x, newPos.y));
    }

    void EatCookie()
    {
        ResetStamina();

        OnCookieEaten?.Invoke();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.eat_Cookie_sfx);
    }



    void RepositionCookie()
    {
        if (!defaultPosition)
            return;

        if (_isReturning)
        {
            transform.position = Vector3.Lerp(transform.position, defaultPosition.position, _returnSpeed * Time.deltaTime);

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

        if (stamina_current <= 0)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.lose_Cookie_sfx);
            GameManager.Instance.GameOver(GameManager.gameoverType.overdipped);

            _cookieChunkParticle.transform.position = transform.position;
            Instantiate(_cookieChunkParticle);

            gameObject.SetActive(false);
        }
    }

}
