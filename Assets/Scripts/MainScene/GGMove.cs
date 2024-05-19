using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GGMove : MonoBehaviour
{
    private Vector3 targetPosition;

    bool coroutineon = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && coroutineon == false)
        {
            Vector3 mousePos= Camera.main.ScreenToWorldPoint(Input.mousePosition);

            targetPosition = new Vector3 (mousePos.x, mousePos.y, transform.position.z);

            StartCoroutine(MoveGG());
        }
    }

    IEnumerator MoveGG()
    {
        coroutineon = true;

        while (Vector3.Distance(targetPosition, gameObject.transform.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(targetPosition, transform.position, 0.1f);
            yield return new WaitForSeconds(0.001f);
        }

        coroutineon = false;
    }
}
