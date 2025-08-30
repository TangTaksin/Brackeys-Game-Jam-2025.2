using DG.Tweening;
using TMPro;
using UnityEngine;

public class UI_CookiePack : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI remainText;

    [SerializeField] GameObject _cookieChunkParticle;

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
        CookiePack.RemainedCookieUpdated += UpdateDisplay;
        }

    void UnsubscribeEvents()
    {
        CookiePack.RemainedCookieUpdated -= UpdateDisplay;
    }

    #endregion

    void UpdateDisplay(int cur, int max)
    {
        remainText.transform.DOShakeScale(.5f, Vector3.right);
        remainText.text = cur.ToString();

        if (cur <= 0)
        {
            _cookieChunkParticle.transform.position = transform.position;
            Instantiate(_cookieChunkParticle);
        }
    }
}
