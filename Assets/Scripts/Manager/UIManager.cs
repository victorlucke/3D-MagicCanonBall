using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    //public enum MenuActivate { Over, Win, Main, Pause, Settings, None };
    //public Slider magicBar;

    /// <summary>
    /// used to interact with value in magicBar
    /// </summary>
    // public float barValue
    // {
    //     get
    //     {
    //         if (!magicBar)
    //         {
    //             Debug.Log("Bar reference Missing");
    //             return 0f;
    //         }

    //         return magicBar.value;
    //     }

    //     set { if (magicBar) magicBar.value = value; }
    // }

    private TextMeshProUGUI counText;

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
        //magicBar = GameObject.Find("MagicBar").GetComponent<Slider>();
    }

    /// <summary>
    /// Access any menu, inside a parent deactivating the remaining ones
    /// </summary>
    /// <param name="myMenusparent">Deactivate all menus inside it</param>
    /// <param name="myMenu">Activate this menu</param>
    public void AccessMenu(GameObject myMenusparent, GameObject myMenu)
    {
        DeactivateAllMenus(myMenusparent);
        ShowMenu(myMenu, true);
        // _currentMenu = newMenu;

        // switch (newMenu)
        // {
        //     case MenuActivate.Main:
        //         DeactivateAllMenus(myMenusparent);
        //         ShowMenu(mainMenuObject, true);
        //         break;
        //     case MenuActivate.Over:
        //         DeactivateAllMenus(menusUI);
        //         ShowMenu(overMenuObject, true);
        //         break;
        //     case MenuActivate.Win:
        //         DeactivateAllMenus(menusUI);
        //         ShowMenu(winnerMenuObject, true);
        //         break;
        //     case MenuActivate.Pause:
        //         DeactivateAllMenus(menusUI);
        //         ShowMenu(pauseMenuObject, true);
        //         break;
        //     case MenuActivate.None:
        //         DeactivateAllMenus(menusUI);
        //         break;
        // }
    }

    /// <summary>
    /// deactivating the remaining objects inside a parent
    /// </summary>
    /// <param name="myMenusParent"></param>
    public void DeactivateAllMenus(GameObject myMenusParent)
    {
        int numberOfMenus;

        numberOfMenus = myMenusParent.transform.childCount;

        for (int i = 0; i < numberOfMenus; i++)
        {
            myMenusParent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// activate a menu game object
    /// </summary>
    /// <param name="myMenu"></param>
    /// <param name="isToThisShow"></param>
    private void ShowMenu(GameObject myMenu, bool isToThisShow)
    {
        myMenu.SetActive(isToThisShow);
    }

    void UpdateCountUI(int newCountValue)
    {
        counText.text = "Count: " + newCountValue;
    }
    public void ChangeImageColor(Image myImage, Color myColor)
    {
        myImage.color = myColor;
    }
}
