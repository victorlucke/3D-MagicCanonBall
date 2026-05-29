using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public PlayerController playerController;
    public Transform playerTransform;
    public float speed;
    private NavMeshAgent navMeshAgent;
    protected private Animator animator;
    protected bool sawEnemy;
    private bool startChasing;

    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerTransform = GameObject.Find("Player").gameObject.transform;
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
        StartPursue();
    }

    protected virtual void StartPursue()
    {
        if (playerController.playerMove && !startChasing)
            startChasing = true;

        if (playerTransform != null && startChasing)
        {
            float currentSpeed = navMeshAgent.velocity.magnitude;

            AnimatorCurrentSpeed("Speed", currentSpeed);

            navMeshAgent.SetDestination(playerTransform.position);
        }
    }

    /// <summary>
    /// Change the parameter Speed On Animator
    /// </summary>
    /// <param name="currentSpeed">the speed.magnitude of the object</param>
    protected virtual void AnimatorCurrentSpeed(string parameterName,float currentSpeed)
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
