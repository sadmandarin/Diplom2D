using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    private string savePath;
    Button button;

    private Image image;

    [SerializeField]
    private Sprite defoltSprite;

    [SerializeField]
    private Sprite nonInteractableButton;

    private void Start()
    {
        button = GetComponent<Button>();

        image = GetComponent<Image>();

        savePath = Path.Combine(Application.persistentDataPath, "saveData.json");

        if (File.Exists(savePath))
        {
            button.interactable = true;

            image.sprite = defoltSprite;
        }
        else
        {
            button.interactable = false;

            image.sprite = nonInteractableButton;
        }

        button.onClick.AddListener(OnClick);
    }
        

    void OnClick()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(1);
    }
}
