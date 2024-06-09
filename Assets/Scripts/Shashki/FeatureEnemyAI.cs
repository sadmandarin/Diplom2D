using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FeatureEnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject game;

    private ReloadShashki reload;

    public GameObject feature;

    [SerializeField]
    private GameObject win;

    [SerializeField]
    private GameObject lose;

    public Square square;

    public bool loaded;

    public GameData gameData;

    private bool _walk;

    private GameObject target;

    Coroutine _coroutine;

    private bool once = false;

    private bool isOurChess = false;

    private bool isLose = false;

    private AudioSource source;

    public int x;
    public int y;

    public bool IsOurChess
    {
        get { return isOurChess; }
        set { isOurChess = value; }
    }

    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }

    public bool Walk
    {
        get { return _walk; }
        set { _walk = value; }
    }

    public GameObject Win
    {
        get { return win; }
    }

    public GameObject Lose
    {
        get { return lose; }
    }

    public GameData GameData
    {
        get { return gameData; }
    }
    private void Start()
    {
        source = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        reload = GameObject.Find("Controller").GetComponent<ReloadShashki>();

        game = GameObject.Find("Shashki(Clone)").gameObject;
    }

    private void Update()
    {
        if (once == false && isOurChess == true)
        {
            StartCoroutine(square.startqueue(x, y));

            once = true;
        }

        if (_walk && square.step == false)
        {
            _coroutine = StartCoroutine(Moving());
        }
        
    }

    IEnumerator Moving()
    {
        GameObject objectMove = feature;

        while (Vector3.Distance(target.transform.position, objectMove.transform.position) > 0.01f)
        {
            feature.transform.position = Vector3.MoveTowards(objectMove.transform.position, target.transform.position, 0.1f);

            yield return new WaitForSeconds(0.5f);
        }

        _walk = false;

        square.SquareArray[x, y].GetComponent<Move>().Loaded = false;

        x = target.GetComponent<Move>().x;

        y = target.GetComponent<Move>().y;

        square.SquareArray[x, y].GetComponent<Move>().Loaded = true;

        _coroutine = null;

        once = false;

        square.step = true;

        isOurChess = false;

        Vertex vertex = new Vertex(x, y);

        if ((x == 0 && y == 0) || (x == 0 && y == 2) || (x == 0 && y == 4) || (x == 0 && y == 6))
        {
            if (isLose == false)
            {
                Debug.Log("You Lose");

                source.PlayOneShot(square.loseClip);

                GameObject lose = Instantiate(GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<FeatureEnemyAI>().Lose, GameObject.FindGameObjectsWithTag("Enemy")[0].transform.position, Quaternion.identity);

                StartCoroutine(Restart(lose));
            }
            isLose = true;

            
        }

        
    }

    IEnumerator Restart(GameObject lose)
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(lose);

        reload.ReloadAssets(game);
    }
}
