using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float destroyTime;
    [HideInInspector] public WeaponManager weapon;
    [HideInInspector] public Vector3 direction;

    void Start()
    {
        Destroy(this.gameObject, destroyTime); 
    }

    
    

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<EnemyHealth>())
        {
            EnemyHealth enemy = collision.gameObject.GetComponentInParent<EnemyHealth>();
            enemy.TakeDamage(weapon.damage);

            if(enemy.health <= 0 && enemy.isDead == false)
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(direction * weapon.enemyKickBack, ForceMode.Impulse);
                enemy.isDead = true;
            }

        }
        Destroy(this.gameObject);
    }
}
