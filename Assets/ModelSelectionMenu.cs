using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // pour utiliser list

public class ModelSelectionMenu : MonoBehaviour
{
    public Transform spawnPoint; // Point d'apparition des personnages sur la carte (défaut : (0, 0, 0))
    public GameObject characterManagerObject; // Référence à l'objet portant le script CharacterManager

    private TriDiLoader triDiLoader; // Référence au script 3DLoader
    public Sprite[] modelImages; // Liste des images des personnages

    private string characterName = ""; // Nom du personnage à créer
    public TMP_InputField nameInputField; // Champ de saisie pour le nom du personnage
    public GameObject characterCreator; // Panneau des détails du personnage à afficher
    //public GameObject emptyCharacterCreator; // Panneau des détails du personnage vide (par défaut)
    public GameObject characterListPanel; // Panneau pour afficher la liste des personnages actifs
    public List<GameObject> activeCharacters = new List<GameObject>(); // Liste des personnages actifs
    //public GameObject characterListItemPrefab; // Préfab d'un personnage dans la liste

    void Start()
    {
        // Chopper la référence au script CharacterManager
        triDiLoader = characterManagerObject.GetComponent<TriDiLoader>();

        // Masquer le panneau des détails du personnage au démarrage
        characterCreator.SetActive(false);

        // Afficher les images dans le menu
        PopulateModelMenu();
    }

    void PopulateModelMenu()
    {
        // Pour chaque modèle 3D, créer un bouton dans le menu défilant
        for (int i = 0; i < triDiLoader.characterModels.Count; i++)
        {
            GameObject modelButton = new GameObject("ModelButton", typeof(RectTransform), typeof(Button), typeof(Image));
            modelButton.transform.SetParent(transform);

            // Assigner l'image à l'image du bouton
            Image image = modelButton.GetComponent<Image>();
            image.sprite = modelImages[i]; // Utilisez l'image correspondante à partir de la liste modelImages

            // Ajouter un listener pour le clic sur le bouton
            Button button = modelButton.GetComponent<Button>();
            int index = i;
            button.onClick.AddListener(() => OnModelButtonClicked(index));
        }
    }

    void OnModelButtonClicked(int modelIndex)
    {
        // Afficher le panneau des détails du personnage
        characterCreator.SetActive(true);

        // Réinitialiser le nom du personnage
        characterName = "";
        nameInputField.text = "";

        // Ajouter un listener pour le champ de saisie du nom du personnage
        nameInputField.onValueChanged.AddListener(OnNameInputFieldValueChanged);

        Button validateButton = characterCreator.GetComponentInChildren<Button>();
        // Retirer les listeners précédemment ajoutés sinon ça m'ajoute un personnage de plus à chaque itération
        validateButton.onClick.RemoveAllListeners();
        // Ajouter un listener pour le bouton valider
        validateButton.onClick.AddListener(() => OnValidateButtonClicked(modelIndex));
    }

    void OnNameInputFieldValueChanged(string value)
    {
        // Mettre à jour le nom du personnage
        characterName = value;
    }

    void OnValidateButtonClicked(int modelIndex)
    {
        // Instancier le modèle 3D du personnage à l'emplacement de spawn avec le nom donné
        GameObject characterModel = triDiLoader.characterModels[modelIndex];
        //spawnPoint.position = new Vector3(spawnPoint.position.x, characterModel.transform.position.y, spawnPoint.position.z);
        GameObject character = Instantiate(characterModel, spawnPoint.position, Quaternion.identity);
        character.name = characterName; // Attribuer le nom au personnage
        character.AddComponent<ModelIdentifier>();
        ModelIdentifier modelIdentifier = character.GetComponent<ModelIdentifier>();
        modelIdentifier.modelID = modelIndex;
        //character.tag = "character";

        // Masquer le panneau des détails du personnage après validation
        characterCreator.SetActive(false);

        // Ajouter le personnage à la liste des personnages actifs
        activeCharacters.Add(character);

        // Afficher sur la liste des personnages actifs
        UpdateCharacterList();
    }

    public void UpdateCharacterList()
    {
        // Effacer les éléments précédents de la liste des personnages
        foreach (Transform child in characterListPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Ajouter un élément pour chaque personnage actif dans la liste
        for (int i = 0; i < activeCharacters.Count; i++)
        {
            /*// Instancier l'élément de la liste à partir du prefab
            GameObject listItem = Instantiate(characterListItemPrefab, characterListPanel.transform);
            listItem.transform.SetParent(characterListPanel.transform);

            // Obtenir les composants nécessaires de l'élément de la liste
            TMP_Text characterNameText = listItem.transform.Find("CharButton/CharName (TMP)").GetComponent<TMP_Text>();
            Button deleteButton = listItem.transform.Find("CharButton/CharDeleteButton").GetComponent<Button>();

            // Assigner le nom du personnage au texte de l'élément de la liste
            characterNameText.text = character.name;

            // Ajouter un listener pour le bouton de suppression
            deleteButton.onClick.AddListener(() => OnDeleteButtonClicked(listItem, character));*/

            GameObject charButton = new GameObject("CharacterButton", typeof(RectTransform), typeof(Button));
            charButton.transform.SetParent(characterListPanel.transform);

            // Créer le texte pour le nom du personnage
            GameObject nameTextObject = new GameObject("NameText", typeof(RectTransform), typeof(Text));
            nameTextObject.transform.SetParent(charButton.transform);
            Text characterNameText = nameTextObject.GetComponent<Text>();
            characterNameText.text = activeCharacters[i].name;
            characterNameText.font = Resources.Load<Font>("Fonts & Materials/AONCC_");
            characterNameText.fontSize = 48;
            characterNameText.color = Color.black; // Couleur de police noire

            // Positionner le texte du nom du personnage
            RectTransform nameTextRect = nameTextObject.GetComponent<RectTransform>();
            nameTextRect.anchorMin = new Vector2(0, 0.5f);
            nameTextRect.anchorMax = new Vector2(0.5f, 0.5f);
            nameTextRect.pivot = new Vector2(0, 0.5f);
            nameTextRect.sizeDelta = new Vector2(204, 96); // Taille du texte
            nameTextRect.anchoredPosition = new Vector2(-60, 0);

            // Créer le bouton de suppression
            GameObject deleteButton = new GameObject("DeleteButton", typeof(RectTransform), typeof(Button));
            deleteButton.transform.SetParent(charButton.transform);
            Button deleteButtonComponent = deleteButton.GetComponent<Button>();
            int index = i;
            deleteButtonComponent.onClick.AddListener(() => OnDeleteButtonClicked(activeCharacters[index]));
            Text deleteButtonText = deleteButton.AddComponent<Text>();
            deleteButtonText.text = "X";
            deleteButtonText.font = Resources.Load<Font>("Fonts & Materials/AONCC_");
            deleteButtonText.fontSize = 36;
            deleteButtonText.color = Color.black;

            // Positionner le bouton de suppression
            RectTransform deleteButtonRect = deleteButton.GetComponent<RectTransform>();
            deleteButtonRect.anchorMin = new Vector2(1, 0.5f);
            deleteButtonRect.anchorMax = new Vector2(1, 0.5f);
            deleteButtonRect.pivot = new Vector2(0, 0.5f);
            deleteButtonRect.sizeDelta = new Vector2(60, 60); // Taille du bouton
            deleteButtonRect.anchoredPosition = new Vector2(0, 0); // Décalage pour aligner le bouton à droite du texte du nom du personnage
        }
    }

    void OnDeleteButtonClicked(GameObject character)//GameObject listItem, 
    {
        // Supprimer le personnage de la liste des personnages actifs et de la scène
        activeCharacters.Remove(character);
        Destroy(character);

        // Supprimer l'élément de la liste
        //Destroy(listItem);

        // Mettre à jour la liste des personnages dans le ScrollView
        UpdateCharacterList();
    }
}