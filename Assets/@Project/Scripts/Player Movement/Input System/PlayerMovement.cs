using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Controller")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private JoystickController joystickController;

    [Header("Character Controller")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator playerAnimator;
    private CharacterController characterController;

    [Header("Jump")]
    [SerializeField] private Button jumpButton;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform jumpPoint;
    private Vector3 velocity;
    private bool isJumping = false;

    [Header("Save Position")]
    private Transform characterTransform;
    private Vector3 startPosition;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();

        jumpButton.onClick.AddListener(Jump);

        characterTransform = transform;
        startPosition = characterTransform.position;

        LoadPlayerPosition();
    }

    private void Update()
    {
        Joystick();
        JumpUpdate();
    }

    private void OnDisable()
    {
        SavePlayerPosition();
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
            playerAnimator.SetTrigger("Jump");
            velocity.y += Mathf.Sqrt(jumpForce * -2.0f * gravity);
            StartCoroutine(ResetJump());
        }
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.1f);
        isJumping = false;
    }

    private void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("PlayerPosX", characterTransform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", characterTransform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", characterTransform.position.z);
        PlayerPrefs.Save();
    }

    private void LoadPlayerPosition()
    {
        float playerPosX = PlayerPrefs.GetFloat("PlayerPosX", startPosition.x);
        float playerPosY = PlayerPrefs.GetFloat("PlayerPosY", startPosition.y);
        float playerPosZ = PlayerPrefs.GetFloat("PlayerPosZ", startPosition.z);

        characterTransform.position = new Vector3(playerPosX, playerPosY, playerPosZ);
    }
}

