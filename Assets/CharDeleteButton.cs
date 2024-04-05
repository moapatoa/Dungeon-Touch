using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    public void OnDeleteButtonClick()
    {
        // Supprimer le personnage associé à cet élément de la liste
        GameObject character = transform.parent.gameObject;
        Destroy(character);
    }
}
