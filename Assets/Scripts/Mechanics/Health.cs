using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health class")]
    [SerializeField] public bool isUnhealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isUnhealth = false;
    }
}
