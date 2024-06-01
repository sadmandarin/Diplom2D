using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadMiniGame : MonoBehaviour
{
    [SerializeField]
    private Test test;

    [SerializeField]
    private List<GameObject> puzzleGames;

    public void ReloadAssets(GameObject currentMiniGame, int lvl)
    {
        Destroy(currentMiniGame);

        test.game = Instantiate(puzzleGames[lvl]);
    }
}
