using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartShashki : MonoBehaviour
{
    [SerializeField]
    private GameObject game;

    private ReloadShashki reload;

    [SerializeField] private GameObject newGame;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        reload = GameObject.Find("Controller").GetComponent<ReloadShashki>();

        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        reload.ReloadAssets(game);
    }
}
