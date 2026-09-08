using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    [Tooltip("The current checkpoint the player will respawn at. If null, the start position is used.")]
    public Transform respawnPoint;

    [Tooltip("If the player falls below this Y position, they respawn.")]
    public float fallRespawnY = -10f;

    [Tooltip("Number of respawns allowed. Set to -1 for unlimited respawns.")]
    public int respawnLimit = -1;

    private int respawnCount = 0;
    private Vector3 startPosition;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        if (respawnPoint == null)
        {
            respawnPoint = transform;
        }
    }

    private void Update()
    {
        if (transform.position.y <= fallRespawnY)
        {
            Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            respawnPoint = other.transform;
            Debug.Log("Respawn point updated: " + other.name);
        }

        if (other.CompareTag("Enemy") || other.CompareTag("Hazard"))
        {
            Respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Hazard"))
        {
            Respawn();
        }
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint != null ? newRespawnPoint : transform;
    }

    public void Respawn()
    {
        if (respawnLimit != -1 && respawnCount >= respawnLimit)
        {
            Debug.Log("Respawn limit reached.");
            return;
        }

        Vector3 targetPosition = respawnPoint != null ? respawnPoint.position : startPosition;
        transform.position = targetPosition;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        respawnCount++;
        Debug.Log("Player respawned (" + respawnCount + ")");
    }
}
