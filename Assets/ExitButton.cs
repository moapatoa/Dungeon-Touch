using UnityEngine;
using UnityEngine.UI;

public class ExitButtonScript : MonoBehaviour
{
    void Start()
    {
        // Ajouter la fonction OnClick au bouton
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    void OnClick()
    {
        // Quitter l'application
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // à retirer à la fin
#endif
    }
}
