using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyMovement : MonoBehaviour
{
    public Towers towers;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public DetectionStateManager detection;
    public GameObject player;
    TowersHealth health;
    EnemyHealth enemyHealth;
    int enemySpeed = 6;
    [SerializeField] int damage = 5;
    [HideInInspector] Transform tower;
    GameObject currentTarget;


    //states
    public EnemyBaseState currentState;
    public EnemyDefaultState defaultState = new EnemyDefaultState();
    public EnemyNoticePlayerState noticePlayerState = new EnemyNoticePlayerState();
    public EnemyBeatingState beatingState = new EnemyBeatingState();
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        detection = GetComponent<DetectionStateManager>();
        enemyHealth = GetComponent<EnemyHealth>();
        // health = GameObject.FindGameObjectWithTag("Tower").GetComponent<TowersHealth>();
        //PickDestination();
        //ChangeState(defaultState);
    }

    void Start()
        {
            if (towers == null)
                towers = GameObject.FindGameObjectWithTag("Towers").GetComponent<Towers>();

            if (towers != null)
            {
                PickDestination();
                ChangeState(defaultState);
            }
        }
  
    public void ChangeState(EnemyBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    bool HasArrived()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    void Update()
    {
        if (currentState != null) currentState.UpdateState(this);
        if(currentTarget == null) PickDestination();
        //if (detection.PlayerSeen()) ChasePlayer();
        //else PickDestination();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tower"))
            {
                agent.updateRotation = false;
                health = other.GetComponent<TowersHealth>();
                agent.speed = 0;
                Debug.Log("Tower hit");
                ChangeState(beatingState);
                tower = other.transform;
                transform.LookAt(tower);
            }
    }
    public void OnAttackAnimationEnd()
    {

        if (ShouldBeatAgain()) 
        {
            animator.SetTrigger("Punch");
        }
        else 
        {
            PickDestination();
            agent.speed = enemySpeed;
            agent.updateRotation = true;
            ChangeState(defaultState);
        } 

    }

   

    public void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    void PickDestination()
    {
        if(towers.towers.Count == 0)
        {
            animator.SetTrigger("spotPlayer");
            agent.ResetPath();
            agent.isStopped = true;
            //health = towers.towers[towers.index].GetComponent<TowersHealth>();
            currentTarget = null;
            return;
        }
        int randomIndex = Random.Range(0, towers.towers.Count);
        currentTarget = towers.towers[randomIndex];

        NavMeshHit hit;
        Vector3 finalPosition = currentTarget.transform.position;

        if (NavMesh.SamplePosition(finalPosition, out hit, 4f, 1))
            finalPosition = hit.position;

        agent.isStopped = false;
        agent.SetDestination(finalPosition);
    }

    public void Atack()
    {
        health.health -= damage;
    }

    public bool ShouldBeatAgain()
    {
        return health.health > 0 && enemyHealth.health > 0;
    }

}
