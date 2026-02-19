using UnityEngine;
using System.Linq;
public class GameOverScript : MonoBehaviour
{

    public GameObject gameOverUI;
    [SerializeField] Towers towers;
    
    
    void Update()
    {
       if(towers.towers.All(obj => obj == null))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
