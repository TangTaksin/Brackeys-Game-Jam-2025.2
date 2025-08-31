using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private enum GameState
    {
        playing,
        win,
        lose
    }
    private GameState _currentGameState;

    // expose only read-only properties
    public bool IsPlaying => _currentGameState == GameState.playing;
    public bool IsWin => _currentGameState == GameState.win;
    public bool IsLose => _currentGameState == GameState.lose;

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
        _currentGameState = GameState.playing;
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
        if (_currentGameState == GameState.win
            || _currentGameState == GameState.lose)
            return;

        _currentGameState = GameState.lose;

        switch (state)
        {
            case gameoverType.smiley_down:

                break;

            case gameoverType.cookie_out:

                break;

            case gameoverType.overdipped:

                break;
        }
        
        AudioManager.Instance.PlaySFX(AudioManager.Instance.lose_Cookie_sfx);

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
        if (_currentGameState == GameState.lose)
            return;


        Debug.Log("🎉 You Win!");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.goal_sfx);
        _currentGameState = GameState.win;
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
