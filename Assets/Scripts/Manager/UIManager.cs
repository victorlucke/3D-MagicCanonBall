using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private TextMeshProUGUI counText;
    public Slider magicBar;
    /// <summary>
    /// used to interact with value in magicBar
    /// </summary>
    public float barValue
    {
        get
        {
            if (!magicBar)
            {
                Debug.Log("Bar reference Missing"); 
                return 0f;
            } 

            return magicBar.value;
        }
        set
        {
            if (magicBar) magicBar.value = value;
        }
    }

    void OnEnable()
    {
        GameManager.OnCountChanged += UpdateCountUI;
    }

    void OnDisable()
    {
        GameManager.OnCountChanged -= UpdateCountUI;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
            Destroy(gameObject);

        //counText = GameObject.Find("CountText").GetComponent<TextMeshProUGUI>();
        magicBar = GameObject.Find("MagicBar").GetComponent<Slider>();
    }

    void UpdateCountUI(int newCountValue)
    {
        counText.text = "Count: " + newCountValue;
    }
}
