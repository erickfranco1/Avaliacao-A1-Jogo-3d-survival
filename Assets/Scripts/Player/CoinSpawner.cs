using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] GameObject coinPrefab;
    [SerializeField] int minimumCoins = 2;

    [Header("Área de Spawn")]
    [SerializeField] Vector3 spawnArea = new Vector3(20f, 5f, 20f);

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
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f),
            Random.Range(-spawnArea.y / 2f, spawnArea.y / 2f),
            Random.Range(-spawnArea.z / 2f, spawnArea.z / 2f)
        );

        Instantiate(
            coinPrefab,
            transform.position + randomPosition,
            Quaternion.identity
        );
    }
}