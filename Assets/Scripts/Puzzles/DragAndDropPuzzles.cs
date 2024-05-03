using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropPuzzles : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;


    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Триггер сработал с объектом: " + other.gameObject.name);


        if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            gameObject.transform.position = other.gameObject.transform.position;
        }
    }
}
