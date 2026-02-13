using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Towers : MonoBehaviour
{

    public GameObject[] towers;
    public int index;
    [SerializeField] GameObject enemy;
    Animator anim;
    void Start()
    {
        anim = enemy.GetComponent<Animator>();
    }
    public Vector3 GetFinalDestination()
    {
        index = Random.Range(0, towers.Length);
        if(towers.All(obj => obj == null)) return enemy.transform.position;
        
        
        int safe = 0;
        do
        {
            index = Random.Range(0, towers.Length);
            safe++;
        }
        while(towers[index] == null && safe < 100);
        if(towers[index] == null)
        {
            anim.SetTrigger("spotPlayer");
            return enemy.transform.position;
        }
            NavMeshHit hit;
            Vector3 finalPosition = towers[index].transform.position;
            if(NavMesh.SamplePosition(towers[index].transform.position, out hit, 4f, 1)) finalPosition = hit.position;
            return finalPosition;
        
    }
}
