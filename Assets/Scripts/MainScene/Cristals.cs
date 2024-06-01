using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristals : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject button;

    [SerializeField]
    private GameData gameData;

    private void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 7f)
        {
            if(gameData.IsAcked && gameData.Games[2] == false)
            {
                if (button.activeSelf == false)
                {
                    button.SetActive(true); ;
                }
            }

                       
        }
        else
        {
            button.SetActive(false);
        }
    }
}