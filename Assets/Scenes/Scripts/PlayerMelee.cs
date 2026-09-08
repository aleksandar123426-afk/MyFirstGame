using UnityEngine;

public class PlayerMelle : MonoBehaviour
{
    public Transform attackOrigin;
    public float attackRadius = 1f;
    public LayerMask EnemyMask;

    public float colldownTime = 0.5f;
    private float cooldownTimer = 0f;

    public int attackDamage = 25;

    public Animator animator;
    private int meleeHash;

    private void Start()
    {
        meleeHash = Animator.StringToHash("Melle");
    }

    private void Update()
    {
        if (cooldownTimer <= 0)
        {
            if (Input.GetKey(KeyCode.K))
            {
                // Example of playing attack animation
                animator.SetTrigger(meleeHash);

                Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, EnemyMask);
                foreach (var enemy in enemiesInRange)
                {
                    enemy.GetComponent<HealthManager>().TakeDamage(attackDamage);
                }

                cooldownTimer = colldownTime;
            }
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }
}