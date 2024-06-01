using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Distance { get; set; }
    public Vertex Previous { get; set; }
    public bool IsVisited { get; set; }
    public bool IsOccupied { get; set; }

    public Vertex(int x, int y)
    {
        X = x;
        Y = y;
        Distance = int.MaxValue;
        Previous = null;
        IsVisited = false;
        IsOccupied = false;
    }
}

public class Square : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    GameObject[,] squareArray;
    [SerializeField]
    private GameObject players;
    [SerializeField]
    private GameObject enemy;
    public bool step = true;
    private int pathLenght = 32132121;
    public GameObject selectedFeature;
    private List<Vector2Int> targets = new List<Vector2Int>();

    [SerializeField]
    private GameObject game;

    public GameObject[,] SquareArray
    {
        get { return squareArray; }
    }

    private void Awake()
    {
        squareArray = new GameObject[8, 8];
        CreateField();
        targets.Add(new Vector2Int(0, 0));
        targets.Add(new Vector2Int(0, 2));
        targets.Add(new Vector2Int(0, 4));
        targets.Add(new Vector2Int(0, 6));
    }

    void CreateField()
    {
        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Vector3 position = new Vector3(i - 11.6f - 0.07f * i, j - 3.4f - 0.05f * j, -1);
                GameObject clone = Instantiate(prefab, position + new Vector3(0.1f, 0, 0), Quaternion.identity);
                squareArray[i, j] = clone;
                if ((i + j) % 2 == 0)
                {
                    clone.AddComponent<Move>();
                    clone.GetComponent<Move>().square = gameObject.GetComponent<Square>();
                    clone.GetComponent<Move>().x = i;
                    clone.GetComponent<Move>().y = j;
                }

                if (i == 7 && j == 3)
                {
                    clone.AddComponent<FeatureEnemyAI>();
                    clone.GetComponent<FeatureEnemyAI>().square = gameObject.GetComponent<Square>();
                    clone.GetComponent<FeatureEnemyAI>().x = i;
                    clone.GetComponent <FeatureEnemyAI>().y = j;
                }
            }
        }
        CreateDot();
    }

    void CreateDot()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (i < 1)
                {
                    if ((i+j)%2 == 0)
                    {
                        Vector3 pos = squareArray[i, j].transform.position;
                        GameObject clone = Instantiate(players, pos, Quaternion.identity, gameObject.transform);
                        clone.name = "Player";
                        squareArray[i, j].GetComponent<Move>().feature = clone;
                        squareArray[i, j].GetComponent<Move>().Loaded = true;
                    }

                }
                if ((i == 7 && j == 3))
                {
                    Vector3 pos = squareArray[i, j].transform.position;
                    GameObject clone = Instantiate(enemy, pos, Quaternion.identity, gameObject.transform);
                    clone.name = "Enemy";
                    squareArray[i, j].GetComponent <FeatureEnemyAI>().feature = clone;
                    squareArray[i, j].GetComponent<Move>().Loaded = true;
                }
            }
        }
    }

    public void WalkController(int I, int J)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (squareArray[i, j].GetComponent<Move>())
                {
                    if (!squareArray[i, j].GetComponent<Move>().select)
                    {
                        if ((J - j == -1 || J-j == 1))
                        {
                            if(I - i == -1)
                            {
                                if (!squareArray[i,j].GetComponent<Move>().Loaded)
                                {
                                    squareArray[i, j].GetComponentInChildren<SpriteRenderer>().enabled = true;

                                    squareArray[i, j].GetComponent<Move>().walk = true;

                                    squareArray[I, J].GetComponent<Move>().Loaded = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void RevertWalk(int I, int J)
    {
        squareArray[I, J].GetComponent<Move>().feature = selectedFeature;
        selectedFeature = null;
        squareArray[I, J].GetComponent<Move>().Loaded = true;
        Clear();
    }

    public void WalkEnemyController(Vector2Int target)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (squareArray[i, j].GetComponent<FeatureEnemyAI>())
                {
                    if (squareArray[i, j].GetComponent<Move>())
                    {
                        squareArray[i, j].GetComponent<FeatureEnemyAI>().Walk = true;

                        squareArray[i, j].GetComponent <FeatureEnemyAI>().Target = squareArray[target.x, target.y];
                    }
                }
            }
        }
    }

    private List<Vector2Int> Dijkstra(int startI, int startJ, List<Vector2Int> targets)
    {
        Vertex[,] vertex = new Vertex[8, 8];
        
        for (int i = 0;i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if ((i + j) % 2 == 0)
                {
                    vertex[i, j] = new Vertex(i, j);

                    if (squareArray[i, j].GetComponent<Move>().Loaded)
                    {
                        vertex[i, j].IsOccupied = true;
                    }
                }
                
            }
        }
        Queue<Vertex> queue = new Queue<Vertex>();
        vertex[startI, startJ].Distance = 0;
        queue.Enqueue(vertex[startI, startJ]);
        while (queue.Count > 0)
        {
            Vertex current = queue.Dequeue();
            current.IsVisited = true;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx != 0 || dy != 0)
                    {
                        int newX = current.X + dx;
                        int newY = current.Y + dy;
                        if ((newX + newY) % 2 == 0)
                        {
                            if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                            {
                                Vertex neighbour = vertex[newX, newY];
                                if (!neighbour.IsOccupied && !neighbour.IsVisited)
                                {
                                    int newDistance = current.Distance + 1;

                                    if (newDistance < neighbour.Distance)
                                    {
                                        neighbour.Distance = newDistance;
                                        neighbour.Previous = current;
                                        queue.Enqueue(neighbour);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        List<Vector2Int> shortestPath = null;
        int shortestDistance = int.MaxValue;
        foreach (var target in targets)
        {
            Vertex current = vertex[target.x, target.y];
            if (current.Distance < shortestDistance)
            {
                shortestDistance = current.Distance;
                shortestPath = new List<Vector2Int>();
                while (current != null)
                {
                    shortestPath.Add(new Vector2Int(current.X, current.Y));
                    current = current.Previous;
                }
            }
            else if (current.Distance == shortestDistance)
            {

            }
        }
        if (shortestPath == null)
        {
            return new List<Vector2Int>();
        }
        shortestPath.Reverse();
        if (shortestPath.Count == 1)
        {
            shortestPath.Clear();
        }
        foreach (var item in shortestPath)
        {
            Debug.Log(item);
        }
        return shortestPath;
    }



    public void Clear()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (squareArray[i, j].GetComponent<Move>())
                {
                    if (!squareArray[i, j].GetComponent<Move>().select)
                    {
                        squareArray[i, j].GetComponentInChildren<SpriteRenderer>().enabled = false;

                        squareArray[i, j].GetComponent<Move>().walk = false;
                    }

                }
                if (squareArray[i, j].GetComponent<FeatureEnemyAI>())
                {
                    squareArray[i, j].GetComponent<FeatureEnemyAI>().IsOurChess = true;
                }
            }
        }
    }

    void IfNoPathFound(int I, int J)
    {
        List<Vector2Int> posiblePoints = new List<Vector2Int>();
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx != 0 || dy != 0)
                {
                    int newX = I + dx;
                    int newY = J + dy;

                    if ((newX + newY) % 2 == 0)
                    {
                        if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                        {
                            

                            if (squareArray[newX, newY].GetComponent<Move>().Loaded == false)
                            {
                                posiblePoints.Add(new Vector2Int(newX, newY));
                            }
                        }
                    }
                }
            }
        }
        if (posiblePoints.Count != 0)
        {
            WalkEnemyController(posiblePoints[Random.Range(0, posiblePoints.Count)]);
        }

        else 
        {
            StartCoroutine(WinGame());
        }
    }

    public IEnumerator startqueue(int i, int j)
    {
        yield return new WaitForSeconds(0.2f);
        List<Vector2Int> path = Dijkstra(i, j, targets);
        if (path.Count != 0)
        {
            WalkEnemyController(path[1]);
        }
        else
        {
            IfNoPathFound(i, j);
        }
    }

    private void OnDestroy()
    {
        GameObject[] squares = GameObject.FindGameObjectsWithTag("Square");

        for (int i = 0; i < squares.Length; i++)
        {
            Destroy(squares[i]);
        }
    }

    IEnumerator WinGame()
    {
        GameObject win = Instantiate(GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<FeatureEnemyAI>().Win, GameObject.FindGameObjectsWithTag("Enemy")[0].transform.position, Quaternion.identity);

        GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<FeatureEnemyAI>().GameData.CompleteMiniGame1(0, true);

        yield return new WaitForSeconds(2);

        Destroy(win);

        Destroy(game);

        Destroy(gameObject);
    } 
}
