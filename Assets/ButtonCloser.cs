using UnityEngine;
using UnityEngine.UI;

public class ButtonCloser : MonoBehaviour
{
    public GameObject[] panels;

    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    void CloseMenu()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        // Il faut également activer/désactiver le script CloseMenuOnClickOutside si on l'utilise
        // closeMenuScript.enabled = isMenuVisible;
    }

    void OnClick()
    {
        CloseMenu();
    }
}
