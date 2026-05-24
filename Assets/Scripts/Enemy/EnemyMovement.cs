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
    private static bool sawEnemy;

    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
        sawEnemy = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator.SetBool("SawEnemy", sawEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && playerController.playerMove)
        {
            float currentSpeed = navMeshAgent.velocity.magnitude;
            animator.SetFloat("Speed", currentSpeed);
            navMeshAgent.SetDestination(player.position);
        }
        
    }
}
