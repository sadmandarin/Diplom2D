using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropPuzzles : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    public bool IsDragging {  get { return isDragging; } }
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
        gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
    }

    void OnMouseUp()
    {
        isDragging = false;
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Target") && isDragging == false)
        {
            gameObject.transform.position = other.gameObject.transform.position;

            //if (other.gameObject.GetComponent<DetectObject>().IsFilled && isDragging == false)
            //{
            //    other.enabled = false;

            //    GetComponent<Collider2D>().enabled = false;
            //}
        }
    }
}
