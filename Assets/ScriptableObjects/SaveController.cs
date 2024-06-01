using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameData gameData;

    private Data currentGameData;

    private void Start()
    {
        currentGameData = SaveManager.LoadGame();

        if (currentGameData != null)
        {
            player.transform.position = currentGameData.position;

            for (int i = 0; gameData.Games.Count > i; i++)
            {
                gameData = currentGameData.gameData;
            }

            player.transform.rotation = currentGameData.rotation;
        }

        else
        {
            currentGameData = new Data();

            currentGameData.position = new Vector3(-11.35f, 1.9f, 0);

            currentGameData.rotation = new Quaternion(0,0,0,0);

            for (int i = 0; i < gameData.Games.Count; i++)
            {
                gameData.CompleteMiniGame1(i, false);
            }

            gameData.IsAcked = false;

            currentGameData.gameData = gameData;
        }
    }

    private void Update()
    {
        SaveManager.UpdatePosition(player.transform.position);

        SaveManager.UpdateRotation(player.transform.rotation);
    }
}
