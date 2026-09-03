using UnityEngine;

public class Gates : MonoBehaviour
{
    public Animator animator;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            CallPressurePlateAnimation();
    }

    void CallPressurePlateAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("PressPlate");
        }
    }

    public void CallOpenGateAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("OpenGate");
        }
    }
}
