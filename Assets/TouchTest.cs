using UnityEngine;

public class TouchTest : MonoBehaviour
{
    private Material originalMaterial;
    private Material redMaterial;
    private Material greenMaterial;
    private Material blueMaterial;

    void Start()
    {
        // Conserver le matériau d'origine
        originalMaterial = GetComponent<Renderer>().material;

        // Créer un nouveau matériau rouge
        redMaterial = new Material(originalMaterial);
        redMaterial.color = Color.red;
        greenMaterial = new Material(originalMaterial);
        greenMaterial.color = Color.red;
        blueMaterial = new Material(originalMaterial);
        blueMaterial.color = Color.red;
    }

    void Update()
    {
        // Vérifier s'il y a un toucher sur l'écran tactile
        if (Input.touchCount > 0)
        {
            // Appliquer le matériau rouge
            GetComponent<Renderer>().material = redMaterial;
        }
        else
        {
            // Revenir au matériau d'origine
            GetComponent<Renderer>().material = originalMaterial;
        }
    }
}
