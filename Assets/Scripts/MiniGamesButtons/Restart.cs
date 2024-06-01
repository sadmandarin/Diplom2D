using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    [SerializeField]
    private GameObject game;

    private ReloadMiniGame reload;

    [SerializeField] private GameObject newGame;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        reload = GameObject.Find("Controller").GetComponent<ReloadMiniGame>();

        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        reload.ReloadAssets(game, game.GetComponent<GameChecker>().Level);
    }
}
