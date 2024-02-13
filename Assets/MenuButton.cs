using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject menuPanel;
    // public CloseMenuOnClickOutside closeMenuScript;
    private bool isMenuVisible = false;

    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
        // closeMenuScript = GameObject.Find("ClickOutsideManager").GetComponent<CloseMenuOnClickOutside>();
    }

    void ToggleMenu()
    {
        isMenuVisible = !isMenuVisible;
        menuPanel.SetActive(isMenuVisible);
        // Il faut également activer/désactiver le script CloseMenuOnClickOutside si on l'utilise
        // closeMenuScript.enabled = isMenuVisible;
    }

    void OnClick()
    {
        ToggleMenu();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }
}
