using UnityEngine;

public class MoveCharTouch : MonoBehaviour
{
    private int touchId = -1; // ID du toucher associé à ce personnage
    private Vector3 fingerOffset; // Décalage entre la position du personnage et le point où le doigt a touché l'écran
    private Plane movementPlane; // Plan imaginaire pour le déplacement du personnage
    private RightClickKiller rightClickKiller; // Objet portant le script RightClickKiller

    void Start()
    {
        // Récupérer l'objet Scriptoferum portant le script RightClickKiller
        GameObject scriptoferum = GameObject.Find("Scriptoferum");
        movementPlane = new Plane(Vector3.up, transform.position);
        rightClickKiller = scriptoferum.GetComponent<RightClickKiller>();
    }

    void Update()
    {
        // Vérifier si ce personnage est déjà en train d'être déplacé
        if (touchId != -1)
        {
            // Récupérer le toucher associé à ce personnage
            Touch touch = Input.GetTouch(touchId);

            // Si le toucher est en cours ou s'est terminé, déplacer le personnage
            if (touch.phase == TouchPhase.Moved)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));

                // Intersection entre le rayon et le plan de déplacement
                float distance;
                if (movementPlane.Raycast(ray, out distance))
                {
                    Vector3 newPosition = ray.GetPoint(distance);
                    // Mettre à jour la position du personnage en fonction de la position de la souris et du décalage calculé
                    transform.position = new Vector3(newPosition.x + fingerOffset.x, transform.position.y, newPosition.z + fingerOffset.z);
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                // Réinitialiser l'ID du toucher lorsque le toucher est terminé ou annulé
                touchId = -1;

                // Débloquer la souris
                //Cursor.lockState = CursorLockMode.None;
                rightClickKiller.isRightClickEnabled = true;
            }
        }
        else
        {
            // Si ce personnage n'est pas en train d'être déplacé et qu'il y a des touchers actifs
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));

                // Vérifier si ce toucher est sur ce personnage
                if (touch.phase == TouchPhase.Began && Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    // Associer ce toucher à ce personnage et calculer le décalage
                    touchId = touch.fingerId;
                    fingerOffset = transform.position - hit.point;
                    // On bloque la souris pour ne pas avoir de conflit
                    //Cursor.lockState = CursorLockMode.Locked;
                    rightClickKiller.isRightClickEnabled = false;
                    break;
                }
            }
        }
    }
}
