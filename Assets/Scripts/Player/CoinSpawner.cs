using UnityEngine;
using UnityEngine.AI;

public class CoinSpawner : MonoBehaviour
{
    [Header("Moeda")]
    [SerializeField] GameObject coin1Prefab;
    [SerializeField] int minimumCoins = 2;

    [Header("Área de procura")]
    [SerializeField] Vector3 spawnArea = new Vector3(30f, 10f, 30f);

    [Header("Validação")]
    [SerializeField] float navMeshDistance = 5f;
    [SerializeField] float checkRadius = 0.5f;
    [SerializeField] LayerMask obstacleLayer;

    [Header("Tentativas")]
    [SerializeField] int maxSpawnAttempts = 30;

    private void Start()
    {
        CheckCoins();
    }

    private void Update()
    {
        CheckCoins();
    }

    void CheckCoins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        int missingCoins = minimumCoins - coins.Length;

        for (int i = 0; i < missingCoins; i++)
        {
            SpawnCoin();
        }
    }

    void SpawnCoin()
    {
        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            // Escolhe uma posição aleatória dentro da área
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f),
                Random.Range(-spawnArea.y / 2f, spawnArea.y / 2f),
                Random.Range(-spawnArea.z / 2f, spawnArea.z / 2f)
            );

            randomPosition += transform.position;

            // Procura o ponto mais próximo no NavMesh
            if (NavMesh.SamplePosition(
                randomPosition,
                out NavMeshHit navHit,
                navMeshDistance,
                NavMesh.AllAreas))
            {
                Vector3 spawnPosition = navHit.position;

                // Verifica se existe algum objeto bloqueando o local
                bool blocked = Physics.CheckSphere(
                    spawnPosition,
                    checkRadius,
                    obstacleLayer
                );

                if (!blocked)
                {
                    Instantiate(
                        coin1Prefab,
                        spawnPosition,
                        Quaternion.identity
                    );

                    return;
                }
            }
        }

        Debug.LogWarning("Não foi possível encontrar um local válido para spawnar a moeda.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(
            transform.position,
            spawnArea
        );
    }
}