using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SaveButtonScript : MonoBehaviour
{
    public SaveManager saveManager; // Référence au script SaveManager pour effectuer la sauvegarde
    public GameObject saveConfirmedImage; // Référence à l'image "SaveConfirmed" à afficher

    private Button saveButton; // Référence au composant Button du bouton

    void Start()
    {
        // Récupérer le composant Button du bouton
        saveButton = GetComponent<Button>();

        // Ajouter un listener pour le clic sur le bouton
        saveButton.onClick.AddListener(SaveGame);

        // Charger la partie précédente
        saveManager.LoadGame();
    }

    void SaveGame()
    {
        // Appeler la méthode de sauvegarde dans le script SaveManager
        saveManager.SaveGame();
        Debug.Log("Partie sauvegardée !");

        // Afficher l'image "SaveConfirmed" pendant deux secondes
        StartCoroutine(ShowSaveConfirmed());
    }

    IEnumerator ShowSaveConfirmed()
    {
        // Activer l'image "SaveConfirmed"
        saveConfirmedImage.SetActive(true);

        // Attendre deux secondes
        yield return new WaitForSeconds(2f);

        // Désactiver l'image "SaveConfirmed" après deux secondes
        saveConfirmedImage.SetActive(false);
    }
}
