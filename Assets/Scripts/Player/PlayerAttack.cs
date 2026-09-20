
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    bool hasCoin = false;

    [Header("Ataque")]
    public float attackRange = 8f;
    public float attackInterval = 1f;
    public GameObject projectilePrefab;
    public GameObject projectile2Prefab;
    public GameObject coinPrefab;
    public Transform firePoint;
    public LayerMask enemyLayer;

    private float nextAttackTime;

    Transform FindNearestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            attackRange,
            enemyLayer
        );

        Transform nearest = null;
        float nearestDist = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            float dist = Vector3.Distance(
                transform.position,
                hit.transform.position
            );

            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = hit.transform;
            }
        }

        return nearest;
    }

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

    public void SetCoinCollected()
    {
        hasCoin = true;
    }

    public void Shoot(Transform target)
    {
        if (hasCoin)
        {
            SpecialAttack(target);
            hasCoin = false;
        }
        else
        {
            NormalAttack(target);
        }
    }

    void NormalAttack(Transform target)
    {
        Vector3 spawnPos = firePoint != null
            ? firePoint.position
            : transform.position + Vector3.up;

        GameObject proj = Instantiate(
            projectilePrefab,
            spawnPos,
            Quaternion.identity
        );

        proj.GetComponent<Projectile>().SetTarget(target);
    }

    void SpecialAttack(Transform target)
    {
        Vector3 spawnPos = firePoint != null
            ? firePoint.position
            : transform.position + Vector3.up;

        GameObject proj = Instantiate(
            projectile2Prefab,
            spawnPos,
            Quaternion.identity
        );

        proj.GetComponent<Projectile>().SetTarget(target);
    }
}