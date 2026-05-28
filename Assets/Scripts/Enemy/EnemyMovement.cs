using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public PlayerController playerController;
    public Transform player;
    public float speed;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool sawEnemy;
    private bool startChasing;

    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        player = GameObject.Find("Player").gameObject.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        sawEnemy = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator != null)
            animator.SetBool("SawEnemy", sawEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        StartPursue();
    }

    void StartPursue()
    {
        if (playerController.playerMove && !startChasing)
            startChasing = true;

        if (player != null && startChasing)
        {
            float currentSpeed = navMeshAgent.velocity.magnitude;
            if (animator != null)
                animator.SetFloat("Speed", currentSpeed);
            navMeshAgent.SetDestination(player.position);
        }
    }
}
