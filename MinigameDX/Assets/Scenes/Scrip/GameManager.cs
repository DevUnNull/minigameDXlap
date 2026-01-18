using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    [Tooltip("Kéo Panel UI hiển thị khi thắng vào đây")]
    public GameObject winPanel;

    [Header("Rotate Rush References")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI yourScoreText;
    private int score = 0;
    private int miss =0 ;
    private int TotalScore = 160;
    public int Score => score; // Expose score for other systems (read-only)
    public bool IsGameOver { get; private set; }

    [Header("Color Settings")]
    public Color[] possibleColors; // Danh sách các màu có thể random
    public Color activeColorA;     // Màu cho bên A (hoặc Left)
    public Color activeColorB;     // Màu cho bên B (hoặc Right)

    [Header("Audio")]
    public AudioSource musicSource;


    private void Awake()
    {
        // Setup Singleton
        if (Instance == null)
        {
            Instance = this;
            RandomizeThemeColors(); // Random màu ngay khi game bắt đầu
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void RandomizeThemeColors()
    {
        if (possibleColors == null || possibleColors.Length < 2)
        {
            // Fallback nếu chưa config
            activeColorA = Color.blue;
            activeColorB = Color.red;
            return;
        }

        // Chọn 2 màu ngẫu nhiên khác nhau
        int indexA = Random.Range(0, possibleColors.Length);
        int indexB = indexA;
        while (indexB == indexA)
        {
            indexB = Random.Range(0, possibleColors.Length);
        }

        activeColorA = possibleColors[indexA];
        activeColorB = possibleColors[indexB];
    }

    private void Start()
    {
        // Đảm bảo lúc bắt đầu game thì tắt bảng Win đi
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void UnAddScore(int amount)
    {
        score = amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("GameManager: 'scoreText' is not assigned in the Inspector.");
        }

        if (yourScoreText != null)
        {
            yourScoreText.text = "Your Score: " + (TotalScore - miss)/TotalScore *100 ;
        }
        else
        {
            Debug.LogWarning("GameManager: 'yourScoreText' is not assigned in the Inspector.");
        }
    }

    public void GameOver()
    {
        if (IsGameOver) return; // tránh gọi nhiều lần
        IsGameOver = true;

        Debug.Log("Game Over!");

        // DỪNG NHẠC
        if (musicSource != null)
        {
            musicSource.Stop();
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Nếu bạn muốn dừng toàn bộ game logic
        Time.timeScale = 0f;
    }
    public void WinGame()
    {
        if (IsGameOver) return; // tránh gọi nhiều lần
        IsGameOver = true;

        Debug.Log("You Win!");

        // DỪNG NHẠC
        if (musicSource != null)
        {
            musicSource.Stop();
        }

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        // Nếu bạn muốn dừng toàn bộ game logic
        Time.timeScale = 0f;
    }


    public void OnLevelComplete()
    {
        Debug.Log("GameManager nhận được thông báo: THẮNG!");

        // Hiển thị UI chiến thắng
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    public void NextLevel()
    {
        // Load màn chơi tiếp theo trong Build Settings
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Nếu còn màn tiếp theo thì load, nếu hết thì quay về màn 0
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Đã hết màn chơi, quay lại màn 0.");
            SceneManager.LoadScene(0);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    // Called when a new local high score is detected
    public void NewHighScore(int newScore)
    {
        PlayerPrefs.SetInt("HighScore", newScore);
        if (yourScoreText != null)
            yourScoreText.text = "Your Score: " + newScore;
        Debug.Log($"NewHighScore set: {newScore}");
    }

    public int getMiss => miss;
    public void AddMiss(int amount)
    {
        miss += amount;
    }
}
