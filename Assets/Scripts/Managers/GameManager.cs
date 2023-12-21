using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int coins { get; private set; }
    public int lives { get; private set; }
    public string world { get; private set; }

    public static bool isDead { get; private set; }
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        NewGame();
    }
    private void NewGame()
    {
        lives = 5;
        coins = 0;
        LoadLevel("Main");
    }
    public void LoadLevel(string world)
    {
        this.world = world;
        SceneManager.LoadScene(world);
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }
    public void ResetLevel(string world)
    {
        lives--;
        if (lives >= 0)
        {
            LoadLevel(world);
        }
        else
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        SceneManager.LoadScene("Death");
    }
    public void AddCoins()
    {
        coins++;
    }
}
