using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GGMove : MonoBehaviour
{
    private Vector3 targetPosition;
    bool coroutineon = false;
    Animator animator;
    [SerializeField]
    float speed = 6f;
    [SerializeField]
    private GameObject background;
    public bool isMiniGameRunning = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && coroutineon == false && !isMiniGameRunning)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider == null)
            {
                targetPosition = new Vector3(mousePos.x, transform.position.y, transform.position.z);
                Rotation(mousePos);
                StartCoroutine(MoveBG(mousePos));
            }
        }

        else if (Input.GetMouseButtonDown(0) && !isMiniGameRunning)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector3(mousePos.x, transform.position.y, transform.position.z);
            Rotation(mousePos);
        }
    }

    void Rotation(Vector3 mousePos)
    {
        if (mousePos.x < transform.position.x && transform.rotation.y == 0)
        {
            transform.Rotate(0, 180, 0);
        }
        if (mousePos.x > transform.position.x && transform.rotation.y != 0)
        {
            transform.Rotate(0, -180, 0);
        }
    }

    public IEnumerator MoveBG(Vector3 mousePos)
    {
        coroutineon = true;
        animator.SetBool("New Bool", true);
        animator.Play("walk");
        float walkDistance = Vector3.Distance(transform.position, targetPosition);
        float currentDistance = 0;
        while ((walkDistance - currentDistance) > 0.01f)
        {
            // Смещаем задний фон в противоположном направлении движения персонажа
            Vector3 direction = (targetPosition - transform.position).normalized;
            float step = speed * Time.deltaTime;
            if (background.transform.position.x > -42 && background.transform.position.x < -2 && (mousePos.x > gameObject.transform.position.x || mousePos.x < gameObject.transform.position.x))
            {
                background.transform.position -= direction * step;
            }
            else
            {
                if(background.transform.position.x >= -39)
                {
                    background.transform.position = new Vector3(-2.1f, background.transform.position.y, background.transform.position.z);
                }
                else if(background.transform.position.x <= -2)
                {
                    background.transform.position = new Vector3(-41.9f, background.transform.position.y, background.transform.position.z);
                }
                animator.SetBool("New Bool", false);
                coroutineon = false;
                break;
            }
            currentDistance += step;
            if ((walkDistance - currentDistance) <= 0.01f)
            {
                animator.SetBool("New Bool", false);
                break; // Останавливаем цикл, если достигли целевой позиции
            }
            yield return null; // Ждем один кадр перед продолжением цикла
        }
        coroutineon = false;
    }
}
