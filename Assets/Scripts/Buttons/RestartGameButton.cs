using UnityEngine;

public class RestartGameButton : MonoBehaviour
{
    
    [SerializeField] EnemyCount enemyCount;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Towers towers;
    [SerializeField] EnemySpawner enemySpawner;
    public void RestartGame()
    {
        towers.DestroyTowers();
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        enemyCount.maxEnemy = enemyCount.orgMaxEnemy;
        towers.SpawnTower();
        for(int i = 0; i < enemySpawner.enemyList.Count; i++)
        {
            Destroy(enemySpawner.enemyList[i]);
        }
        enemySpawner.enemyList.Clear();
    }
}
