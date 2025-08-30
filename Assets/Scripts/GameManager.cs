using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    enum gameState
    {
        playing,
        win,
        lose
    }
    gameState _currentGameState;

    public enum gameoverType
    {
        smiley_down,
        cookie_out,
        overdipped,
    }

    [SerializeField] float _gameoverDelay = 0.5f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        _currentGameState = gameState.playing;
        DOTween.KillAll();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }


    public void GameOver(gameoverType state)
    {
        if (_currentGameState == gameState.win 
            || _currentGameState == gameState.lose)
            return;

        _currentGameState = gameState.lose;

        switch (state)
        {
            case gameoverType.smiley_down:

                break;

            case gameoverType.cookie_out:

                break;

            case gameoverType.overdipped:

                break;
        }

        Invoke("RestartScene", _gameoverDelay);
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

        Init();
    }
    
    
    public void Win()
    {
        if (_currentGameState == gameState.lose)

        Debug.Log("🎉 You Win!");
        _currentGameState = gameState.win;
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


        Init();
    }
}
