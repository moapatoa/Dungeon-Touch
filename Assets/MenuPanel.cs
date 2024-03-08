using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public Button backButton;
    void Start()
    {
        // on ajuste la taille dès le démarrage
        AdjustPanelSize();

        // le listener permet d'"écouter" l'état du bouton (c'est un gestionnaire d'événement)
        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }
    }
    void OnBackButtonClicked()
    {
        // Fermer le panneau du menu
        gameObject.SetActive(false); // NB : pour une raison ou pour un autre, quand je ferme avec ce bouton, je dois faire 2 clics pour rouvrir. A méditer...
    }

    void AdjustPanelSize()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        RectTransform panelRectTransform = GetComponent<RectTransform>();
        if (panelRectTransform != null)
        {
            float panelWidthPercentage = 0.33f; // Défini sur un tiers de l'écran, seems good comme ça
            float panelWidth = screenWidth * panelWidthPercentage;

            panelRectTransform.sizeDelta = new Vector2(panelWidth, screenHeight);
        }
    }
}