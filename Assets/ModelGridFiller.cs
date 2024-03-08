using UnityEngine;
using UnityEngine.UI;

public class ModelGridFiller : MonoBehaviour
{
    public GameObject modelGridPrefab;
    public Sprite[] modelImages;

    void Start()
    {
        PopulateGrid();
    }

    void PopulateGrid()
    {
        foreach (Sprite modelImage in modelImages)
        {
            GameObject gridItem = Instantiate(modelGridPrefab, transform);
            Image modelImageComponent = gridItem.GetComponentInChildren<Image>();

            if (modelImageComponent != null)
            {
                modelImageComponent.sprite = modelImage;
            }
        }
    }
}

public class ModelInstantiation : MonoBehaviour
{
    public GameObject modelPrefab;

    public void SpawnModel()
    {
        // On instancie le prefab de notre modèle 3D lors du clic
        Instantiate(modelPrefab, transform.position, Quaternion.identity);
    }
}