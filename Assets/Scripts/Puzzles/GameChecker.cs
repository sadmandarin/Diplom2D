using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChecker : MonoBehaviour
{
    [SerializeField]
    private IsOnPlace[] puzzles;

    [SerializeField]
    private DetectObject[] places;

    [SerializeField]
    private GameObject newLevelPrefab;

    private bool levelDone = false;

    public bool LevelDone { get { return levelDone; } }

    private bool IsCorrectPosition()
    {
        foreach (IsOnPlace place in puzzles)
        {
            if (place.IsCorrectPlace == false)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsFullFilled()
    {
        foreach(DetectObject obj in places)
        {
            if (obj.IsFilled == false)
            {
                return false;
            }
        }

        return true;
    }

    private void Update()
    {
        if (levelDone == false)
        {
            StartCoroutine(GameWin());
        }
        
    }

    private IEnumerator GameWin()
    {
        yield return new WaitForSeconds(1);
        if (IsFullFilled()) 
        {
            if (IsCorrectPosition())
            {
                levelDone = true;

                Debug.Log("You Win the Game");
            }
            
            else
            {
                Debug.Log("Try something else");
            }
        }
    }
}
