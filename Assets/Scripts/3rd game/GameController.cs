using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int hod = 0;

    [SerializeField]
    private GameObject game;

    private Coroutine coroutine;

    [SerializeField]
    private Transform girl;

    [SerializeField]
    private Transform ghost;

    [SerializeField]
    private GameObject nextgame;

    [SerializeField]
    private int currentLvl;

    [SerializeField]
    private Node targetOfGame;

    [SerializeField]
    private GameObject star;

    private ReloadGhostGame reload;

    [SerializeField]
    private GameData gameData;

    AudioSource source;

    [SerializeField]
    AudioClip clip;

    public int CurrentLvl
    {
        get { return currentLvl; }
    }

    public int Hod { get { return hod; } set { hod = value; } }

    public Coroutine Coroutine { get { return coroutine; } set { coroutine = value; } }


    private void Start()
    {
        source = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        reload = GameObject.Find("Controller").GetComponent<ReloadGhostGame>();
    }

    public IEnumerator MovingCharacter(GameObject player, Vector3 actualPos)
    {
        while (Vector3.Distance(player.transform.position, actualPos) > 0.01f)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, actualPos, 4.5f * Time.deltaTime);

            yield return new WaitForSeconds(0.01f);
        }

        if (targetOfGame == gameObject.GetComponent<CharacterControlelr>().CurrentNode)
        {
            source.PlayOneShot(clip);

            StartCoroutine(MovingStar());
        }

        if (Vector3.Distance(ghost.position, girl.position) <= 0.5)
        {
            source.PlayOneShot(gameObject.GetComponent<Detection>().Clip);

            Debug.Log("Game Over");

            reload.ReloadAssets(game, game.GetComponent<GameController>().CurrentLvl);
        }


        hod++;

        coroutine = null;
    }


        
    

    public void WaitForPlayerMove()
    {
        hod++;
    }

    IEnumerator MovingStar()
    {
        int step = 0;

        star.GetComponent<SpriteRenderer>().sortingOrder = 15;

        while(step < 100)
        {
            star.transform.position += new Vector3(0, 2f, 0) * Time.deltaTime;

            yield return new WaitForSeconds(0.01f);

            step++;
        }

        if (currentLvl == 1 || currentLvl == 2)
        {
            Instantiate(nextgame);

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);

            gameData.CompleteMiniGame1(2, true);

            GameObject.Find("девочка").GetComponent<GGMove>().isMiniGameRunning = false;

            GameObject.Find("девочка").gameObject.transform.position = new Vector3(-9.13f, -1.9f, 1);

        }
    }
}
