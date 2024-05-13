using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private List<GameObject> puzzlePrefab;

    int currentLevel = 0;

    private bool active = false;

    bool activeRoutine = false;

    Coroutine routine;

    private GameObject game;

    private void Update()
    {
        if (game != null)
        {
            bool levelDone = game.GetComponent<GameChecker>().LevelDone;

            if (levelDone && activeRoutine == false && (currentLevel == 1 || currentLevel == 2))
            {
                routine = StartCoroutine(RemoveGameAfterWin());
            }

            else if (levelDone && currentLevel == 3) 
            {
                Debug.Log("You Win");
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (!active) 
            {
                game = Instantiate(prefab);

                active = true;
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (active)
            {
                Destroy(game);

                active = false;

                game = Instantiate(puzzlePrefab[currentLevel - 1]);

                active = true;
            }
        }

        if (Input.GetKey(KeyCode.P)) 
        {
            if(!active)
            {
                game = Instantiate(puzzlePrefab[currentLevel]);

                currentLevel++;

                active = true;
            }
        }

        if (Input.GetKey(KeyCode.Escape)) 
        {
            Destroy(game);

            currentLevel = 0;

            active = false;
        }
    }

    private IEnumerator RemoveGameAfterWin()
    {
        activeRoutine = true;

        while (game.transform.position.y <= 14f)
        {
            game.transform.position += new Vector3(0, 0.02f, 0);

            yield return null;
        }

        if (game.transform.position.y >= 14f)
        {
            Destroy(game);

            game = Instantiate(puzzlePrefab[currentLevel]);

            currentLevel++;

            activeRoutine = false;
        }

        yield return null;
    }
}
