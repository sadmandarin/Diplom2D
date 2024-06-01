using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField]
    private GameObject ghost;

    [SerializeField]
    private GameObject targetSprite;

    [SerializeField]
    private Node _currentNode;

    private bool finded = false;

    private Node _targetNode;

    bool isActive = false;

    

    private void Update()
    {
        if (gameObject.GetComponent<GameController>().Hod % 2 == 1)
        {
            if (IsOnSameLine(_currentNode, gameObject.GetComponent<CharacterControlelr>().CurrentNode))
            {
                finded = true;

                _targetNode = gameObject.GetComponent<CharacterControlelr>().CurrentNode;

                targetSprite.transform.position = _targetNode.gameObject.transform.position;

                targetSprite.SetActive(true);

                var path = GetPathOnLine(_currentNode, gameObject.GetComponent<CharacterControlelr>().CurrentNode);

                if (path.Count == 0)
                {
                    finded = false;

                    targetSprite.SetActive(false);

                    gameObject.GetComponent<GameController>().WaitForPlayerMove();
                }

                else if (path.Count == 1)
                {
                    Debug.Log("Game Over");

                    Destroy(gameObject);
                }

                else if (path.Count > 1 && gameObject.GetComponent<GameController>().Coroutine == null)
                {
                    MoveToNode(path[1]);

                    path.Clear();
                }
            }

            else
            {
                if (_targetNode != null)
                {
                    if (_currentNode != _targetNode)
                    {
                        if (finded == true)
                        {
                            targetSprite.transform.position = _targetNode.gameObject.transform.position;

                            targetSprite.SetActive(true);
                        }

                        else
                        {
                            {
                                targetSprite.SetActive(false);
                            }
                        }

                        var path = GetPathOnLine(_currentNode, _targetNode);

                        if (path.Count == 0)
                        {
                            finded = false;

                            targetSprite.SetActive(false);
                            gameObject.GetComponent<GameController>().WaitForPlayerMove();
                        }

                        if (path.Count != 0 && gameObject.GetComponent<GameController>().Coroutine == null)
                        {
                            MoveToNode(path[1]);

                            path.Clear();
                        }
                    }

                    else
                    {
                        Destroy(gameObject);

                        Debug.Log("Game Over");
                    }
                }
                else
                {
                    gameObject.GetComponent<GameController>().WaitForPlayerMove();
                }
                
            }
        }
    }

    void MoveToNode(Node target)
    {
        _currentNode = target;

        Vector3 actualPos = target.transform.position - new Vector3(0, -0.9f, 0);

        gameObject.GetComponent<GameController>().Coroutine = StartCoroutine(gameObject.GetComponent<GameController>().MovingCharacter(ghost, actualPos));
    }


    bool IsOnSameLine(Node target, Node current)
    {
        for (int i = 0; target.Lines.Count > i; i++)
        {
            for (int j = 0; j < current.Lines.Count; j++)
            {
                if (target.Lines[i] == current.Lines[j])
                {
                    return true;
                }
            }
        }

        return false;

    }

    public List<Node> GetPathOnLine(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        if (!IsOnSameLine(startNode, targetNode))
        {
            return path; // ѕуть не найден
        }

        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        Queue<Node> frontier = new Queue<Node>();
        HashSet<Node> visitedNodes = new HashSet<Node>();

        frontier.Enqueue(startNode);
        visitedNodes.Add(startNode);

        while (frontier.Count > 0)
        {
            Node currentNode = frontier.Dequeue();

            if (currentNode == targetNode)
            {
                path = ReconstructPath(cameFrom, startNode, targetNode);
                return path;
            }

            foreach (Node neighbor in currentNode.Neighbours)
            {
                if (visitedNodes.Contains(neighbor) || neighbor.gameObject.tag == "NonGhost" || !IsOnSameLine(neighbor, targetNode) || currentNode.Lines.Contains(7) && neighbor.Lines.Contains(7))
                {
                    continue;
                }

                visitedNodes.Add(neighbor);
                cameFrom[neighbor] = currentNode;
                frontier.Enqueue(neighbor);
            }
        }

        return path; // ѕуть не найден
    }

    private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = cameFrom[currentNode];
        }
        path.Add(startNode);
        path.Reverse();
        return path;
    }
}