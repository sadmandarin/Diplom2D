using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoCristals : MonoBehaviour
{
    [SerializeField]
    private GameObject crystals;

    [SerializeField]
    private GameObject go;

    [SerializeField]
    private bool isActive = false;

    [SerializeField]
    private GGMove move;

    [SerializeField]
    private AudioSource playerAudioSource;

    [SerializeField]
    private AudioClip playerAudioClip;

    public bool IsActive
    {
        get { return isActive; }
        set { IsActive = value; }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Проверяем попадание луча в коллайдер объекта
            if (hit.collider != null && hit.transform == transform && isActive == false)
            {
                playerAudioSource.PlayOneShot(playerAudioClip);

                OnObjectClicked();
            }
        }
    }

    private void OnObjectClicked()
    {
        StartCoroutine(AnimGirl());
    }

    IEnumerator AnimGirl()
    {
        Animator animator = move.gameObject.GetComponent<Animator>();

        animator.SetBool("New Bool", true);

        animator.Play("walk");

        while (Vector3.Distance(crystals.transform.position, move.gameObject.transform.position) > 0.01f)
        {
            move.gameObject.transform.position = Vector3.MoveTowards(move.gameObject.transform.position, crystals.transform.position, 4f * Time.deltaTime);

            yield return new WaitForSeconds(0.001f);
        }

        animator.SetBool("New Bool", false);

        Instantiate(go);

        move.isMiniGameRunning = true;

        isActive = true;

        StartCoroutine(SetNonActive());
    }

    IEnumerator SetNonActive()
    {
        yield return new WaitForSeconds(1.5f);

        isActive = false;
    }
}
