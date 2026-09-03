using UnityEngine;

public abstract class CollectObject : MonoBehaviour
{
    protected abstract float IncrementValue();
    
    //Detect if player has collected itens
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IncrementValue();
            gameObject.SetActive(false);
            //GameManager.Instance.AddCount();
        }
    }
}
