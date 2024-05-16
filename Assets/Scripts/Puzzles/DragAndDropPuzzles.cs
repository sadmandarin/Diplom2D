using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropPuzzles : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    private Vector3 startPosition;

    private Vector3 endPosition;

    private bool isAttached = false;

    bool isOut = false;

    bool filled;

    public bool IsDragging {  get { return isDragging; } }

    private void Start()
    {
        startPosition = transform.position;
    }

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

        isAttached = false;


    }

    void OnMouseUp()
    {
        isDragging = false;
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

        

         if (isAttached == false && isOut == true && filled == false)
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

            startPosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

            transform.position = startPosition;
        }

        else if (!filled && !isOut)
        {
            gameObject.transform.position = endPosition;
        }

        else if (!filled)
        {
            gameObject.transform.position = endPosition;
        }


        else
        {
            gameObject.transform.position = startPosition;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Target") && isDragging)
        {
            if (!other.gameObject.GetComponent<DetectObject>().IsFilled)
            {
                filled = false;

                isAttached = true;

                isOut = false;

                endPosition = other.gameObject.transform.position;

                startPosition = endPosition;

                Debug.Log("first");
            }

            else
            {
                Debug.Log("second");

                //isAttached = false;

                filled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Target") && isDragging)
        {


            isAttached = false;

            isOut = true;
        }
    }
}
