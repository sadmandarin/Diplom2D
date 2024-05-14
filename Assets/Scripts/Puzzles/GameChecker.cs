using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChecker : MonoBehaviour
{
    [SerializeField]
    private IsOnPlace[] puzzles;

    [SerializeField]
    private List<GameObject> puzzle1;

    [SerializeField]
    private DetectObject[] places;

    [SerializeField] private List<GameObject> connect;

    [SerializeField]
    private List<GameObject> placeList;

    [SerializeField]
    private GameObject newLevelPrefab;

    private bool outPuzzle = false;

    private bool levelDone = false;

    private bool isPuzzleOut = false;

    public bool LevelDone { get { return levelDone; } }

    public bool IsPuzzleOut { get {  return isPuzzleOut; } }

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
            if (IsCorrectPosition() && !outPuzzle)
            {
                levelDone = true;

                outPuzzle = true;

                foreach (GameObject obj in placeList)
                {
                    obj.SetActive(false);
                }

                foreach (GameObject obj in connect)
                {
                    obj.SetActive(false);

                    yield return new WaitForSeconds(0.3f);
                }   

                RemovePuzzles();

                newLevelPrefab.SetActive(false);

                   

                Debug.Log("You Win the Game");
            }
            
            else
            {
                Debug.Log("Try something else");
            }
        }
    }

    void RemovePuzzles()
    {
        
        foreach (var puzzle in puzzle1)
        {
            Debug.Log("КОрутина рабоатет");

            StartCoroutine(RemovePuzzle(puzzle));
        }
        
        
    }

    IEnumerator RemovePuzzle(GameObject puzzle)
    {
        yield return new WaitForSeconds(0.7f);

        while (puzzle.transform.position.y < 14f)
        {
            puzzle.GetComponent<CircleCollider2D>().enabled = false;

            puzzle.transform.position += new Vector3(0, 0.04f, 0);

            yield return null;
        }

        if (puzzle.transform.position.y >= 14)
        {
            isPuzzleOut = true;

        }
    }
}
