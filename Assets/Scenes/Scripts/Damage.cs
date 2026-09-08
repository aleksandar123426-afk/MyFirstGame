using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth player = other.gameObject.GetComponent<playerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}