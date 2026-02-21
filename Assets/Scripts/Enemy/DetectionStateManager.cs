using UnityEngine;

public class DetectionStateManager : MonoBehaviour
{

    [SerializeField] float lookDistance =  30, fov = 120;
    [SerializeField] Transform enemyEyes;
    [SerializeField] GameObject playerHead;

    [SerializeField] GameManager gameManager;



    public bool PlayerSeen()
    {
        if(Vector3.Distance(enemyEyes.position, playerHead.transform.position) > lookDistance) return false;

        Vector3 dirToPlayer = (playerHead.transform.position - enemyEyes.position).normalized;

        float angleToPlayer = Vector3.Angle(enemyEyes.parent.forward, dirToPlayer);

        if(angleToPlayer > (fov/2)) return false;

        enemyEyes.LookAt(playerHead.transform.position);

        RaycastHit hit; 
        if(Physics.Raycast(enemyEyes.position, enemyEyes.forward, out hit, lookDistance))
        {
            if(hit.transform == null) return false;
            if(hit.transform.root == playerHead.transform.root)
            {
                Debug.DrawLine(enemyEyes.position, hit.point, Color.red);
                return true;
            }
        }
        return false;
    }
}
