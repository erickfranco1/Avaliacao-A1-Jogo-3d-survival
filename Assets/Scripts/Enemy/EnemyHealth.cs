using UnityEngine;
using UnityEngine.Events;

// Anexe no PREFAB do inimigo.
public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;

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
        //efeito de partícula/som antes de destruir pode vir aquiu
        Destroy(gameObject);
    }
}