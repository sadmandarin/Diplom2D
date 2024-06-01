using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [SerializeField]
    private List<bool> games;

    [SerializeField]
    private bool isAcked;

    public List<bool> Games
    {
        get { return games; }
    }

    public bool IsAcked
    {
        get { return isAcked; }
        set { isAcked = value; }
    }

    public void CompleteMiniGame1(int game, bool completed)
    {
        for (int i = 0; i < games.Count; i++)
        {
            if (game == i)
            {
                games[i] = completed;
            }
        }
    }
}
