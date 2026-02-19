using UnityEngine;

public class ContinueGameButton : MonoBehaviour
{
    EnemyCount enemyCounter;
    Towers towers;
    
    WinGame winGame;

    [SerializeField] EnemySpawner enemySpawner;
    public void Continue()
    {
        towers.DestroyTowers();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        winGame.gameWonUI.SetActive(false);
        Time.timeScale = 1;
        towers.SpawnTower();
        for(int i = 0; i < enemySpawner.enemyList.Count; i++)
        {
            Destroy(enemySpawner.enemyList[i]);
        }
        enemySpawner.enemyList.Clear();
        
          
    }
    
    void Start()
    {
        enemyCounter = GameObject.FindGameObjectWithTag("EnemyCounter").GetComponent<EnemyCount>();
        towers = GameObject.FindGameObjectWithTag("Towers").GetComponent<Towers>();
        winGame = GameObject.FindGameObjectWithTag("UIManager").GetComponent<WinGame>();
    }

    void Update()
    {
        
    }
}
