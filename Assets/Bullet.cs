using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float rotationSpeed = 220f;
    public int damagePerShot = 1;

    private Rigidbody2D rb;
    private Transform target;
    private Collider2D bulletCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<Collider2D>();

        if (bulletCollider != null)
        {
            bulletCollider.isTrigger = true;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            target = FindNearestWeakPoint();
        }

        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = transform.right * speed;
        }
    }

    private Transform FindNearestWeakPoint()
    {
        GameObject[] weakPoints = GameObject.FindGameObjectsWithTag("Weak Point");
        Transform nearest = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject weakPoint in weakPoints)
        {
            if (weakPoint == null)
                continue;

            float distance = Vector2.Distance(transform.position, weakPoint.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = weakPoint.transform;
            }
        }

        return nearest;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null)
            return;

        if (other.gameObject.CompareTag("Weak Point"))
        {
            KillWeakPoint(other.gameObject);
            return;
        }

        Destroy(gameObject);
    }

    private void KillWeakPoint(GameObject weakPoint)
    {
        EnemyHealth enemyHealth = weakPoint.transform.root.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            int clampedDamage = Mathf.Clamp(damagePerShot, 1, 1);
            enemyHealth.TakeDamage(clampedDamage);
        }
        else
        {
            Destroy(weakPoint);
        }

        Destroy(gameObject);
    }
}
