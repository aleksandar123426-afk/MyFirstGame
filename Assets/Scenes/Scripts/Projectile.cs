using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    private bool hasHit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit)
            return;

        if (collision == null)
            return;

        // Try to find an EnemyHealth or ShipHealth component on the collided object or its parents
        EnemyHealth enemyHealth = collision.GetComponentInParent<EnemyHealth>();
        if (enemyHealth != null)
        {
            hasHit = true;
            enemyHealth.TakeDamage(1);
            Destroy(gameObject);
            return;
        }

        ShipHealth shipHealth = collision.GetComponentInParent<ShipHealth>();
        if (shipHealth != null)
        {
            hasHit = true;
            shipHealth.TakeDamage(1);
            Destroy(gameObject);
            return;
        }

        // Fallback: if the collided object is tagged Enemy, destroy that object only
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasHit = true;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}