using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using FirstGearGames.SmoothCameraShaker;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public MenuController menuController;
    public ShakeData enemyShakeData;
    public TextMeshProUGUI countText;
    public GameObject finalPhase;
    public Vector3 oppositeDirection;
    public bool playerMove;
    public bool onGround;
    public float speed;
    private Rigidbody rb;
    [SerializeField] private int count;
    private float movementX;
    private float movementY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuController = GameObject.Find("MenuUI").GetComponent<MenuController>();

        rb = GetComponent<Rigidbody>();

        count = 0;
        
        finalPhase.SetActive(false);

        SetCountText();

        AudioManager.Instance.ChangeMusic("Phase1Music");
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 MovementVector = movementValue.Get<Vector2>();

        movementX = MovementVector.x;
        movementY = MovementVector.y;

        playerMove = true;
    }

    void FinalPhase()
    {
        menuController.AccessMenu(MenuController.MenuActivate.Pause);
        finalPhase.SetActive(true);
        AudioManager.Instance.ChangeMusic("Phase3Music");
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 9)
        {
            menuController.AccessMenu(MenuController.MenuActivate.Win);
            AudioManager.Instance.ChangeMusic("WinnerMusic");
        }else if (count == 8)
        {
            FinalPhase();
        }
    }

    void FixedUpdate()
    {
        if (onGround)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            oppositeDirection = -movement + gameObject.transform.position;

            rb.AddForce(movement * speed);
        }
        if (transform.position.y < 0)
        {
            menuController.AccessMenu(MenuController.MenuActivate.Lose);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            menuController.AccessMenu(MenuController.MenuActivate.Lose);
            CameraShakerHandler.Shake(enemyShakeData);
            Destroy(gameObject);
        }
    }

    //Detect if player has collected itens
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            count = count + 1;
            other.gameObject.SetActive(false);
            SetCountText();
        }
    }
}
