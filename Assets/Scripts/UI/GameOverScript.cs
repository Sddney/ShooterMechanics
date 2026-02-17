using UnityEngine;
using System.Linq;
public class GameOverScript : MonoBehaviour
{

     [SerializeField] GameObject gameOverUI;
    [SerializeField] Towers towers;
    
    
    void Update()
    {
       if(towers.towers.All(obj => obj == null))
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
