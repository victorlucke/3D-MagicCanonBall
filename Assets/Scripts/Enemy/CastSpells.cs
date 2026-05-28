using UnityEngine;
using UnityEngine.InputSystem;

public class CastSpells : MonoBehaviour
{
    public GameObject knowingSpell;
    private GameObject castSlot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartAction();
    }
    
    void StartAction()
    {
        InputAction interactAction = InputSystem.actions.FindAction("Interact");

        if(interactAction.IsPressed())
            CastMagic(knowingSpell);
    }

    void CastMagic(GameObject magic)
    {
        Transform playerTransform = GameObject.FindWithTag("Player").gameObject.transform;

        if(playerTransform != null && castSlot == null)
        {
            castSlot = Instantiate(magic, playerTransform.position, magic.transform.rotation);
        }
    }
}
