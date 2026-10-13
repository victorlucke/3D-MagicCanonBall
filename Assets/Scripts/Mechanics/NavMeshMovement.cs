using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : SeeTarget
{
    [Header("EnemyMovement CLASS")]
    protected PlayerController playerController;
    //protected Transform targetTransform;
    protected private Animator animator;
    public Transform NewDiversionDestination { get { return _newDiversionDestination; } set { _newDiversionDestination = value; } }
    private Transform _newDiversionDestination;
    private NavMeshAgent navMeshAgent;
    private bool startChasing;


    protected override void Awake()
    {
        base.Awake();

        if (GetComponent<NavMeshAgent>())
            navMeshAgent = GetComponent<NavMeshAgent>();
        else Debug.Log(gameObject.name + " dont have NavMesh");
        if (GetComponentInChildren<Animator>())
            animator = GetComponentInChildren<Animator>();
        else Debug.Log(gameObject.name + " dont have animator");
    }

    // Update is called once per frame
    protected override void Update()
    {
        LookAtTarget();

        SetMovementAnimation();

        if (!_newDiversionDestination)
            TryPursueTarget(currentTargetDetected);
        else
            MakeDetour(_newDiversionDestination);
    }

    /// <summary>
    /// Pursue target if can he can see it
    /// </summary>
    /// <param name="myTargetTransform"></param>
    protected virtual void TryPursueTarget(GameObject myTargetObj)
    {
        Transform myTargetTransform;

        myTargetTransform = null;

        if (myTargetObj) myTargetTransform = myTargetObj.transform;

        if (sawEnemy && myTargetTransform)
        {
            if (IsTargetMoving(myTargetTransform.gameObject) && !startChasing)
                startChasing = true;

            if (myTargetTransform && startChasing)
            {
                if (navMeshAgent.enabled)
                    navMeshAgent.SetDestination(myTargetTransform.position);
            }
        }
    }

    /// <summary>
    /// look for target transform on scene
    /// </summary>
    /// <param name="myTargetReference">Reference of target prefab, not the object in scene</param>
    protected Transform FindTarget(GameObject myTargetPrefab)
    {
        Transform myTargetTransform = myTargetPrefab.transform;
        //if (!playerController || !targetTransform)
        if (!myTargetTransform)
        {
            if (GameObject.FindGameObjectWithTag(myTargetPrefab.tag))
            {
                //playerController = GameObject.FindGameObjectWithTag(TargetPrefab.tag).GetComponent<PlayerController>();
                return GameObject.FindGameObjectWithTag(myTargetPrefab.tag).transform;
            }
            else Debug.Log("Cant find any transform to pursue in " + gameObject.name);
        }

        return null;
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
    protected virtual void MakeDetour(Transform myNewDestination)
    {
        if (navMeshAgent.enabled)
            navMeshAgent.SetDestination(myNewDestination.position);

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
