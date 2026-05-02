using UnityEngine;
using UnityEngine.InputSystem;

public class AIPlayerJump : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Este método será chamado pelo componente "Player Input"
    public void OnJump(InputValue value)
    {
        // Verifica se a tecla foi pressionada E se está no chão
        if (value.isPressed && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
