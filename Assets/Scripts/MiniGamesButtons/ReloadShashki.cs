using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadShashki : MonoBehaviour
{
    [SerializeField]
    private Test test;

    [SerializeField]
    private GameObject puzzleGames;

    public void ReloadAssets(GameObject currentMiniGame)
    {
        Destroy(currentMiniGame);

        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return null;

        test.game = Instantiate(puzzleGames);
    }
}
