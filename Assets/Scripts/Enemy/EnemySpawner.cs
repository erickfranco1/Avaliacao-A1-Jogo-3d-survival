using UnityEngine;

// Anexe no GameObject "EnemySpawner".
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints; // arraste pontos vazios posicionados nas bordas da arena

    [Header("Dificuldade")]
    public float initialInterval = 3f;
    public float minInterval = 0.7f;
    public float intervalDecreaseRate = 0.05f; // reduz o intervalo conforme o tempo de jogo passa

    private float currentInterval;
    private float nextSpawnTime;

    void Start()
    {
        currentInterval = initialInterval;
        nextSpawnTime = Time.time + currentInterval;
    }

    void Update()
    {
        currentInterval = Mathf.Max(minInterval, initialInterval - (Time.timeSinceLevelLoad * intervalDecreaseRate));

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + currentInterval;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoints == null || spawnPoints.Length == 0) return;

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, point.position, point.rotation);
    }
}