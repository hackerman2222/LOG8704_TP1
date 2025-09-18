using UnityEngine;
using UnityEngine.InputSystem;

// Script permettant au joueur de bouger avec le stick gauche
[RequireComponent(typeof(CharacterController))]
public class JoystickMovement:MonoBehaviour
{
    public float speed =  2.0f; // Vitesse de déplacement de l'utilisateur
    public InputActionProperty moveInput;// référence à l'action du joystick (stick gauche)

    private CharacterController controller; // Gestion des collisions
    private Transform cameraTransform; // Orientation du mouvement selon la cam

    private void Start()
    {
        controller = GetComponent<CharacterController>(); // Récupère le character Controller
        cameraTransform = Camera.main!=null ?Camera.main.transform:transform;// Récupère la caméra principale (casque)
        moveInput.action.Enable();

    }
    private void Update()
    {
        // Lecture du Joystick (x= gauche/droite ; y=avant/arrière)
        Vector2 input = moveInput.action.ReadValue<Vector2>();
        // création d'un vecteur de déplacement 
        Vector3 move = new Vector3(input.x, 0, input.y);
        // orientation du déplacement selon où la caméra regarde
        move=cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0; // on ne bouge pas par rapport au sol 
        // Déplacement du PlayerRoot Avec Collision
        controller.Move(move*speed*Time.deltaTime);
        if (input != Vector2.zero) {
            Debug.Log("Input detect" + input);
        
        }

    }
}
