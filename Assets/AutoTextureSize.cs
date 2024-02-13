using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTextureSize : MonoBehaviour
{
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && renderer.material != null)
        {
            Texture texture = renderer.material.mainTexture;
            if (texture != null)
            {
                transform.localScale = new Vector3(
                    texture.width / 100.0f, // on peut ajuster au besoin
                    1.0f,
                    texture.height / 100.0f
                );
            }
        }
    }
}