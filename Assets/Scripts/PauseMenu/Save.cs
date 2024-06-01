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
            data.gameData = gameData;

            data.position = SaveManager.GetPosition();

            data.rotation = SaveManager.GetRotation();
        }

        else
        {
            data = new Data();

            data.position = SaveManager.GetPosition();

            data.gameData = gameData;

            data.rotation = SaveManager.GetRotation();
        }

        SaveManager.SaveGame(data);

    }
}
