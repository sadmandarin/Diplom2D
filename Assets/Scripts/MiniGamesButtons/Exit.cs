using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private GameObject game;

    Button button;

    private GGMove gMove;

    private void Start()
    {
        gMove = GameObject.Find("девочка").GetComponent<GGMove>();

        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        gMove.gameObject.transform.position = new Vector3(-9.13f, -1.9f, 1);

        Destroy(game);

        gMove.isMiniGameRunning = false;
    }
}
