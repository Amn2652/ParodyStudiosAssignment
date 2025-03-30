using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 4f;
    public CameraController camController;
    public float rotationSpeed = 5f;
    Quaternion requiredRotation;

    [Header("Animation Settings")]
    public Animator animator;

    [Header("Collision and Gravity Settings")]
    public CharacterController characterController;
    public float surfaceCheckRadius = 0.3f;
    public Vector3 surfaceCheckOffset;
    public LayerMask surfacelayer;
    [SerializeField] float fallingSpeed;
    Vector3 moveDir;
    bool onSurface;

    [Header("Cudes Setting")]
    public GameManager gameManager;

    void Update()
    {
        SurfaceCheck();
        PlayerMovements();

        ApplyGravity();
    }

    void PlayerMovements()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementInput = new Vector3(horizontal, 0, vertical).normalized;

        float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        Vector3 movementRotation = camController.FlatRotation() * movementInput;
        moveDir = movementRotation;

        Vector3 velocity = movementRotation * movementSpeed;
        velocity.y = fallingSpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (moveAmount > 0)
        {
            requiredRotation = Quaternion.LookRotation(movementRotation);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, requiredRotation, rotationSpeed * Time.deltaTime);

        animator.SetFloat("Speed", moveAmount, 0.2f, Time.deltaTime);
    }

    void SurfaceCheck()
    {
        onSurface = Physics.CheckSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius, surfacelayer);
    }

    void ApplyGravity()
    {
        if (onSurface)
        {
            fallingSpeed = -1f; 
            animator.SetBool("falling", false);
        }
        else
        {
            fallingSpeed += Physics.gravity.y * Time.deltaTime;
            animator.SetBool("falling", true);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("cubes"))
        {
            gameManager.CollectCube();
            Destroy(hit.gameObject);
        }
    }
}
