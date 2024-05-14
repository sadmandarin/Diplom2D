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

            bool isPuzzleOut = game.GetComponent<GameChecker>().IsPuzzleOut;

            if (levelDone && activeRoutine == false && (currentLevel == 1 || currentLevel == 2))
            {
                if (isPuzzleOut)
                {
                    routine = StartCoroutine(RemoveGameAfterWin());
                }
                
            }

            else if (levelDone && currentLevel == 3) 
            {
                Debug.Log("You Win");

                Destroy(game);
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

        Destroy(game);

        game = Instantiate(puzzlePrefab[currentLevel]);

        currentLevel++;

        activeRoutine = false;
        

        yield return null;
    }
}
