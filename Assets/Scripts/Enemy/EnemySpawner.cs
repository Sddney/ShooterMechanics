using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 10;
    [SerializeField] Transform pos1, pos2;
    
    
    void Update()
    {
        spawnInterval -= Time.deltaTime;
        if (spawnInterval <= 0)
        {
            Instantiate(enemyPrefab, pos1.position, Quaternion.identity);
            Instantiate(enemyPrefab, pos2.position, Quaternion.identity);
            
            spawnInterval = 5;
        }
        
    }
}
