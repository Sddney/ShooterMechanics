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
    [SerializeField] int damage = 20;
    [HideInInspector] Transform tower;

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
                towers = FindAnyObjectByType<Towers>();

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
            if(towers.towers.All(obj => obj == null)) 
            {
                animator.SetTrigger("spotPlayer");
                return;
            }
            agent.speed = enemySpeed;
            agent.updateRotation = true;
            ChangeState(defaultState);
            PickDestination();
        } 

    }

   

    public void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    void PickDestination()
    {
        agent.SetDestination(towers.GetFinalDestination());
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
