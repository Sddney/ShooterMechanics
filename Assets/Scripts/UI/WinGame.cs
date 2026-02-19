using UnityEngine;

public class WinGame : MonoBehaviour
{
    public GameObject gameWonUI;
    [SerializeField] EnemyCount enemyCount;
    
    Towers towers;

    void Start()
    {
        towers = towers = GameObject.FindGameObjectWithTag("Towers").GetComponent<Towers>();
    }
    
    void Update()
    {
       if (enemyCount.enemyCount >= enemyCount.maxEnemy) 
       {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            gameWonUI.SetActive(true);
            enemyCount.enemyCount = 0;
            enemyCount.maxEnemy += 5;
        }
    }
}
