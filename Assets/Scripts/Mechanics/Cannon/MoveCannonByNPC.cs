using UnityEngine;

public class MoveCannonByNPC : MonoBehaviour
{
    [Header("MoveByNPC Class")]
    [SerializeField] private MoveCannon ParentCannonToRotate;
    [SerializeField] private GameObject npcAllowedToMove;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(npcAllowedToMove.tag))
        {
            Debug.Log("Collided");
            ParentCannonToRotate.Rotate(0, 0, -1);
        }
    }
}
