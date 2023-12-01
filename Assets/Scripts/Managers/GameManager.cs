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
        LoadLevel("Chapter2");
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
    public void ResetLevel()
    {
        lives--;
        if (lives >= 0)
        {
            LoadLevel(this.world);
        }
        else
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        //it will be adding death scene it will be empty for now
    }
    public void AddCoins()
    {
        coins++;
        //maybe something special can be added  for 100 coins but that's it for now
    }
}
