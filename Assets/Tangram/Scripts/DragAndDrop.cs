using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging = false;
    private Vector3 startPosition;
    private RectTransform rectTransform;

    private bool rotate = false;

    private float rotationAngle = 0;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        startPosition = rectTransform.position - Input.mousePosition;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector3 closestSquareCenter = FindClosestSquarePosition(rectTransform.position);

        // Перемещение фигуры к центру ближайшего квадрата
        rectTransform.position = closestSquareCenter;

        startPosition = Vector3.zero;


        isDragging = false;

        rotate = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            rectTransform.position = Input.mousePosition + startPosition;

        }
    }

    Vector3 FindClosestSquarePosition(Vector3 position)
    {
        GameObject[] squares = GameObject.FindGameObjectsWithTag("Square"); // Получаем все квадраты в поле

        float minDistance = Mathf.Infinity;
        Vector3 closestPosition = Vector3.zero;

        foreach (GameObject square in squares)
        {
            Vector3 squareCenter = square.transform.position;
            float distanceToCenter = Vector3.Distance(position, squareCenter); // Расстояние между фигурой и центром квадрата

            // Расстояние до каждого угла квадрата
            Vector3[] squareCorners = GetSquareCorners(square.transform.position, 50f);

            foreach (Vector3 corner in squareCorners)
            {
                float distanceToCorner = Vector3.Distance(position, corner);

                if (distanceToCorner < minDistance)
                {
                    minDistance = distanceToCorner;
                    closestPosition = corner;
                }
            }

            // Проверяем, если расстояние до центра меньше
            if (distanceToCenter < minDistance)
            {
                minDistance = distanceToCenter;
                closestPosition = squareCenter;
            }
        }

        return closestPosition;
    }

    Vector3[] GetSquareCorners(Vector3 center, float size)
    {
        Vector3[] corners = new Vector3[4];

        corners[0] = center + new Vector3(size, 0, size); // Верхний правый угол
        corners[1] = center + new Vector3(-size, 0, size); // Верхний левый угол
        corners[2] = center + new Vector3(size, 0, -size); // Нижний правый угол
        corners[3] = center + new Vector3(-size, 0, -size); // Нижний левый угол

        return corners;
    }

    Vector3 GetClosestCorner(Vector3 position, Vector3[] corners)
    {
        float minDistance = Mathf.Infinity;
        Vector3 closestCorner = Vector3.zero;

        foreach (Vector3 corner in corners)
        {
            float distance = Vector3.Distance(position, corner);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCorner = corner;
            }
        }

        return closestCorner;
    }

    private void Update()
    {
        if (isDragging)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !rotate)
            {
                rotationAngle += 45;

                if (rotationAngle >= 360)
                {
                    rotationAngle = 0;
                }

                rectTransform.rotation = Quaternion.Euler(0, 0, rotationAngle);

                rotate = true;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                rotate = false;
            }
        }
        
    }
}
