using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 6f;
    public float rotationSpeed = 720f;
    public float gravity = -20f;

    [Header("Pulo")]
    public float jumpHeight = 2f;

    CharacterController controller;
    Vector2 moveInput;
    Vector3 velocity;
    bool jumpInput;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);
        moveDir = Vector3.ClampMagnitude(moveDir, 1f);

        Vector3 horizontalMove = moveDir * moveSpeed;

        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        if (controller.isGrounded && jumpInput)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpInput = false;
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove =
            (horizontalMove + Vector3.up * velocity.y) * Time.deltaTime;

        controller.Move(finalMove);

        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(moveDir, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            jumpInput = true;
    }
}