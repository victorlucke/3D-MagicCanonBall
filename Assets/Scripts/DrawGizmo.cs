using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    
    public float range;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, range);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
