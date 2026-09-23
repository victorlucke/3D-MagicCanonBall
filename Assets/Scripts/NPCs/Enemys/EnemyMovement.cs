using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("ENEMY MOVEMENT CLASS")]
    [Header("Movement Controller")]
    public Transform NewDiversionDestination
    {
        get { return _newDiversionDestination; }
        set { _newDiversionDestination = value; }
    }
    protected PlayerController playerController;
    protected Transform playerTransform;
    protected private Animator animator;
    protected bool sawEnemy;
    [SerializeField] private Transform _newDiversionDestination;
    private NavMeshAgent navMeshAgent;
    private bool startChasing;
    

    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        sawEnemy = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AnimatorSawEnemy("SawEnemy", sawEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_newDiversionDestination)
            StartPursue(playerController.playerMove);
        else
            MakeDetour();

    }

    /// <summary>
    /// chase the player if player move
    /// </summary>
    protected virtual void StartPursue(bool isMoved)
    {
        if (isMoved && !startChasing)
            startChasing = true;

        if (playerTransform && startChasing)
        {
            float currentSpeed = navMeshAgent.velocity.magnitude;

            AnimatorCurrentSpeed("Speed", currentSpeed);

            if (navMeshAgent.enabled)
                navMeshAgent.SetDestination(playerTransform.position);
        }
    }

    /// <summary>
    /// Detor from the player chase to go toward another position case it exist in '_newDiversionDestination'
    /// </summary>
    protected virtual void MakeDetour()
    {
        if (_newDiversionDestination)
        {
            float currentSpeed = navMeshAgent.velocity.magnitude;

            AnimatorCurrentSpeed("Speed", currentSpeed);

            if (navMeshAgent.enabled)
                navMeshAgent.SetDestination(_newDiversionDestination.position);
        }
    }

    /// <summary>
    /// Change the parameter Speed On Animator
    /// </summary>
    /// <param name="currentSpeed">the speed.magnitude of the object</param>
    protected virtual void AnimatorCurrentSpeed(string parameterName, float currentSpeed)
    {
        if (animator != null)
            animator.SetFloat(parameterName, currentSpeed);
    }

    /// <summary>
    /// Change the parameter SawEnemy On Animator
    /// </summary>
    /// <param name="isSaw"></param>
    protected virtual void AnimatorSawEnemy(string parameterName, bool isSaw)
    {
        if (animator != null)
            animator.SetBool(parameterName, isSaw);
    }
}
