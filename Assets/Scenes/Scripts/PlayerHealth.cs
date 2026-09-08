using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    [Min(0f)]
    public float health = 100f;
    [Min(0.01f)]
    public float maxHealth = 100f;
    public Image healthBar;

    private bool isDead;

    public GameManagerScript gameManager;

    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindAnyObjectByType<GameManagerScript>();
        }

        maxHealth = Mathf.Max(maxHealth, 0.01f);
        health = Mathf.Clamp(health, 0f, maxHealth);
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (isDead || damage <= 0f)
        {
            return;
        }

        health = Mathf.Max(health - damage, 0f);
        UpdateHealthBar();

        if (health <= 0f)
        {
            isDead = true;
            if (gameManager != null)
            {
                gameManager.gameOver();
            }

            Debug.Log("Dead");
            Destroy(gameObject);
        }
    }

    public void Heal(float amount)
    {
        if (isDead || amount <= 0f)
        {
            return;
        }

        health = Mathf.Min(health + amount, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = health / maxHealth;
        }
    }
}