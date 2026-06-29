using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// UPDATE ****************
/// remover icone de interacao do script canhao, criar um novo so para interagir
/// remover disparar o canhao como acao e colocar como timer. atira apos xSegundos
/// </summary>
public class Cannon : BasicFunctionalities
{
    public EscolhaEvento evento;
    public Transform CannonMouthTransform;
    public GameObject interactIcon;
    public float shootStrenght;
    private InputAction interactAction;
    private InputAction fireAction;
    private bool isReloaded;
    [SerializeField] private GameObject loadedObject;

    void Awake()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        fireAction = InputSystem.actions.FindAction("Attack");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RemoveAsInteractable();
    }

    void FixedUpdate()
    {
        if (loadedObject != null)
        {
            if (fireAction.IsPressed())
            {
                ShootCannon(loadedObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        MarkAsInteractable();
    }

    void OnTriggerStay(Collider other)
    {
        if (interactAction.IsPressed())
            ReloadCannon(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        RemoveAsInteractable();
    }

    /// <summary>
    /// show the icon indicating you can interact with
    /// </summary>
    void MarkAsInteractable()
    {
        interactIcon.SetActive(true);
    }

    /// <summary>
    /// remove the icon indicating you can interact with
    /// </summary>
    void RemoveAsInteractable()
    {
        interactIcon.SetActive(false);
    }

    /// <summary>
    /// Start to process to shoot the object
    /// </summary>
    /// <param name="ammunition">object to load the cannon</param>
    public void ReloadCannon(GameObject ammunition)
    {
        if (!isReloaded)
        {
            if (VerifyShootable(ammunition))
            {
                ChangeKinematic(ammunition, true);
                PositionAmmunition(ammunition, true);

                if (CannonMouthTransform.Find(ammunition.name))
                {
                    loadedObject = ammunition;
                    isReloaded = true;
                }
            }
        }
    }

    void ShootCannon(GameObject ammo)
    {
        if (isReloaded)
        {
            Rigidbody ammoRigidbody;
            Vector3 shootDirection;

            ammoRigidbody = ammo.GetComponent<Rigidbody>();
            shootDirection = new Vector3(CannonMouthTransform.position.x, 0, 0);

            ChangeKinematic(ammo, false);
            PositionAmmunition(ammo, false);

            ammoRigidbody.AddForce(CannonMouthTransform.forward * shootStrenght, ForceMode.Impulse);

            evento.Invoke(ammo);

            PlaySoundEffect();

            loadedObject = null;
            isReloaded = false;
        }
    }

    /// <summary>
    /// Verify the tag of object and return if he can be shoot
    /// </summary>
    /// <param name="shootObject"></param>
    /// <returns>bool value</returns>
    bool VerifyShootable(GameObject shootObject)
    {
        bool isShootable;

        if (shootObject.CompareTag("Player"))
            isShootable = true;
        else
            isShootable = false;

        return isShootable;
    }

    /// <summary>
    ///  The Object you want to change kinematic true or false, to remove or add physics interaction
    /// </summary>
    /// <param name="motionAmmo">the object to change</param>
    /// <param name="isKinematic"></param>
    public void ChangeKinematic(GameObject motionAmmo, bool isKinematic)
    {
        Rigidbody ammoRigidbody;

        if (motionAmmo.GetComponent<Rigidbody>())
        {
            ammoRigidbody = motionAmmo.GetComponent<Rigidbody>();

            ammoRigidbody.isKinematic = isKinematic;
        }
    }

    /// <summary>
    /// position the object in the cannons mouth
    /// </summary>
    /// <param name="ammoOut">the object to reposition</param>
    /// <param name="isLockInPlace">if true the ammo is made a parent of cannon mouth and reset the position to 0
    /// else, remove the cannon mouth parent</param>
    void PositionAmmunition(GameObject ammoOut, bool isLockInPlace)
    {
        if (isLockInPlace)
        {
            ammoOut.transform.position = CannonMouthTransform.position;
            ammoOut.transform.SetParent(CannonMouthTransform);
        }
        else
        {
            ammoOut.transform.SetParent(null);
        }
    }
}
