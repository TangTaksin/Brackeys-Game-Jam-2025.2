using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool _inWinState;

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


    public void GameOver()
    {
        Debug.Log("💀 Game Over!");

        
        // TODO: เพิ่ม UI หรือ restart
    }

    public void RestartScene()
    {
        Transition.CalledFadeIn?.Invoke();
        Transition.FadeInOver += LoadCurrentScene;
    }

    public void LoadCurrentScene()
    {
        Transition.FadeInOver -= LoadCurrentScene;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    
    public void Win()
    {
        Debug.Log("🎉 You Win!");
        _inWinState = true;
        Transition.CalledFadeIn?.Invoke();
        Transition.FadeInOver += LoadNextScene;
    }

    public void LoadNextScene()
    {
        Transition.FadeInOver -= LoadNextScene;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if next scene exists
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes to load!");
            // Optional: Loop back to first scene or show end game UI
            SceneManager.LoadScene(0);
        }

        _inWinState = false;
    }
}
