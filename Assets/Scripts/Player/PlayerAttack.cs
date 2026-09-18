using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ataque")]
    public float attackRange = 8f;
    public float attackInterval = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;   
    public LayerMask enemyLayer;

    private float nextAttackTime;

    void Update()
    {
        if (Time.time < nextAttackTime) return;

        Transform nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            Shoot(nearestEnemy);
            nextAttackTime = Time.time + attackInterval;
        }
    }

    Transform FindNearestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        Transform nearest = null;
        float nearestDist = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            float dist = Vector3.Distance(transform.position, hit.transform.position);
            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = hit.transform;
            }
        }
        return nearest;
    }

    void Shoot(Transform target)
    {
        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position + Vector3.up;
        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        proj.GetComponent<Projectile>().SetTarget(target);
    }

    // desenha o alcance de ataque no Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}