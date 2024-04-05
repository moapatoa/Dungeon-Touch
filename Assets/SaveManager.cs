using UnityEngine;
using System.IO;
using System.Collections.Generic;
public class SaveManager : MonoBehaviour
{
    public ModelSelectionMenu modelSelectionMenu; // Référence à la section du menu des personnages portant le script de gestion des personnages (dont la liste des personnages)
    public ModelSelectionMenu modelTokSelectionMenu; // Pareil mais goût fraise... euh, je veux dire pareil mais pour les jetons
    public GameObject characterManagerObject; // Référence à l'objet portant le script CharacterManager
    public GameObject tokenManagerObject;
    private TriDiLoader triDiLoaderChar; // Référence au script 3DLoader
    private TriDiLoader triDiLoaderTok;

    [System.Serializable] // Ça va sérialiser sévère !
    public class SaveData
    {
        public CharacterData[] characters; // Tableau pour stocker les données de chaque personnage
        public TokenData[] tokens; // goût fraise
    }

    [System.Serializable] // Il faut l'ajouter à chaque fois
    public class CharacterData
    {
        public string name;
        public Vector3 position;
        public int modelIndex;
    }
    [System.Serializable] // fraise... again
    public class TokenData
    {
        public string name;
        public Vector3 position;
        public int modelIndex;
    }

    public void SaveGame()
    {
        //ModelSelectionMenu modelSelectionMenu = Content_Model.GetComponent<ModelSelectionMenu>();
        //ModelSelectionMenu modelTokSelectionMenu = Content_ModelTok.GetComponent<ModelSelectionMenu>();
        List<GameObject> activeCharacters = modelSelectionMenu.activeCharacters;
        List<GameObject> activeTokens = modelTokSelectionMenu.activeCharacters;

        SaveData saveData = new SaveData
        {
            characters = new CharacterData[activeCharacters.Count],
            tokens = new TokenData[activeTokens.Count]
        };

        // Pour chaque personnage, enregistrer son nom et sa position
        for (int i = 0; i < activeCharacters.Count; i++)
        {
            GameObject character = activeCharacters[i];
            ModelIdentifier modelIdentifier = character.GetComponent<ModelIdentifier>();
            saveData.characters[i] = new CharacterData
            {
                name = character.name,
                position = character.transform.position,
                modelIndex = modelIdentifier.modelID
            };
        }
        for (int i = 0; i < activeTokens.Count; i++)
        {
            GameObject token = activeTokens[i];
            ModelIdentifier modelIdentifier = token.GetComponent<ModelIdentifier>();
            saveData.tokens[i] = new TokenData
            {
                name = token.name,
                position = token.transform.position,
                modelIndex = modelIdentifier.modelID
            };
        }

        // Convertir les données en JSON
        string json = JsonUtility.ToJson(saveData);

        // Enregistrer les données dans un fichier
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public void LoadGame()
    {
        List<GameObject> activeCharacters = modelSelectionMenu.activeCharacters;
        List<GameObject> activeTokens = modelTokSelectionMenu.activeCharacters;

        // Charger les données depuis le fichier
        string path = Application.persistentDataPath + "/save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            triDiLoaderChar = characterManagerObject.GetComponent<TriDiLoader>();
            triDiLoaderTok = tokenManagerObject.GetComponent<TriDiLoader>();

            // Pour chaque personnage, restaurer sa position
            for (int i = 0; i < saveData.characters.Length; i++)
            {
                CharacterData characterData = saveData.characters[i];
                GameObject characterModel = triDiLoaderChar.characterModels[characterData.modelIndex];
                GameObject character = Instantiate(characterModel, characterData.position, Quaternion.identity);
                character.name = characterData.name; // Attribuer le nom au personnage
                activeCharacters.Add(character);
            }
            for (int i = 0; i < saveData.tokens.Length; i++)
            {
                TokenData tokenData = saveData.tokens[i];
                GameObject tokenModel = triDiLoaderTok.characterModels[tokenData.modelIndex];
                GameObject token = Instantiate(tokenModel, tokenData.position, Quaternion.identity);
                token.name = tokenData.name;
                activeTokens.Add(token);
            }
        }
        modelSelectionMenu.UpdateCharacterList();
        modelTokSelectionMenu.UpdateCharacterList();
    }
}
