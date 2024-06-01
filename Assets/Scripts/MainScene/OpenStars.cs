using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStars : MonoBehaviour
{
    [SerializeField]
    List<GameObject> stars;

    [SerializeField]
    private GameObject _openDoor;

    [SerializeField]
    private GameData gameData;

    private SpriteRenderer spriteRenderer;

    private bool isRunning = false;

    float alpha = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {

        int countStar = 0;
        for (int i = 0; i < stars.Count; i++)
        {

            if (gameData.Games[i] == true)
            {
                stars[i].SetActive(true);

                countStar++;
            }
            else
            {
                stars[i].SetActive(false);
            }
        }

        if (countStar == 3 && !isRunning)
        {
            StartCoroutine(FadeSprite());

            isRunning = true;
        }
    }

    private IEnumerator FadeSprite()
    {
        while (alpha < 2)
        {
            _openDoor.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);

            alpha += 0.05f;

            yield return new WaitForSeconds(0.07f);
        }
        

        

    }
}
