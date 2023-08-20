using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public JoystickController joystickController;
    public Transform cameraTransform;

    private CharacterController characterController;

    public float jumpForce = 5.0f;
    public float gravity = -9.81f;
    public Transform jumpPoint;
    public CharacterController controller;
    public Animator playerAnimator;
    public Button jumpButton;

    private Vector3 velocity;
    private bool isJumping = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();

        jumpButton.onClick.AddListener(Jump);
    }

    private void Update()
    {
        Joystick();
        JumpUpdate();
    }

    private void Joystick()
    {
        Vector2 joystickInput = joystickController.GetDirection();

        Vector3 moveDirection = cameraTransform.forward * joystickInput.y + cameraTransform.right * joystickInput.x;
        moveDirection.y = 0;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    private void JumpUpdate()
    {
        if (controller.isGrounded)
        {
            velocity.y = 0.0f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (controller.isGrounded && !isJumping)
        {
            isJumping = true;
            playerAnimator.SetTrigger("Jump"); // Предполагается, что аниматор имеет параметр "Jump"
            velocity.y += Mathf.Sqrt(jumpForce * -2.0f * gravity);
            StartCoroutine(ResetJump());
        }
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0f); // Подождать 1 секунду, чтобы предотвратить множественные прыжки
        isJumping = false;
    }
}

