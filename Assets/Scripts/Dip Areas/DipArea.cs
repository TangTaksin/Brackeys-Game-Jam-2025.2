
using UnityEngine;

public class DipArea : MonoBehaviour
{
    protected Cookie _cookie;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _cookie = collision.gameObject.GetComponent<Cookie>();

        if (_cookie)
            OnEnterEffect();
    }

    protected virtual void OnEnterEffect() { }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_cookie)
            OnStayEffect();
    }

    protected virtual void OnStayEffect() { }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_cookie == collision.GetComponent<Cookie>())
        {
            _cookie = null;
            OnExitEffect();
        }
    }

    protected virtual void OnExitEffect() { }
}
