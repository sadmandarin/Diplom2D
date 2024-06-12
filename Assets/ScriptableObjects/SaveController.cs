using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject background;

    [SerializeField]
    private GameData gameData;

    private Data currentGameData;

    private void Start()
    {
        currentGameData = SaveManager.LoadGame();

        if (currentGameData != null)
        {
            player.transform.position = currentGameData.position;

            background.transform.position = currentGameData.backGroundPos;
            
            for (int i = 0;i < gameData.Games.Count; i++)
            {
                gameData.Games[i] = currentGameData.games[i];
            }

            gameData.IsAcked = currentGameData.isAcked;
            

            player.transform.rotation = currentGameData.rotation;
        }

        else
        {
            currentGameData = new Data();

            currentGameData.position = new Vector3(-11.35f, 1.9f, 0);

            currentGameData.rotation = new Quaternion(0,0,0,0);

            currentGameData.backGroundPos = new Vector3(-9.04f, 1.168381f, 0.03071298f);

            for (int i = 0; i < gameData.Games.Count; i++)
            {
                gameData.CompleteMiniGame1(i, false);
            }

            gameData.IsAcked = false;

            currentGameData.isAcked = gameData.IsAcked;

            currentGameData.games = new List<bool>();

            for (int i = 0;i < gameData.Games.Count;i++)
            {
                currentGameData.games.Add(gameData.Games[i]);
            }
        }
    }

    private void Update()
    {
        SaveManager.UpdatePosition(player.transform.position);

        SaveManager.UpdateRotation(player.transform.rotation);

        SaveManager.UpdateBack(background.transform.position);
    }
}
