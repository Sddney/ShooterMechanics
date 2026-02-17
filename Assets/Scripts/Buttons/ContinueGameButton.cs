using UnityEngine;

public class ContinueGameButton : MonoBehaviour
{
    EnemyCount enemyCounter;
    Towers towers;
    void Continue()
    {
        for(int i = 0; i < towers.towers.Count; i++)
        {
            
        }

    }
    
    void Start()
    {
        enemyCounter = GameObject.FindGameObjectWithTag("EnemyCounter").GetComponent<EnemyCount>();
        towers = GameObject.FindGameObjectWithTag("Towers").GetComponent<Towers>();
    }

    void Update()
    {
        
    }
}
