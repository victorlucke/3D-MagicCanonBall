using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public PlayerController playerController;
    public Transform player;
    public float speed;
    private NavMeshAgent navMeshAgent;

    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && playerController.playerMove)
        {
            navMeshAgent.SetDestination(player.position);
        }
        
    }
}
