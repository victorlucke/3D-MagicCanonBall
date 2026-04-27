using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI endGameText;
    public GameObject endGameScreen;
    public GameObject finalPhase;
    public GameObject finalPhaseMenu;
    private Rigidbody rb;
    [SerializeField] private int count;
    private float movementX;
    private float movementY;
    [SerializeField] private bool onGround;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        endGameScreen.SetActive(false);
        finalPhaseMenu.SetActive(false);
        finalPhase.SetActive(false);
        SetCountText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 MovementVector = movementValue.Get<Vector2>();

        movementX = MovementVector.x;
        movementY = MovementVector.y;
    }

    void FinalPhase()
    {
        Time.timeScale = 0;
        finalPhase.SetActive(true);
        finalPhaseMenu.SetActive(true);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 9)
        {
            EndGame("You Win!");
        }else if (count == 8)
        {
            FinalPhase();
        }
    }

    void EndGame(string test)
    {
        endGameScreen.SetActive(true);
        endGameText.text = test;
        Time.timeScale = 0;
    }

    void FixedUpdate()
    {
        if (onGround)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);

            rb.AddForce(movement * speed);
        }
        if (transform.position.y < 0)
        {
            EndGame("You Lose!");
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
            EndGame("You Lose!");
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
