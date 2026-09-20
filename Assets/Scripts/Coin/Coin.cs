using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] float floatHeight = 0.5f;
    [SerializeField] float floatSpeed = 2f;

    [Header("Rotação")]
    [SerializeField] float rotationSpeed = 180f;

    Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = new Vector3(
            startPosition.x,
            newY,
            startPosition.z
        );

        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttack player = other.GetComponent<PlayerAttack>();

            if (player != null)
            {
                player.SetCoinCollected();
            }

            Destroy(gameObject);
        }
    }
}