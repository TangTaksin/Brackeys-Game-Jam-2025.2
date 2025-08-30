using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
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

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
