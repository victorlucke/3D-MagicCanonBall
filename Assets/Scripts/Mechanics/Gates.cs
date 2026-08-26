using UnityEngine;
using UnityEngine.AI;

public class Gates : MonoBehaviour
{
    private NavMeshObstacle navMeshObstacle;
    private GameObject objectPressurePlate;
    private Animator animator;

    void Awake()
    {
        objectPressurePlate = transform.Find("PlatePivot").transform.Find("PressurePlate").gameObject;

        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            CallPressurePlateAnimation();
    }

    void CallPressurePlateAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("PressPlate");
        }
    }

    public void CallOpenGateAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("OpenGate");
        }
    }
}
