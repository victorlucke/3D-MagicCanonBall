using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : SeeTarget
{
    [Header("EnemyMovement CLASS")]
    protected PlayerController playerController;
    protected Transform playerTransform;
    protected private Animator animator;
    //protected bool sawEnemy;
    public Transform NewDiversionDestination
    {
        get { return _newDiversionDestination; }
        set { _newDiversionDestination = value; }
    }
    private Transform _newDiversionDestination;
    private NavMeshAgent navMeshAgent;
    private bool startChasing;


    protected virtual void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        StartSeeTarget();
    }

    // Update is called once per frame
    protected override void Update()
    {
        LookAtTarget();

        SetMovementAnimation();

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
        if (sawEnemy)
        {
            if (isMoved && !startChasing)
                startChasing = true;

            if (playerTransform && startChasing)
            {
                if (navMeshAgent.enabled)
                    navMeshAgent.SetDestination(playerTransform.position);
            }
        }
        else Debug.Log(gameObject.name + " cant see the target");
    }

    /// <summary>
    /// Check current speed to change animation acorndly
    /// </summary>
    private void SetMovementAnimation()
    {
        float currentSpeed = navMeshAgent.velocity.magnitude;

        AnimatorCurrentSpeed("Speed", currentSpeed);
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
