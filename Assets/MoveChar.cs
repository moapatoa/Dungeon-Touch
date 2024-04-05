using UnityEngine;

public class MoveChar : MonoBehaviour
{
    private bool isDragging = false; // Indique si le personnage est en train d'être déplacé
    private Vector3 offset; // Décalage entre la position du personnage et le point où l'utilisateur a cliqué

    private Plane movementPlane; // Plan imaginaire pour le déplacement du personnage

    void Start()
    {
        // Créer un plan dans le monde à la hauteur du personnage pour le déplacement
        movementPlane = new Plane(Vector3.up, transform.position);
    }

    void Update()
    {
        // Vérifier si le joueur appuie sur le bouton de la souris
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

            // Lancer un rayon depuis la position de la souris pour détecter les collisions avec les personnages
            if (Physics.Raycast(ray, out hit))
            {
                // Vérifier si le GameObject touché est la bonne entité
                if (hit.collider.gameObject == gameObject && (hit.collider.CompareTag("entity") || hit.collider.CompareTag("character")))
                {
                    // Le joueur a cliqué sur ce personnage, commencer le déplacement
                    isDragging = true;
                    // Calculer le décalage entre la position du personnage et le point de la souris
                    offset = transform.position - hit.point;
                }
            }
        }
        // Si le joueur relâche le bouton de la souris, arrêter le déplacement
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Si le joueur est en train de faire glisser le personnage, mettre à jour sa position
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

            // Intersection entre le rayon et le plan de déplacement
            float distance;
            if (movementPlane.Raycast(ray, out distance))
            {
                Vector3 newPosition = ray.GetPoint(distance);
                // Mettre à jour la position du personnage en fonction de la position de la souris et du décalage calculé
                transform.position = new Vector3(newPosition.x + offset.x, transform.position.y, newPosition.z + offset.z);
            }
        }
    }
}

/*
        // Vérifier si le bouton gauche de la souris est enfoncé
        if (Input.GetMouseButtonDown(0))
        {
            // Lancer un rayon depuis la position de la souris dans le monde
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Vérifier si le rayon heurte un GameObject avec un Collider
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("touché");
                Debug.Log(hit.collider.tag);
                // Vérifier si le GameObject heurté est un personnage
                if (hit.collider.CompareTag("character"))// || hit.collider.CompareTag("entity"))
                {
                    Debug.Log("Char");
                    // Déplacer le personnage vers la position heurtée sur le plan (sans modifier la hauteur)
                    Vector3 destination = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    transform.position = destination;
                }
            }
        }
*/