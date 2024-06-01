using UnityEngine;
using UnityEngine.UI;

public class ReloadGhost : MonoBehaviour
{
    [SerializeField]
    private GameObject game;

    private ReloadGhostGame reload;

    [SerializeField] private GameObject newGame;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        reload = GameObject.Find("Controller").GetComponent<ReloadGhostGame>();

        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        reload.ReloadAssets(game, game.GetComponent<GameController>().CurrentLvl);
    }
}
