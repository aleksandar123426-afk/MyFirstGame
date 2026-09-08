using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = playerSpawnPoint.position;
        }
    }
}
