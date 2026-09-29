using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : SeeTarget
{
    [Header("EnemyMovement CLASS")]
    protected PlayerController playerController;
    protected Transform targetTransform;
    protected private Animator animator;
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
            StartPursue();
        else
            MakeDetour();
    }

    /// <summary>
    /// chase the player if player move
    /// </summary>
    protected virtual void StartPursue()
    {
        if (sawEnemy)
        {
            FindTargetTransform(targetObject);

            if (IsTargetMoving(targetTransform.gameObject) && !startChasing)
                startChasing = true;

            if (targetTransform && startChasing)
            {
                if (navMeshAgent.enabled)
                    navMeshAgent.SetDestination(targetTransform.position);
            }
        }
        else
        {
            Debug.Log(gameObject.name + " cant see the target");
            targetTransform = null;
        }
    }

    /// <summary>
    /// look for target transform on scene
    /// </summary>
    /// <param name="myTargetReference">Reference of target prefab, not the object in scene</param>
    void FindTargetTransform(GameObject myTargetPrefab)
    {
        //if (!playerController || !targetTransform)
        if (!targetTransform)
        {
            if (GameObject.FindGameObjectWithTag(myTargetPrefab.tag))
            {
                //playerController = GameObject.FindGameObjectWithTag(TargetPrefab.tag).GetComponent<PlayerController>();
                targetTransform = GameObject.FindGameObjectWithTag(myTargetPrefab.tag).transform;
            }
            else Debug.Log("Cant find any transform to pursue in " + gameObject.name);
        }
    }

    /// <summary>
    /// Check if the 
    /// </summary>
    /// <param name="myTargetPrefab"></param>
    /// <returns></returns>
    bool IsTargetMoving(GameObject myTargetPrefab)
    {
        GameObject targetInScene;
        Rigidbody targetRigidbody;
        float targetCurrentSpeed = 0;

        //search for target prefab in scene
        if (GameObject.FindGameObjectWithTag(myTargetPrefab.tag))
        {
            targetInScene = GameObject.FindGameObjectWithTag(myTargetPrefab.tag);

            if (targetInScene.GetComponent<Rigidbody>())
            {
                targetRigidbody = targetInScene.GetComponent<Rigidbody>();
                targetCurrentSpeed = targetRigidbody.linearVelocity.magnitude;
            }
            else Debug.Log(gameObject.name + " Cant find any rigidbody attached in " + targetInScene.name);
        }
        else Debug.Log("Object cant be found to check movement " + gameObject.name);

        return targetCurrentSpeed > 0;
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
