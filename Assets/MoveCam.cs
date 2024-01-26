using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float zoomSpeed = 5.0f;
    public Transform plane;
    public Renderer planeRenderer; // Références au plan
    public float minYMultiplier = 1.0f; // petit facteur arbitraire pour ajuster la limite du déplacement vertical lors du zoom

    private float minX, maxX, minZ, maxZ, minY, maxY, tanFieldAngleX, tanFieldAngleZ;

    void Start()
    {
        // Calculer les limites en fonction de la taille du plan
        Texture texture = planeRenderer.material.mainTexture;
        float halfPlaneWidth = texture.width / 2.0f;
        float halfPlaneLength = texture.height / 2.0f;

        minX = plane.position.x - halfPlaneWidth; // a priori, le plan est centré en 0, mais au cas où... #sécurité
        maxX = plane.position.x + halfPlaneWidth;
        minZ = plane.position.z - halfPlaneLength;
        maxZ = plane.position.z + halfPlaneLength;
        minY = plane.position.y + minYMultiplier; // un peu arbitraire
        tanFieldAngleZ = Mathf.Tan(Camera.main.fieldOfView / 2.0f);
        tanFieldAngleX = Mathf.Tan(Camera.VerticalToHorizontalFieldOfView(Camera.main.fieldOfView, (float)Screen.width / Screen.height) / 2.0f);
        maxY = Math.Max(halfPlaneLength / tanFieldAngleZ, halfPlaneWidth / tanFieldAngleX);// * minYMultiplier; // Dépend de la hauteur de la caméra
    }

    void Update()
    {
        // Déplacement des caméras avec les touches du clavier (flèches)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical) * moveSpeed * Time.deltaTime * transform.position.y / 2.0f;
        Vector3 newPosition = transform.position + movement;

        // Limiter la position aux bords du plan
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY); // on limite le point entre les bornes min et max
        newPosition.x = Mathf.Clamp(newPosition.x, minX + newPosition.y * tanFieldAngleZ, maxX); //
        newPosition.z = Mathf.Clamp(newPosition.z, minZ - newPosition.y * tanFieldAngleZ, maxZ);

        transform.position = newPosition;

        // Déplacement des caméras avec les touches du clavier (ZQSD) (en fait en WASD ici car Unity lance par défaut un clavier anglais... je verrai plus tard la modification)
        float moveUp = Input.GetKey("w") ? 1.0f : 0.0f;
        float moveDown = Input.GetKey("s") ? -1.0f : 0.0f;
        float moveLeft = Input.GetKey("a") ? -1.0f : 0.0f;
        float moveRight = Input.GetKey("d") ? 1.0f : 0.0f;

        Vector3 directionalMovement = new Vector3(moveLeft + moveRight, 0.0f, moveUp + moveDown) * transform.position.y * moveSpeed * Time.deltaTime * transform.position.y / 2.0f;
        newPosition = transform.position + directionalMovement;

        // Limiter la position aux bords du plan
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition.x = Mathf.Clamp(newPosition.x, minX + newPosition.y * tanFieldAngleZ, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ - newPosition.y * tanFieldAngleZ, maxZ);

        transform.position = newPosition;

        // Zoom avec la molette de la souris
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoom = new Vector3(0.0f, scrollWheel * zoomSpeed, 0.0f);
        Vector3 newZoomPosition = transform.position - zoom;

        // Limiter la position aux bords du plan lors du zoom
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition.x = Mathf.Clamp(newPosition.x, minX + newPosition.y * tanFieldAngleZ, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ - newPosition.y * tanFieldAngleZ, maxZ);

        transform.position = newZoomPosition;

        // Zoom avec les touches A et E
        if (Input.GetKey("q"))
        {
            Vector3 zoomKey = new Vector3(0.0f, -1.0f * zoomSpeed, 0.0f);
            transform.Translate(zoomKey);

            // Limiter la position aux bords du plan lors du zoom avec les touches A et E
            transform.position = new Vector3(
                Mathf.Clamp(newPosition.y, minY, maxY),
                Mathf.Clamp(newPosition.x, minX + newPosition.y * tanFieldAngleZ, maxX),
                Mathf.Clamp(newPosition.z, minZ - newPosition.y * tanFieldAngleZ, maxZ)
            );
        }
        else if (Input.GetKey("e"))
        {
            Vector3 zoomKey = new Vector3(0.0f, 1.0f * zoomSpeed, 0.0f);
            transform.Translate(zoomKey);

            // Limiter la position aux bords du plan lors du zoom avec les touches A et E
            transform.position = new Vector3(
                Mathf.Clamp(newPosition.y, minY, maxY),
                Mathf.Clamp(newPosition.x, minX + newPosition.y * tanFieldAngleZ, maxX),
                Mathf.Clamp(newPosition.z, minZ - newPosition.y * tanFieldAngleZ, maxZ)
            );
        }
    }
}