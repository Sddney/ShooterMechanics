using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System.Collections.Generic;

public class Towers : MonoBehaviour
{

    [HideInInspector] public List<GameObject> towers = new List<GameObject>();
    [SerializeField] GameObject towerPrefab;
    [SerializeField] Transform[] spawnLocations;
    public int index = 4;
    [SerializeField] GameObject enemy;
    Animator anim;
    [HideInInspector] public bool spawned = false;
    [SerializeField] UIDisplay ui;
  

    void Start()
    {
        anim = enemy.GetComponent<Animator>();
        SpawnTower();
    }

    public void SpawnTower()
    {
        for(int i = 0; i < spawnLocations.Length; i++)
        {
            GameObject t = Instantiate(towerPrefab, spawnLocations[i].position, Quaternion.identity);
            towers.Add(t);

            ui.towersHealth.Add(t.GetComponent<TowersHealth>());
        }
    }

    public void DestroyTowers()
    {
        for(int i = 0; i < towers.Count; i++)
        {
            Destroy(towers[i]);
        }
        towers.Clear();
        ui.towersHealth.Clear();
    }
    public Vector3 GetFinalDestination()
    {   
        if(towers.Count == 0) return transform.position;  
        index = Random.Range(0, towers.Count);
        NavMeshHit hit;
        Vector3 finalPosition = towers[index].transform.position;
        if(NavMesh.SamplePosition(towers[index].transform.position, out hit, 4f, 1)) finalPosition = hit.position;
    
        return finalPosition;

        
    }
}
