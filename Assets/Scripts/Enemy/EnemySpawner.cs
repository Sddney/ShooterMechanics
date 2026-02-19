using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 10;
    [SerializeField] Transform pos1, pos2;
    public int maxEnemy = 15;

    //EnemyHealth enemyHealth;

    public List<GameObject> enemyList = new List<GameObject>();
    
    void Update()
    {
        spawnInterval -= Time.deltaTime;
        if (spawnInterval <= 0)
        {
            GameObject enemy1 = Instantiate(enemyPrefab, pos1.position, Quaternion.identity);
            GameObject enemy2 = Instantiate(enemyPrefab, pos2.position, Quaternion.identity);
            
            spawnInterval = 5;

            enemyList.Add(enemy1);
            enemyList.Add(enemy2);
        }
        
    }
}
