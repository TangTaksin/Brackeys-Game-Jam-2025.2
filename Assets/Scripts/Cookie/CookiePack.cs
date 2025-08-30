using UnityEngine;

public class CookiePack : MonoBehaviour
{
    [SerializeField] Transform spawnpoint;
    [SerializeField] GameObject cookieObject;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        
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

    }
}
