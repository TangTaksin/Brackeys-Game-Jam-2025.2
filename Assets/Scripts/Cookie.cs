using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cookie : MonoBehaviour
{
    [SerializeField] Camera cam;


    public void OnMouseDrag()
    {
        var cursor = Mouse.current.position.ReadValue();
        var newPos = cam.ScreenToWorldPoint(cursor);;
        transform.position = new Vector2(newPos.x, newPos.y);
    }
}
