using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script handles everything that the player character does, such as movement, interactions and health/experience managament
/// </summary>
public class PlayerControler : MonoBehaviour
{
    // Declaration of variables

    /// <summary>
    /// Speed at which the player character moves
    /// </summary>
    [SerializeField] private float movementSpeed = 2f;
    [Range(0, 1)]
    [SerializeField] private float idleAnimationSpeed = 0.75f;
    [Range(0, 2)]
    [SerializeField] private float walkingAnimationSpeed = 1f;
    private Vector2 movementVector;

    private Animator animator;

    // Initializing variables in Awake()
    void Awake()
    {
        movementVector = Vector2.zero;
        animator = GetComponent<Animator>();
    }

    // FixedUpdate
    void FixedUpdate()
    {
        UpdateMovement();
    }

    /// <summary>
    /// Updates the player's movement based on the movementVector
    /// Also makes the player look at the direction it's moving
    /// and switches animation states based on movement
    /// </summary>
    private void UpdateMovement()
    {
        transform.position += new Vector3(movementVector.x, 0, movementVector.y) * movementSpeed * Time.deltaTime;
        if (movementVector != Vector2.zero)
        {
            transform.LookAt(new Vector3(transform.position.x + movementVector.x, 0, 
                transform.position.z + movementVector.y));
            animator.SetBool("isWalking", true);
            animator.speed = walkingAnimationSpeed;
        }
        else
        {
            animator.speed = idleAnimationSpeed;
            animator.SetBool("isWalking", false);
        }   
    }

    /// <summary>
    /// Reads the movement input from the player Input Action
    /// </summary>
    /// <param name="context"></param>
    public void MovePlayer(InputAction.CallbackContext context)
    {
        movementVector = context.ReadValue<Vector2>();
    }
}
