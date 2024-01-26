using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCam : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float zoomSpeed = 5.0f;
    public Transform plane;
    public Renderer planeRenderer; // Références au plan
    public float minYMultiplier = 1.0f; // petit facteur arbitraire pour ajuster la limite du déplacement vertical lors du zoom

    private float minX, maxX, minZ, maxZ, minY, maxY, tanFieldAngleX, tanFieldAngleZ;

    void Start()
    {
        // il s'agit de calculer les limites en fonction de la taille du plan
        Bounds planeBounds = planeRenderer.bounds;
        Texture texture = planeRenderer.material.mainTexture;
        Vector3 planeScale = new Vector3(texture.width / 100.0f, 1.0f, texture.height / 100.0f);
        /*float halfPlaneWidth = planeRenderer.bounds.size.x / 2.0f;
        float halfPlaneLength = planeRenderer.bounds.size.z / 2.0f;


        /*minX = plane.position.x - halfPlaneWidth; // a priori, le plan est centré en 0, mais au cas où... #sécurité
        maxX = plane.position.x + halfPlaneWidth;
        minZ = plane.position.z - halfPlaneLength;
        maxZ = plane.position.z + halfPlaneLength;
        minY = plane.position.y + minYMultiplier; // un peu arbitraire
        tanFieldAngleZ = Mathf.Tan(Camera.main.fieldOfView / 2.0f);
        tanFieldAngleX = Mathf.Tan(Camera.VerticalToHorizontalFieldOfView(Camera.main.fieldOfView, (float)Screen.width / Screen.height) / 2.0f);
        maxY = Math.Max(halfPlaneLength / tanFieldAngleZ, halfPlaneWidth / tanFieldAngleX);// * minYMultiplier; // Dépend de la hauteur de la caméra
*/

        minX = planeBounds.min.x * planeScale.x; // on multiplie par localScale car on l'a modifié dans AutoTextureSize
        maxX = planeBounds.max.x * planeScale.x;
        minZ = planeBounds.min.z * planeScale.z;
        maxZ = planeBounds.max.z * planeScale.z;
        minY = planeBounds.min.y * planeScale.y + minYMultiplier;

        tanFieldAngleZ = Mathf.Tan(Camera.main.fieldOfView * Mathf.Deg2Rad / 2.0f);
        tanFieldAngleX = Mathf.Tan(Camera.VerticalToHorizontalFieldOfView(Camera.main.fieldOfView, Camera.main.aspect) * Mathf.Deg2Rad / 2.0f);
        maxY = Mathf.Min(planeBounds.size.z * planeScale.z / (2 * tanFieldAngleZ), planeBounds.size.x * planeScale.x / (2 * tanFieldAngleX));
    }
    void Update()
    {
        // Déplacement des caméras avec les touches du clavier (flèches)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical) * moveSpeed * Time.deltaTime * transform.position.y / 2.0f;
        Vector3 newPosition = transform.position + movement;

        // Limiter la position aux bords du plan
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition.x = Mathf.Clamp(newPosition.x, minX + newPosition.y * tanFieldAngleX, maxX - newPosition.y * tanFieldAngleX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ + newPosition.y * tanFieldAngleZ, maxZ - newPosition.y * tanFieldAngleZ);

        transform.position = newPosition;

        // Zoom avec la molette de la souris
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoom = new Vector3(0.0f, scrollWheel * zoomSpeed, 0.0f);
        Vector3 newZoomPosition = transform.position - zoom;

        // Limiter la position aux bords du plan lors du zoom
        newZoomPosition.y = Mathf.Clamp(newZoomPosition.y, minY, maxY);
        newZoomPosition.x = Mathf.Clamp(newZoomPosition.x, minX + newZoomPosition.y * tanFieldAngleX, maxX - newZoomPosition.y * tanFieldAngleX);
        newZoomPosition.z = Mathf.Clamp(newZoomPosition.z, minZ + newZoomPosition.y * tanFieldAngleZ, maxZ - newZoomPosition.y * tanFieldAngleZ);

        transform.position = newZoomPosition;
    }
}