using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float destroyTime;
    [SerializeField] float timer;
    void Start()
    {
        
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= destroyTime) Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
