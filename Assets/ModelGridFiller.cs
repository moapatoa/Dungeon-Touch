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