using UnityEngine;

public class WinGame : MonoBehaviour
{
    [SerializeField] GameObject gameWonUI;
    [SerializeField] EnemyCount enemyCount;
    
    
    void Update()
    {
       if (enemyCount.enemyCount >= enemyCount.maxEnemy) 
       {
            Time.timeScale = 0f;
            gameWonUI.SetActive(true);
        }
    }
}
