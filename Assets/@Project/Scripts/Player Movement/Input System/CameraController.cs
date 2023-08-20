using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public JoystickController joystickCameraController;
    public Transform target; // Персонаж, вокруг которого будет крутиться камера
    public float cameraRotationSpeed = 2f;
    public Vector3 cameraOffset = new Vector3(0, 2, -5);

    private Vector3 initialOffset;
    private Vector3 rotationAngles = Vector3.zero;

    private void Start()
    {
        initialOffset = transform.position - target.position;
    }

    private void Update()
    {
        float joystickInputX = joystickCameraController.GetDirection().x;
        float joystickInputY = joystickCameraController.GetDirection().y;

        // Вычисляем углы поворота
        rotationAngles.y += joystickInputX * cameraRotationSpeed;
        rotationAngles.x -= joystickInputY * cameraRotationSpeed;
        rotationAngles.x = Mathf.Clamp(rotationAngles.x, -90, 90); // Ограничиваем вертикальный угол

        // Преобразуем углы в кватернион
        Quaternion rotation = Quaternion.Euler(rotationAngles);

        // Вычисляем позицию камеры с учетом вращения
        Vector3 targetPosition = target.position + rotation * initialOffset;

        transform.position = targetPosition;
        transform.LookAt(target);
    }
}
