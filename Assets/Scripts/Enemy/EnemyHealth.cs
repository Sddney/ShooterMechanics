using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    RagdollManager ragdollManager;

    public float health = 100;

    [HideInInspector] public bool isDead;
    [HideInInspector] Animator animator;
    [HideInInspector] EnemyMovement enemyMovement;
    [HideInInspector] EnemyCount enemyCounter;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        ragdollManager = GetComponent<RagdollManager>();
        enemyCounter = GameObject.FindGameObjectWithTag("EnemyCounter").GetComponent<EnemyCount>();
    }

   public void TakeDamage(float damage)
    {
        if(isDead) return;
        health -= damage;
        Debug.Log(health);
        
        if (health <= 0)
        {
            isDead = true;
            EnemyDeath(); 
        } 
    }

    void EnemyDeath()
    {
        animator.enabled = false;
        enemyMovement.agent.speed = 0;
        ragdollManager.EnableRagdoll();
        enemyCounter.enemyCount++;
        isDead = true;
        Destroy(gameObject, 10f);
    }
}
