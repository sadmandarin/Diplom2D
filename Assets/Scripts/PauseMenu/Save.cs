using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    Button button;

    [SerializeField]
    private GameData gameData;

    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Data data = SaveManager.LoadGame();
        
        if (data != null)
        {
            data.isAcked = gameData.IsAcked;

            for (int i = 0; i < gameData.Games.Count; i++)
            {
                data.games[i] = gameData.Games[i];
            }

            data.backGroundPos = SaveManager.GetBack();

            data.position = SaveManager.GetPosition();

            data.rotation = SaveManager.GetRotation();
        }

        else
        {
            data = new Data();

            data.position = SaveManager.GetPosition();

            data.backGroundPos = SaveManager.GetBack();

            //data.gameData = new GameData();

            data.isAcked = gameData.IsAcked;

            data.games = new List<bool>();

            for (int i = 0; i < gameData.Games.Count; i++)
            {
                data.games.Add(gameData.Games[i]);
            };

            data.rotation = SaveManager.GetRotation();
        }

        SaveManager.SaveGame(data);

    }
}
