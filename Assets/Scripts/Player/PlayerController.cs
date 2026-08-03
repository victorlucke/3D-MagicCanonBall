using UnityEngine;
using UnityEngine.InputSystem;
using FirstGearGames.SmoothCameraShaker;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Things to do: 
    /// mecher audioManager para tocar musica com base na phase, status do jogo, 
    /// update gameManager com phases para captar os sons, menus etc?
    /// update gameManager, alterar variavel GameStatus para Pause Unpause, Win Lose separados...
    /// mudar condicao de vitoria no gameManager para menuController acessar
    /// isso importa, porque voce e um programador, faca o codigo descente, NAO IMPORTA SE NINGUEM VAI VER!
    /// VOCE VAI FAZER E VAI CONSEGUIR DEIXAR ESSE CODIGO MELHOR AAAAAAAAAAAAA.
    /// pensar em terminar o level design? veremos....................
    /// </summary>
    //Necessary to play with it: GameManager, Ground, 
    public ShakeData enemyShakeData;
    public GameObject finalPhase;
    public Vector3 oppositeDirection;
    public bool playerMove;
    public bool playerMouseMove;
    public bool player;
    public bool onGround;
    public float speed;
    public float movementX;
    public float movementY;
    private Rigidbody rb;
    private Vector3 targetPosition;

    void Awake()
    {

        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finalPhase.SetActive(false);

        // SetCountText();

        //AudioManager.Instance.ChangeMusic("Phase1Music");
    }

    void OnEnable()
    {
        GameManager.OnGameOver += DestroyPlayer;
    }

    void OnDisable()
    {
        GameManager.OnGameOver -= DestroyPlayer;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

        if (movementX != 0 || movementY != 0)
            playerMove = true;
        else
            playerMove = false;

        //Debug.Log(playerMove);
    }

    void DetectMouseClick()
    {
        if (Pointer.current.press.isPressed)
        {
            string[] groundLayers = new string[3];
            Vector2 aimPosition;
            RaycastHit hit;
            Ray ray;
            LayerMask groundLayer;

            aimPosition = Pointer.current.position.ReadValue();
            ray = Camera.main.ScreenPointToRay(aimPosition);
            groundLayers[0] = "Wood";
            groundLayers[1] = "Stone";
            groundLayers[2] = "Sand";
            groundLayer = LayerMask.GetMask(groundLayers);

            Debug.DrawRay(ray.origin, ray.direction * 50, Color.yellow);

            if (Physics.Raycast(ray, out hit, 100, groundLayer))
            {
                targetPosition = hit.point;
                playerMouseMove = true;
            }
        }else 
            playerMouseMove = false;
    }

    // void FinalPhase()
    // {
    //     menuController.AccessMenu(MenuController.MenuActivate.Pause);
    //     finalPhase.SetActive(true);
    //     AudioManager.Instance.ChangeMusic("Phase3Music");
    // }

    // void SetCountText()
    // {
    //     countText.text = "Count: " + count.ToString();
    //     if (count >= 9)
    //     {
    //         menuController.AccessMenu(MenuController.MenuActivate.Win);
    //         AudioManager.Instance.ChangeMusic("WinnerMusic");
    //     }
    //     else if (count == 8)
    //     {
    //         FinalPhase();
    //     }
    // }

    void Update()
    {
        DetectMouseClick();
    }

    void FixedUpdate()
    {
        if (onGround)
        {
            movePlayer(movementX, 0, movementY);

            if(playerMouseMove)
                movePlayerOnMouseClick();
        }

        VerifyFallingDeath();
    }

    /// <summary>
    /// If fall below limit, game over
    /// </summary>
    void VerifyFallingDeath()
    {
        if (transform.position.y < 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    void movePlayer(float directionX, float directionY, float directionZ)
    {
        Vector3 direction = new Vector3(directionX, directionY, directionZ);
        oppositeDirection = -direction + gameObject.transform.position;

        rb.AddForce(direction * speed);
    }

    void movePlayerOnMouseClick()
    {
        Vector3 direction = new Vector3 (targetPosition.x - rb.position.x, 0, targetPosition.z - rb.position.z);
        direction.Normalize();

        oppositeDirection = -direction + gameObject.transform.position;

        rb.AddForce(direction * speed);
    }

    void DestroyPlayer()
    {
        CameraShakerHandler.Shake(enemyShakeData);
        Destroy(gameObject);
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.GameOver();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!onGround)
                onGround = true;
        }
    }

    //Detect if player has collected itens
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.AddCount();
        }
    }
}
