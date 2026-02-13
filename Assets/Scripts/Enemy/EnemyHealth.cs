using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    RagdollManager ragdollManager;

    public float health = 100;

    [HideInInspector] public bool isDead;
    [HideInInspector] Animator animator;
    [HideInInspector] EnemyMovement enemyMovement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        ragdollManager = GetComponent<RagdollManager>();
    }

   public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            Debug.Log(health);
        }
        else if (health <= 0) EnemyDeath();
    }

    void EnemyDeath()
    {
        animator.enabled = false;
        enemyMovement.agent.speed = 0;
        ragdollManager.EnableRagdoll();
        Debug.Log("Enemy Dead");
        Destroy(gameObject, 10f);
    }
}
