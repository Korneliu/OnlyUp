using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 2.0f;
    [SerializeField] private Rect swipeZone = new Rect(0.5f, 0f, 0.5f, 1f);

    private Vector2 rotation = Vector2.zero;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (swipeZone.Contains(touch.position / new Vector2(Screen.width, Screen.height)))
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    rotation.x += touch.deltaPosition.x * rotationSpeed;
                    rotation.y -= touch.deltaPosition.y * rotationSpeed;
                    rotation.y = Mathf.Clamp(rotation.y, -80f, 80f);
                }
            }
        }

        transform.rotation = Quaternion.Euler(rotation.y, rotation.x, 0);

        transform.position = target.position - transform.forward * 5f;
    }
}
