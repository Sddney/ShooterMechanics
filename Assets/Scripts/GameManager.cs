using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    void Awake()
    {
        Camera.main.gameObject.AddComponent<CinemachineBrain>();
    }
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    

}
