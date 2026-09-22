using UnityEngine;
using UnityEngine.Events;

// Anexe no PREFAB do inimigo.
public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;

    [Header("Efeito de morte")]
    public GameObject deathEffectPrefab; 
    public float deathEffectLifetime = 2f; 

    public static UnityEvent onAnyEnemyDeath = new UnityEvent();

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        onAnyEnemyDeath.Invoke();

        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectLifetime);
        }

        Destroy(gameObject);
    }
}