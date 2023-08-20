using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform joystickBackground;
    private Vector2 defaultPosition;
    private Vector2 direction = Vector2.zero;

    public float joystickRadius;

    private void Start()
    {
        defaultPosition = joystickBackground.position;
        joystickRadius = joystickBackground.rect.width * 1.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joystickPosition = eventData.position - defaultPosition;
        direction = joystickPosition.normalized;

        float distance = Vector2.Distance(defaultPosition, eventData.position);
        if (distance > joystickRadius)
        {
            Vector2 cappedPosition = defaultPosition + direction * joystickRadius;
            transform.position = cappedPosition;
        }
        else
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        direction = Vector2.zero;
        transform.position = defaultPosition;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }
}
