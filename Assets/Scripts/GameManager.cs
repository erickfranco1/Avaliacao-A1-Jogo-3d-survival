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

    // conecta no evento "On Health Changed" do playerHealth
    public void UpdateHealthUI(int current, int max)
    {
        if (healthText != null)
            healthText.text = $"Vida: {current}/{max}";
    }

    // conecta no "On Death" do PlayerHealth, no inspetor
    public void OnPlayerDeath()
    {
        EndGame(false);
    }

    void OnEnemyKilled()
    {
        // espaço livre pra um contador de mortes/pontuação, caso chegue a expandir
    }

    void EndGame(bool victory)
    {
        gameEnded = true;
        gameOverPanel.SetActive(true);
        gameOverMessage.text = victory ? "Você sobreviveu!" : "Game Over";
        Time.timeScale = 0f;
    }

    // conecta este método no OnClick do botão "Reiniciar" da UI
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}