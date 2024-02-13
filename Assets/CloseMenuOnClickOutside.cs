using UnityEngine;

public class CloseMenuOnClickOutside : MonoBehaviour
{
    public GameObject menuPanel;

    void Update()
    {
        // Vérifier si le menu est ouvert et si le clic est en dehors du panneau
        if (Input.GetMouseButtonDown(0) && menuPanel.activeSelf)
        {
            RectTransform panelRectTransform = menuPanel.GetComponent<RectTransform>();
            if (panelRectTransform != null)
            {
                Vector2 mousePosition = Input.mousePosition;

                // Convertir la position du clic en coordonnées locales du panneau
                Vector2 localMousePos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, mousePosition, null, out localMousePos);

                // Vérifier si le clic est en dehors du panneau
                if (!panelRectTransform.rect.Contains(localMousePos))
                {
                    // Fermer le menu si le clic est en dehors du panneau
                    menuPanel.SetActive(false);
                }
            }
        }
    }
}
// en vrai je vais pas l'utiliser ça va être galère avec les 2 écrans