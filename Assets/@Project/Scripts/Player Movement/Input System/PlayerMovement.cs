using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public JoystickController joystickController;
    public Transform cameraTransform; // Ссылка на камеру

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Получаем ввод от джойстика
        Vector2 joystickInput = joystickController.GetDirection();

        // Преобразовываем входные направления в мировое пространство
        Vector3 moveDirection = cameraTransform.forward * joystickInput.y + cameraTransform.right * joystickInput.x;
        moveDirection.y = 0; // Убираем вертикальное движение

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }
}
