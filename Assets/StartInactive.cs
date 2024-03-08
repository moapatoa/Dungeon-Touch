using UnityEngine;

public class DisableElement : MonoBehaviour
{
    void Start()
    {
        // Désactiver l'objet au lancement du jeu
        gameObject.SetActive(false);
    }
}
