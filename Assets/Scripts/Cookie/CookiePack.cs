using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class CookiePack : MonoBehaviour
{
    [SerializeField] Transform spawnpoint;
    [SerializeField] GameObject cookieObject;

    [Space]
    [SerializeField] int cookieLimit = 5;
    [SerializeField] float tweenTime = .5f;

    int _cookie_remain;
    int cookie_remain
    {
        get { return _cookie_remain; }
        set
        {
            _cookie_remain = value;
            RemainedCookieUpdated?.Invoke(cookie_remain, cookieLimit);
        }
    }

    public static Action<int, int> RemainedCookieUpdated;
    public static Action NoCookieLeftEvent;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        cookie_remain = cookieLimit;

        if (!cookieObject)
        {
            GameObject.Find("Cookie");
        }
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #region event subscription

    void SubscribeEvents()
    {
        Cookie.OnCookieEaten += TakeOutCookie;
    }

    void UnsubscribeEvents()
    {
        Cookie.OnCookieEaten -= TakeOutCookie;
    }

    #endregion

    void TakeOutCookie()
    {
        cookie_remain--;
        Mathf.Clamp(cookie_remain, 0, cookieLimit);

        if (cookie_remain <= 0)
        {
            GameManager.Instance.RestartScene();
            NoCookieLeftEvent?.Invoke();
            return;
        }

    }
}
