using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private TextMeshProUGUI counText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
            Destroy(gameObject);

        counText = GameObject.Find("CountText").GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        GameManager.OnCountChanged += UpdateCountUI;
    }

    void OnDisable()
    {
        GameManager.OnCountChanged -= UpdateCountUI;
    }

    void UpdateCountUI(int newCountValue)
    {
        counText.text = "Count: " + newCountValue;
    }
}
