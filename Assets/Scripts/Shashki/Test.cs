using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private List<GameObject> puzzlePrefab;

    int currentLevel = 1;

    private bool active = false;

    bool activeRoutine = false;

    Coroutine routine;

    public GGMove ggmove;

    public GameObject game;

    [SerializeField]
    private Data data;

    private void Update()
    {
        if(game != null)
        {
            if (game.GetComponent<GameChecker>())
            {
                bool levelDone = game.GetComponent<GameChecker>().LevelDone;

                bool isPuzzleOut = game.GetComponent<GameChecker>().IsPuzzleOut;

                if (levelDone && activeRoutine == false)
                {
                    if (isPuzzleOut)
                    {
                        if (currentLevel == 1 || currentLevel == 2)
                        {
                            routine = StartCoroutine(RemoveGameAfterWin());
                        }

                        else if (currentLevel == 3)
                        {
                            routine = StartCoroutine(RemoveLastlvl());
                        }


                    }

                }

                else if (levelDone && currentLevel == 3)
                {
                    Debug.Log("You Win");

                    Destroy(game);
                }
            }
            
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

    private IEnumerator RemoveLastlvl()
    {
        activeRoutine = true;

        Destroy(game);

        ggmove.isMiniGameRunning = false;

        yield return null;
    }
}
