using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Win()
    {
        Debug.Log("🎉 You Win!");
        // TODO: เพิ่ม UI หรือโหลด scene ใหม่
    }

    public void GameOver()
    {
        Debug.Log("💀 Game Over!");
        // TODO: เพิ่ม UI หรือ restart
    }
}
