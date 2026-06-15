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
    public bool onGround;
    public float speed;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

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
            GameManager.Instance.GameOver();
        }
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
