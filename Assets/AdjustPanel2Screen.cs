using UnityEngine;

public class AdjustPanel2Screen : MonoBehaviour
{
    RectTransform panelRectTransform;
    public float panelWidthPercentage = 1.0f;

    void Start()
    {
        panelRectTransform = GetComponent<RectTransform>();
        if (panelRectTransform != null)
        {
            AdjustPanelSize();
        }
    }

    void AdjustPanelSize()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        RectTransform panelRectTransform = GetComponent<RectTransform>();
        if (panelRectTransform != null)
        {
            float panelWidth = screenWidth * panelWidthPercentage;

            panelRectTransform.sizeDelta = new Vector2(panelWidth, screenHeight);

            /*// Repositionner le coin supérieur gauche du panneau au coin supérieur gauche de l'écran (parce que sinon l'étape d'avant envoie notre panneau à Perpette)
            panelRectTransform.anchorMin = new Vector2(0, 1);
            panelRectTransform.anchorMax = new Vector2(0, 1);
            panelRectTransform.pivot = new Vector2(0, 1);
            panelRectTransform.anchoredPosition = Vector2.zero;*/
        }
    }
}
