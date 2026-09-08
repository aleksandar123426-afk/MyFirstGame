using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public GameObject deathEffect;
    public bool destroyOnDeath = true;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
            return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        if (destroyOnDeath)
            Destroy(gameObject);
    }
}
