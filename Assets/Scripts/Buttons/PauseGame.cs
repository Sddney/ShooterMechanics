using UnityEngine;

public class PauseGame : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseMenu;
    public bool isPaused = false;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = isPaused ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = isPaused ? false : true;
            Time.timeScale = pauseMenu.activeSelf ? 1f : 0f;
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            isPaused = !isPaused;
        }
    }
}
