using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image hoverImage;
    private bool isHovered = false;

    void Start()
    {
        // Récupérer le composant Image du bouton
        hoverImage = GetComponent<Image>(); // C'est l'image qu'on a sélectionné dans Image source de la composante image de notre bouton

        // Cacher l'image au départ
        SetHoverImageActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // La souris survole le bouton
        SetHoverImageActive(true);
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // La souris quitte le bouton
        SetHoverImageActive(false);
        isHovered = false;
    }

    void Update()
    {
        if (isHovered)
        {
            // On peut faire d'autres trucs ici
        }
    }

    void SetHoverImageActive(bool active)
    {
        // Activer ou désactiver l'image de cercle
        if (hoverImage != null)
        {
            hoverImage.enabled = active;
        }
    }
}
