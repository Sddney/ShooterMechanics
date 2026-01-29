using UnityEditor.PackageManager;
using UnityEngine;

public class DetectionStateManager : MonoBehaviour
{

    [SerializeField] float lookDistance =  30, fov = 120;
    [SerializeField] Transform enemyEyes;
    Transform playerHead;

    [SerializeField] GameManager gameManager;

    
    void Start()
    {

        playerHead = gameManager.playerHead;
    }

    
    private void FixedUpdate()
    {
        if(PlayerSeen())
        {
            Debug.Log("Player Spotted");
        }
        else Debug.Log("Player Not Spotted");
    }

    public bool PlayerSeen()
    {
        if(Vector3.Distance(enemyEyes.position, playerHead.position) > lookDistance) return false;

        Vector3 dirToPlayer = (playerHead.position - enemyEyes.position).normalized;

        float angleToPlayer = Vector3.Angle(enemyEyes.parent.forward, dirToPlayer);

        if(angleToPlayer > (fov/2)) return false;

        enemyEyes.LookAt(playerHead.position);

        RaycastHit hit; 
        if(Physics.Raycast(enemyEyes.position, enemyEyes.forward, out hit, lookDistance))
        {
            if(hit.transform == null) return false;
            if(hit.transform.root == playerHead.root)
            {
                Debug.DrawLine(enemyEyes.position, hit.point, Color.red);
                return true;
            }
        }
        return false;
    }
}
