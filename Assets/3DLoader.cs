using UnityEngine;
using System.Collections.Generic;

public class TriDiLoader : MonoBehaviour
{
    public List<GameObject> characterModels = new List<GameObject>(); // Liste des modèles 3D des personnages

    void Start()
    {
        // Charger les modèles 3D à partir des ressources
        GameObject[] loadedModels = Resources.LoadAll<GameObject>("CharacterModels");

        // Ajouter les modèles chargés à la liste
        characterModels.AddRange(loadedModels);
    }
}