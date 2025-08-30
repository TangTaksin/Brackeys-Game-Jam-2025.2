using TMPro;
using UnityEngine;

public class UI_CookiePack : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI remainText;

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
        CookiePack.RemainedCookieUpdated += UpdateDisplay;
    }

    #endregion

    void UpdateDisplay(int cur, int max)
    {
        remainText.text = cur.ToString();
    }
}
