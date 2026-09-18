using UnityEngine;
using UnityEngine.AI;

// fica no PREFAB do inimigo, precisa ter o navmesh agent junto tambem
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public float updateInterval = 0.3f;
    public int contactDamage = 10;
    public float damageCooldown = 1f;

    private NavMeshAgent agent;
    private Transform player;
    private float nextUpdateTime;
    private float lastDamageTime = -999f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        if (Time.time >= nextUpdateTime)
        {
            agent.SetDestination(player.position);
            nextUpdateTime = Time.time + updateInterval;
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time < lastDamageTime + damageCooldown) return;

        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(contactDamage);
            lastDamageTime = Time.time;
        }
    }
}