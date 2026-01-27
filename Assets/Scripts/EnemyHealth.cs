using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    RagdollManager ragdollManager;

    public float health = 100;

    [HideInInspector] public bool isDead;

    private void Start()
    {
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
        ragdollManager.EnableRagdoll();
        Debug.Log("Enemy Dead");
    }
}
