using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject menuButton;
    public GameObject backButton;
    public GameObject[] menuSections;
    // rajouter toutes les parties du menu au fur et à mesure

    // public CloseMenuOnClickOutside closeMenuScript;
    private bool isVisible = false;

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
        isVisible = !isVisible;
        //menuPanel.SetActive(isVisible);
        menuButton.SetActive(isVisible);
        //backButton.SetActive(isVisible);
        // Il faut également activer/désactiver le script CloseMenuOnClickOutside si on l'utilise
        // closeMenuScript.enabled = isVisible;
    }

    void OnClick()
    {
        ToggleMenu();
    }
    void Update()
    {
        if (menuButton.activeInHierarchy)
        {
            menuPanel.SetActive(false);
            backButton.SetActive(false);
            foreach (GameObject i in menuSections)
            {
                i.SetActive(false);
            }

        }
    }
}

// penser à regarder le script "button", à la rigueur le copier et modifier légèrement sur certains boutons (peut être à l'origine du bug du double-clic)