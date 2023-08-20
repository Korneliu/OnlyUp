using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public JoystickController joystickController;
    public Transform cameraTransform; // ������ �� ������

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // �������� ���� �� ���������
        Vector2 joystickInput = joystickController.GetDirection();

        // ��������������� ������� ����������� � ������� ������������
        Vector3 moveDirection = cameraTransform.forward * joystickInput.y + cameraTransform.right * joystickInput.x;
        moveDirection.y = 0; // ������� ������������ ��������

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }
}
