using UnityEngine;
using UnityEngine.Events;
using Unity.Cinemachine;

// Anexe no GameObject "Player".
public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Feedback visual (opcional)")]
    public Renderer[] renderersParaFlash;
    public float flashDuration = 0.15f;
    public Color flashColor = Color.red;

    [Header("Feedback de câmera (opcional)")]
    public CinemachineImpulseSource impulseSource; 

    [Header("Eventos (conecte no Inspector)")]
    public UnityEvent<int, int> onHealthChanged;
    public UnityEvent onDeath;

    private Color[] originalColors;

    void Awake()
    {
        currentHealth = maxHealth;

        if (renderersParaFlash != null && renderersParaFlash.Length > 0)
        {
            originalColors = new Color[renderersParaFlash.Length];
            for (int i = 0; i < renderersParaFlash.Length; i++)
                originalColors[i] = renderersParaFlash[i].material.color;
        }
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        onHealthChanged?.Invoke(currentHealth, maxHealth);

        if (renderersParaFlash != null && renderersParaFlash.Length > 0)
            StartCoroutine(FlashRoutine());

        if (impulseSource != null)
            impulseSource.GenerateImpulse();

        if (currentHealth <= 0)
            onDeath?.Invoke();
    }

    System.Collections.IEnumerator FlashRoutine()
    {
        for (int i = 0; i < renderersParaFlash.Length; i++)
            renderersParaFlash[i].material.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        for (int i = 0; i < renderersParaFlash.Length; i++)
            renderersParaFlash[i].material.color = originalColors[i];
    }
}