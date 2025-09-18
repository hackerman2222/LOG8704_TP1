using UnityEngine;
using UnityEngine.InputSystem;

// Script permettant au joueur de bouger avec le stick gauche
[RequireComponent(typeof(CharacterController))]
public class JoystickMovement:MonoBehaviour
{
    public float speed =  2.0f; // Vitesse de d�placement de l'utilisateur
    public InputActionProperty moveInput;// r�f�rence � l'action du joystick (stick gauche)

    private CharacterController controller; // Gestion des collisions
    private Transform cameraTransform; // Orientation du mouvement selon la cam

    private void Start()
    {
        controller = GetComponent<CharacterController>(); // R�cup�re le character Controller
        cameraTransform = Camera.main!=null ?Camera.main.transform:transform;// R�cup�re la cam�ra principale (casque)
        moveInput.action.Enable();

    }
    private void Update()
    {
        // Lecture du Joystick (x= gauche/droite ; y=avant/arri�re)
        Vector2 input = moveInput.action.ReadValue<Vector2>();
        // cr�ation d'un vecteur de d�placement 
        Vector3 move = new Vector3(input.x, 0, input.y);
        // orientation du d�placement selon o� la cam�ra regarde
        move=cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0; // on ne bouge pas par rapport au sol 
        // D�placement du PlayerRoot Avec Collision
        controller.Move(move*speed*Time.deltaTime);
        if (input != Vector2.zero) {
            Debug.Log("Input detect" + input);
        
        }

    }
}
