using UnityEngine;
using UnityEngine.UI;

public class ModelListController : MonoBehaviour
{
    public GameObject modelPrefab;
    public Transform content;

    void Start()
    {
        // Liste des modèles 3D (à remplir manuellement quand j'aurai créé plusieurs modèles)
        string[] modelNames = { "Wen" };

        // Instancier les préfabs pour chaque modèle
        foreach (string modelName in modelNames)
        {
            GameObject modelInstance = Instantiate(modelPrefab, content);

            // Chopper l'image qui va avec le modèle
            modelInstance.GetComponentInChildren<Text>().text = modelName;

            // On verra la suite après
        }
    }
}
