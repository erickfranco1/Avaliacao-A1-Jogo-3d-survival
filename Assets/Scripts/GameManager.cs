using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// fica num GameObject vazio chamado "GameManager"
public class GameManager : MonoBehaviour
{
    [Header("Config")]
    public float survivalTimeGoal = 120f; // 2 minutos para vencer

    [Header("UI (TextMeshPro)")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI healthText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverMessage;

    [Header("Referências")]
    public PlayerHealth playerHealth;
    public GameObject gameOverCamera; 

    private float elapsedTime;
    private bool gameEnded;

    void Start()
    {
        gameOverPanel.SetActive(false);
        EnemyHealth.onAnyEnemyDeath.AddListener(OnEnemyKilled);
    }

    void Update()
    {
        if (gameEnded) return;

        elapsedTime += Time.deltaTime;
        UpdateTimerUI();

        if (elapsedTime >= survivalTimeGoal)
            EndGame(true);
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;
        float timeLeft = Mathf.Max(0, survivalTimeGoal - elapsedTime);
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    
    public void UpdateHealthUI(int current, int max)
    {
        if (healthText != null)
            healthText.text = $"Vida: {current}/{max}";
    }

    
    public void OnPlayerDeath()
    {
        EndGame(false);
    }

    void OnEnemyKilled()
    {
        // espaço livre pra um contador de mortes/pontuação
    }

    void EndGame(bool victory)
    {
        gameEnded = true;
        gameOverPanel.SetActive(true);
        gameOverMessage.text = victory ? "Você sobreviveu!" : "Game Over";

        if (gameOverCamera != null)
            gameOverCamera.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}